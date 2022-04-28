using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoArea : trepaCielosCorrupto
{
    [Header("Limite de persecucion")]
    public Collider2D perimetro;

    public override void gestionDistancias()
    {
        if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) >= radioAtaque
            && perimetro.bounds.Contains(getObjetivoPerseguir().transform.position))
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
                getEnemigoAnimator().SetBool("Despertar", true);
            }
        }
        else
        {
            if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) > radioPersecucion
                || !perimetro.bounds.Contains(getObjetivoPerseguir().transform.position))
            {
                getEnemigoAnimator().SetBool("Despertar", false);
                setEstadoActualEnemigo(EnemyState.durmiendo);
            }
        }
    }

}
