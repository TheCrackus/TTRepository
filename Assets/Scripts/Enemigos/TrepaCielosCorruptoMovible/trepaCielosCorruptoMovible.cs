using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoMovible : trepaCielosCorrupto
{
    private bool direccionAdelante = true;
    [Header("Objetivos a seguir")]
    public Transform[] camino;
    [Header("Numero del objetivo actual")]
    public int puntoActual;
    [Header("Objetivo actual")]
    public Transform puntoActualMeta;
    [Header("Distancia para cambiar de objetivo")]
    public float distanciaAlPunto;

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
            }
        }
        else
        {
            if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) > radioPersecucion)
            {
                if (Vector3.Distance(gameObject.transform.position, camino[puntoActual].position) > distanciaAlPunto)
                {
                    Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, camino[puntoActual].position, velocidadMovimientoEnemigo * Time.deltaTime);
                    Vector3 refAnimacion = camino[puntoActual].position - vectorTemporal;
                    Vector3 vectorMovimiento = cambiaAnimaciones(refAnimacion);
                    getEnemigoRigidBody().MovePosition(transform.position + vectorMovimiento * velocidadMovimientoEnemigo * Time.fixedDeltaTime);
                }
                else
                {
                    cambiaPuntoMeta();
                }
            }
        }
    }

    private void cambiaPuntoMeta() 
    {
        if (direccionAdelante)
        {
            puntoActual++;
            puntoActualMeta = camino[puntoActual];
            if (puntoActual == (camino.Length - 1))
            {
                direccionAdelante = false;
            }
        }
        else 
        {
            puntoActual--;
            puntoActualMeta = camino[puntoActual];
            if (puntoActual == 0)
            {
                direccionAdelante = true;
            }
        }
    }

}
