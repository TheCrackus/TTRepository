using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoArea : trepaCielosCorrupto
{
    [Header("Limite de persecucion")]
    [SerializeField] private Collider2D perimetro;

    public override void gestionDistancias()
    {
        if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) >= radioAtaque
            && perimetro.bounds.Contains(getObjetivoPerseguir().transform.position))
        {
            if (getEstadoEnemigo() == estadoGenerico.caminando
               || getEstadoEnemigo() == estadoGenerico.durmiendo
               || getEstadoEnemigo() == estadoGenerico.ninguno)
            {
                Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, getObjetivoPerseguir().position, 
                    getVelocidadMovimientoEnemigo() * Time.fixedDeltaTime);
                Vector3 refAnimacion = getObjetivoPerseguir().position - vectorTemporal;
                Vector3 vectorMovimiento = cambiaAnimaciones(refAnimacion);
                getEnemigoRigidBody().MovePosition(gameObject.transform.position + vectorMovimiento * getVelocidadMovimientoEnemigo() * Time.fixedDeltaTime);
                setEstadoEnemigo(estadoGenerico.caminando);
                getEnemigoAnimator().SetBool("Despertar", true);
            }
        }
        else
        {
            if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) > radioPersecucion
                || !perimetro.bounds.Contains(getObjetivoPerseguir().transform.position))
            {
                if (Vector3.Distance(getPosicionMapa().transform.position, gameObject.transform.position) > radioAtaque)
                {
                    Debug.Log(Vector3.Distance(getPosicionMapa().transform.position, gameObject.transform.position));
                    if (getEstadoEnemigo() == estadoGenerico.caminando
                       || getEstadoEnemigo() == estadoGenerico.durmiendo
                       || getEstadoEnemigo() == estadoGenerico.ninguno)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position, getPosicionMapa().transform.position,
                        getVelocidadMovimientoEnemigo() * Time.fixedDeltaTime);
                        Vector3 refAnimacion = getPosicionMapa().transform.position - vectorTemporal;
                        Vector3 vectorMovimiento = cambiaAnimaciones(refAnimacion);
                        getEnemigoRigidBody().MovePosition(gameObject.transform.position + vectorMovimiento * getVelocidadMovimientoEnemigo() * Time.fixedDeltaTime);
                        setEstadoEnemigo(estadoGenerico.caminando);
                        getEnemigoAnimator().SetBool("Despertar", true);
                    }
                }
                else 
                {
                    getEnemigoAnimator().SetBool("Despertar", false);
                    setEstadoEnemigo(estadoGenerico.durmiendo);
                }
            }
        }
    }

}
