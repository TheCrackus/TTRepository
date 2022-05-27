using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoMovible : trepaCielosCorrupto
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

    public override void gestionDistancias()
    {
        if (ObjetivoPerseguir != null) 
        {
            if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) <= RadioPersecucion
                && Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) >= RadioAtaque)
            {
                if (EstadoEnemigo != null) 
                {
                    if (EstadoEnemigo.Estado == estadoGenerico.caminando
                        || EstadoEnemigo.Estado == estadoGenerico.durmiendo
                        || EstadoEnemigo.Estado == estadoGenerico.ninguno)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, ObjetivoPerseguir.position, VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        Vector3 refAnimacion = ObjetivoPerseguir.position - vectorTemporal;
                        cambiaAnimaciones(refAnimacion);
                        if (EnemigoRigidBody != null)
                        {
                            EnemigoRigidBody.MovePosition(transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }  
                        EstadoEnemigo.Estado = estadoGenerico.caminando;
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
                        cambiaAnimaciones(refAnimacion);
                        if (EnemigoRigidBody != null)
                        {
                            EnemigoRigidBody.MovePosition(transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }
                        if (EstadoEnemigo != null) 
                        {
                            EstadoEnemigo.Estado = estadoGenerico.caminando;
                        }
                    }
                    else
                    {
                        cambiaPuntoMeta();
                    }
                }
            }
        }
    }

    private void cambiaPuntoMeta() 
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
