using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoMovible : trepaCielosCorrupto
{

    public Transform[] camino;
    public int puntoActual;
    public Transform puntoActualMeta;
    public float distanciaAlPunto;
    private bool direccionAdelante = true;

    public override void gestionDistancias()
    {
        if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) >= radioAtaque)
        {
            PlayerState estadoPlayer = getPlayer().GetComponent<movimientoPlayer>().getEstadoActualPlayer();
            if (getEstadoActualEnemigo() != EnemyState.estuneado
                && getEstadoActualEnemigo() != EnemyState.atacando
                && getEstadoActualEnemigo() != EnemyState.inactivo
                && (getEstadoActualEnemigo() == EnemyState.caminando
                    || getEstadoActualEnemigo() == EnemyState.durmiendo
                    || getEstadoActualEnemigo() == EnemyState.ninguno)
                && estadoPlayer != PlayerState.estuneado
                && estadoPlayer != PlayerState.inactivo
                && estadoPlayer != PlayerState.interactuando
                && (estadoPlayer == PlayerState.caminando
                    || estadoPlayer == PlayerState.atacando
                    || estadoPlayer == PlayerState.ninguno))
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
