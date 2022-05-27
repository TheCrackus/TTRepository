using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoArea : trepaCielosCorrupto
{
    [Header("Limite de persecucion")]
    [SerializeField] private Collider2D perimetro;

    public override void gestionDistancias()
    {
        if (ObjetivoPerseguir != null) 
        {
            if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) <= RadioPersecucion
                && Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) >= RadioAtaque
                && perimetro.bounds.Contains(ObjetivoPerseguir.transform.position))
            {
                if (EstadoEnemigo != null)
                {
                    if (EstadoEnemigo.Estado == estadoGenerico.caminando
                       || EstadoEnemigo.Estado == estadoGenerico.durmiendo
                       || EstadoEnemigo.Estado == estadoGenerico.ninguno)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position,
                            ObjetivoPerseguir.position,
                            VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        Vector3 refAnimacion = ObjetivoPerseguir.position - vectorTemporal;
                        cambiaAnimaciones(refAnimacion);
                        if (EnemigoRigidBody != null)
                        {
                            EnemigoRigidBody.MovePosition(gameObject.transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }
                        EstadoEnemigo.Estado = estadoGenerico.caminando;
                        if (EnemigoAnimator != null)
                        {
                            EnemigoAnimator.SetBool("Despertar", true);
                        }
                    }
                }
            }
            else
            {
                if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) > RadioPersecucion
                    || !perimetro.bounds.Contains(ObjetivoPerseguir.transform.position))
                {
                    if (Vector3.Distance(PosicionMapa.transform.position, gameObject.transform.position) > RadioAtaque)
                    {
                        if (EstadoEnemigo != null)
                        {
                            if (EstadoEnemigo.Estado == estadoGenerico.caminando
                               || EstadoEnemigo.Estado == estadoGenerico.durmiendo
                               || EstadoEnemigo.Estado == estadoGenerico.ninguno)
                            {
                                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position,
                                    PosicionMapa.transform.position,
                                VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                                Vector3 refAnimacion = PosicionMapa.transform.position - vectorTemporal;
                                cambiaAnimaciones(refAnimacion);
                                if (EnemigoRigidBody != null)
                                {
                                    EnemigoRigidBody.MovePosition(gameObject.transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                                }
                                EstadoEnemigo.Estado = estadoGenerico.caminando;
                                if (EnemigoAnimator != null)
                                {
                                    EnemigoAnimator.SetBool("Despertar", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (EnemigoAnimator != null)
                        {
                            EnemigoAnimator.SetBool("Despertar", false);
                        }
                        if (EstadoEnemigo != null)
                        {
                            EstadoEnemigo.Estado = estadoGenerico.durmiendo;
                        }
                    }
                }
            }
        }
    }

}
