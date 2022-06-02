using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class interaccionesEmpujeGlobales : MonoBehaviour
{

    [Header("Fuerza de empuje al objetivo")]
    [SerializeField] private float fuerza;
    [Header("Tiempo en el que la fuerza sera aplicada")]
    [SerializeField] private float tiempoAplicarFuerza;
    [Header("El objetivo para aplicar un empuje")]
    [SerializeField] private string colisionDetectadaTag;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag(colisionDetectadaTag)
            && colisionDetectada.isTrigger)
        {
            Rigidbody2D rigidBodyAfectado = colisionDetectada.gameObject.GetComponentInParent<Rigidbody2D>();
            if (rigidBodyAfectado != null) 
            {
                Vector3 diferencia = rigidBodyAfectado.transform.position - gameObject.transform.position;
                diferencia = diferencia.normalized * fuerza;
                rigidBodyAfectado.DOMove(rigidBodyAfectado.transform.position + diferencia, tiempoAplicarFuerza);
                if (gameObject.CompareTag("ArmaObjetoPlayer"))
                {
                    colisionDetectada.gameObject.GetComponentInParent<Enemigo>().iniciarEmpujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza);
                }
                else
                {
                    if (gameObject.CompareTag("Enemigo"))
                    {
                        gameObject.GetComponentInParent<Enemigo>().PuedoMoverme = false;
                        colisionDetectada.gameObject.GetComponentInParent<MovimientoPlayer>().comenzarEmpujaPlayer(tiempoAplicarFuerza);
                    }
                    else
                    {
                        if (gameObject.CompareTag("ProyectilEnemigo"))
                        {
                            colisionDetectada.gameObject.GetComponentInParent<MovimientoPlayer>().comenzarEmpujaPlayer(tiempoAplicarFuerza);
                        }
                        else
                        {
                            if (gameObject.CompareTag("ArmaObjetoEnemigo"))
                            {
                                gameObject.GetComponentInParent<Enemigo>().PuedoMoverme = false;
                                colisionDetectada.gameObject.GetComponentInParent<MovimientoPlayer>().comenzarEmpujaPlayer(tiempoAplicarFuerza);
                            }
                            else
                            {
                                if (gameObject.CompareTag("ProyectilPlayer"))
                                {
                                    colisionDetectada.gameObject.GetComponentInParent<Enemigo>().iniciarEmpujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}