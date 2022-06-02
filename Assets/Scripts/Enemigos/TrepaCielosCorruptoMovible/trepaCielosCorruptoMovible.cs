using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrepaCielosCorruptoMovible : TrepaCielosCorrupto
{
    private bool direccionAdelante = true;

    [Header("Objetivos a seguir")]
    [SerializeField] private Transform[] camino;

    [Header("Numero del objetivo actual")]
    [SerializeField] private int puntoActual;

    [Header("Objetivo actual")]
    [SerializeField] private Transform puntoActualMeta;

    [Header("Distancia para cambiar de objetivo")]
    [SerializeField] private float distanciaAlPunto;

    public override void gestionarDistancias()
    {
        if (ObjetivoPerseguir != null) 
        {
            if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) <= RadioPersecucion
                && Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) >= RadioAtaque)
            {
                if (EstadoEnemigo != null) 
                {
                    if (EstadoEnemigo.Estado == EstadoGenerico.caminando
                        || EstadoEnemigo.Estado == EstadoGenerico.durmiendo
                        || EstadoEnemigo.Estado == EstadoGenerico.ninguno)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, ObjetivoPerseguir.position, VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        Vector3 refAnimacion = ObjetivoPerseguir.position - vectorTemporal;
                        cambiarAnimaciones(refAnimacion);
                        if (EnemigoRigidBody != null)
                        {
                            EnemigoRigidBody.MovePosition(transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }  
                        EstadoEnemigo.Estado = EstadoGenerico.caminando;
                    }
                }
            }
            else
            {
                if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) > RadioPersecucion)
                {
                    if (Vector3.Distance(gameObject.transform.position, camino[puntoActual].position) > distanciaAlPunto)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, camino[puntoActual].position, VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        Vector3 refAnimacion = camino[puntoActual].position - vectorTemporal;
                        cambiarAnimaciones(refAnimacion);
                        if (EnemigoRigidBody != null)
                        {
                            EnemigoRigidBody.MovePosition(transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }
                        if (EstadoEnemigo != null) 
                        {
                            EstadoEnemigo.Estado = EstadoGenerico.caminando;
                        }
                    }
                    else
                    {
                        cambiarPuntoMeta();
                    }
                }
            }
        }
    }

    private void cambiarPuntoMeta() 
    {
        if (direccionAdelante)
        {
            puntoActual++;
            if (puntoActualMeta != null) 
            {
                puntoActualMeta = camino[puntoActual];
            }
            if (puntoActual == (camino.Length - 1))
            {
                direccionAdelante = false;
            }
        }
        else 
        {
            puntoActual--;
            if (puntoActualMeta != null)
            {
                puntoActualMeta = camino[puntoActual];
            }
            if (puntoActual == 0)
            {
                direccionAdelante = true;
            }
        }
    }

}
