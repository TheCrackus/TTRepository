using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrepaCielosCorruptoArea : TrepaCielosCorrupto
{
    [Header("Limite de persecucion")]
    [SerializeField] private Collider2D perimetro;

    public override void gestionarDistancias()
    {
        if (ObjetivoPerseguir != null) 
        {
            if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) <= RadioPersecucion
                && Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) >= RadioAtaque
                && perimetro.bounds.Contains(ObjetivoPerseguir.transform.position))
            {
                if (EstadoEnemigo != null)
                {
                    if (EstadoEnemigo.Estado == EstadoGenerico.caminando
                       || EstadoEnemigo.Estado == EstadoGenerico.durmiendo
                       || EstadoEnemigo.Estado == EstadoGenerico.ninguno)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position,
                            ObjetivoPerseguir.position,
                            VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        Vector3 refAnimacion = ObjetivoPerseguir.position - vectorTemporal;
                        cambiarAnimaciones(refAnimacion);
                        if (EnemigoRigidBody != null)
                        {
                            EnemigoRigidBody.MovePosition(gameObject.transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }
                        EstadoEnemigo.Estado = EstadoGenerico.caminando;
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
                            if (EstadoEnemigo.Estado == EstadoGenerico.caminando
                               || EstadoEnemigo.Estado == EstadoGenerico.durmiendo
                               || EstadoEnemigo.Estado == EstadoGenerico.ninguno)
                            {
                                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position,
                                    PosicionMapa.transform.position,
                                VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                                Vector3 refAnimacion = PosicionMapa.transform.position - vectorTemporal;
                                cambiarAnimaciones(refAnimacion);
                                if (EnemigoRigidBody != null)
                                {
                                    EnemigoRigidBody.MovePosition(gameObject.transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                                }
                                EstadoEnemigo.Estado = EstadoGenerico.caminando;
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
                            EstadoEnemigo.Estado = EstadoGenerico.durmiendo;
                        }
                    }
                }
            }
        }
    }

}
