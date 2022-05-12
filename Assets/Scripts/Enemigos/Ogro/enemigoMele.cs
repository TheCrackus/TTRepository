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
        setEstadoEnemigo(estadoGenerico.ninguno);
        setContadorEsperaMovimiento(getTiempoEsperaMovimiento());
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
        getPosicionMapa().transform.position = getPosicionOriginal();
        setObjetivoPerseguir(GameObject.FindWithTag("Player").transform);
        setEnemigoRigidBody(gameObject.GetComponent<Rigidbody2D>());
        setEnemigoAnimator(gameObject.GetComponent<Animator>());
        setEstadoEnemigo(estadoGenerico.ninguno);
    }

    public override void gestionDistancias()
    {
        if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) >= radioAtaque)
        {
            if (getEstadoEnemigo() == estadoGenerico.caminando
                || getEstadoEnemigo() == estadoGenerico.ninguno)
            {
                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, getObjetivoPerseguir().position, getVelocidadMovimientoEnemigo() * Time.fixedDeltaTime);
                Vector3 refAnimacion = getObjetivoPerseguir().position - vectorTemporal;
                Vector3 vectorMovimiento = cambiaAnimaciones(refAnimacion);
                getEnemigoRigidBody().MovePosition(transform.position + vectorMovimiento * getVelocidadMovimientoEnemigo() * Time.fixedDeltaTime);
                setEstadoEnemigo(estadoGenerico.caminando);
            }
        }
        else
        {
            if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioAtaque)
            {
                if (getEstadoEnemigo() == estadoGenerico.caminando
                    || getEstadoEnemigo() == estadoGenerico.ninguno)
                {
                    setEstadoEnemigo(estadoGenerico.atacando);
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
        if (getEstadoEnemigo() == estadoGenerico.atacando)
        {
            setEstadoEnemigo(estadoGenerico.ninguno);
        }
    }
}
