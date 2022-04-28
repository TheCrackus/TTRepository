using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoMele : trepaCielosCorrupto
{
    private AnimationClip atacandoClip;

    public override void Start()
    {
        setObjetivoPerseguir(GameObject.FindWithTag("Player").transform);
        setEnemigoRigidBody(gameObject.GetComponent<Rigidbody2D>());
        setEnemigoAnimator(gameObject.GetComponent<Animator>());
        setEstadoActualEnemigo(EnemyState.ninguno);
        setContadorEsperaMovimiento(tiempoEsperaMovimiento);
        setPuedoMoverme(true);
        foreach (AnimationClip clip in getEnemigoAnimator().runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Atacando Arriba")
            {
                atacandoClip = clip;
            }
        }
    }

    public override void OnEnable()
    {
        miPosicionMapa.transform.position = posicionOriginal;
        setVidaEnemigo(vidaMaxima.valorInicial);
        setObjetivoPerseguir(GameObject.FindWithTag("Player").transform);
        setEnemigoRigidBody(gameObject.GetComponent<Rigidbody2D>());
        setEnemigoAnimator(gameObject.GetComponent<Animator>());
        setEstadoActualEnemigo(EnemyState.ninguno);
    }

    public override void gestionDistancias()
    {
        if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) >= radioAtaque)
        {
            if (getEstadoActualEnemigo() != EnemyState.estuneado
                && getEstadoActualEnemigo() != EnemyState.atacando
                && getEstadoActualEnemigo() != EnemyState.inactivo
                && (getEstadoActualEnemigo() == EnemyState.caminando
                    || getEstadoActualEnemigo() == EnemyState.durmiendo
                    || getEstadoActualEnemigo() == EnemyState.ninguno))
            {
                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, getObjetivoPerseguir().position, velocidadMovimientoEnemigo * Time.deltaTime);
                Vector3 refAnimacion = getObjetivoPerseguir().position - vectorTemporal;
                Vector3 vectorMovimiento = cambiaAnimaciones(refAnimacion);
                getEnemigoRigidBody().MovePosition(transform.position + vectorMovimiento * velocidadMovimientoEnemigo * Time.fixedDeltaTime);
                setEstadoActualEnemigo(EnemyState.caminando);
            }
        }
        else
        {
            if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioAtaque)
            {
                if (getEstadoActualEnemigo() != EnemyState.estuneado
                && getEstadoActualEnemigo() != EnemyState.atacando
                && getEstadoActualEnemigo() != EnemyState.inactivo
                && (getEstadoActualEnemigo() == EnemyState.caminando
                    || getEstadoActualEnemigo() == EnemyState.durmiendo
                    || getEstadoActualEnemigo() == EnemyState.ninguno))
                {
                    setEstadoActualEnemigo(EnemyState.atacando);
                    StartCoroutine(ataca());
                }
            }
        }
    }

    private IEnumerator ataca() 
    {
        getEnemigoAnimator().SetBool("Atacar", true);
        yield return new WaitForSeconds(atacandoClip.length);
        getEnemigoAnimator().SetBool("Atacar", false);
        if (getEstadoActualEnemigo() != EnemyState.caminando
            && getEstadoActualEnemigo() == EnemyState.atacando
            && getEstadoActualEnemigo() != EnemyState.ninguno
            && getEstadoActualEnemigo() != EnemyState.estuneado
            && getEstadoActualEnemigo() != EnemyState.durmiendo
            && getEstadoActualEnemigo() != EnemyState.inactivo)
        {
            setEstadoActualEnemigo(EnemyState.ninguno);
        }
    }
}
