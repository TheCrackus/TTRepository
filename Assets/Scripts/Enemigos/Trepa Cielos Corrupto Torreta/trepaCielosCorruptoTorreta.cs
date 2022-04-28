using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoTorreta : trepaCielosCorrupto
{
    private float tiempoDisparoSegundos;
    private bool puedoDisparar;
    [Header("El proyectil que arroja el enemigo")]
    public GameObject proyectilPiedra;
    [Header("El tiempo entre cada disparo")]
    public float tiempoDisparo;

    public override void Start()
    {
        base.Start();
        puedoDisparar = false;
    }

    public override void Update()
    {
        base.Update();
        if (!puedoDisparar) 
        {
            tiempoDisparoSegundos -= Time.deltaTime;
            if (tiempoDisparoSegundos <= 0)
            {
                tiempoDisparoSegundos = tiempoDisparo;
                puedoDisparar = true;
            }
        }
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
                if (puedoDisparar) 
                {
                    Vector3 vectorTemporal = getObjetivoPerseguir().transform.position - gameObject.transform.position;
                    GameObject proyectilActual = Instantiate(proyectilPiedra, gameObject.transform.position, Quaternion.identity);
                    proyectilActual.GetComponent<proyectilPiedra>().arroja(vectorTemporal);
                    puedoDisparar = false;
                }
                setEstadoActualEnemigo(EnemyState.caminando);
                getEnemigoAnimator().SetBool("Despertar", true);
            }
        }
        else
        {
            if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) > radioPersecucion)
            {
                getEnemigoAnimator().SetBool("Despertar", false);
                setEstadoActualEnemigo(EnemyState.durmiendo);
            }
        }
    }

}
