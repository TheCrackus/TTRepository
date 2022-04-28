using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaccionesEmpujeGlobales : MonoBehaviour
{
    private float contadorEntreGolpes;
    private bool puedoGolpear;
    [Header("Estadisticas globales para atacar")]
    public float fuerza;
    public float tiempoAplicarFuerza;
    public float vidaRestar;
    [Header("Tiempo antes de volver a golpear")]
    public float tiempoEntreGolpes;

    void Start()
    {
        puedoGolpear = true;
        contadorEntreGolpes = tiempoEntreGolpes;    
    }

    void Update()
    {
        if (!puedoGolpear) 
        {
            contadorEntreGolpes -= Time.deltaTime;
            if (contadorEntreGolpes <= 0)
            {
                puedoGolpear = true;
                contadorEntreGolpes = tiempoEntreGolpes;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (puedoGolpear) 
        {
            if (colisionDetectada.gameObject.CompareTag("Rompible")
            && gameObject.CompareTag("ArmaObjetoPlayer"))
            {
                colisionDetectada.GetComponent<jarro>().Romper();
                puedoGolpear = false;
            }
            else
            {
                if ((colisionDetectada.gameObject.CompareTag("Enemigo")
                    && gameObject.CompareTag("ArmaObjetoPlayer"))
                    ||
                    (colisionDetectada.gameObject.CompareTag("Player")
                    && gameObject.CompareTag("Enemigo"))
                    ||
                    (colisionDetectada.gameObject.CompareTag("Player")
                    && gameObject.CompareTag("ProyectilEnemigo"))
                    ||
                    (colisionDetectada.gameObject.CompareTag("Player")
                    && gameObject.CompareTag("ArmaObjetoEnemigo")))
                {
                    Rigidbody2D rigidBodyAfectado = colisionDetectada.gameObject.GetComponent<Rigidbody2D>();
                    if (rigidBodyAfectado != null)
                    {
                        if (colisionDetectada.gameObject.CompareTag("Enemigo")
                            && gameObject.CompareTag("ArmaObjetoPlayer")
                            && colisionDetectada.isTrigger)
                        {
                            colisionDetectada.gameObject.GetComponent<enemigo>().comienzaEmpujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza, vidaRestar);
                            puedoGolpear = false;
                        }
                        else
                        {
                            if (colisionDetectada.gameObject.CompareTag("Player")
                                && gameObject.CompareTag("Enemigo")
                                && colisionDetectada.isTrigger)
                            {
                                gameObject.GetComponent<enemigo>().setPuedoMoverme(false);
                                colisionDetectada.gameObject.GetComponent<movimientoPlayer>().comienzaEmpujaPlayer(tiempoAplicarFuerza, vidaRestar);
                                puedoGolpear = false;
                            }
                            else
                            {
                                if (colisionDetectada.gameObject.CompareTag("Player")
                                    && gameObject.CompareTag("ProyectilEnemigo")
                                    && colisionDetectada.isTrigger)
                                {
                                    colisionDetectada.gameObject.GetComponent<movimientoPlayer>().comienzaEmpujaPlayer(tiempoAplicarFuerza, vidaRestar);
                                    puedoGolpear = false;
                                }
                                else 
                                {
                                    if (colisionDetectada.gameObject.CompareTag("Player")
                                        && gameObject.CompareTag("ArmaObjetoEnemigo")
                                        && colisionDetectada.isTrigger)
                                    {
                                        gameObject.GetComponentInParent<enemigo>().setPuedoMoverme(false);
                                        colisionDetectada.gameObject.GetComponent<movimientoPlayer>().comienzaEmpujaPlayer(tiempoAplicarFuerza, vidaRestar);
                                        puedoGolpear = false;
                                    }
                                }
                            }
                        }
                        Vector2 diferencia = rigidBodyAfectado.transform.position - gameObject.transform.position;
                        Vector2 direccion = diferencia.normalized * fuerza;
                        rigidBodyAfectado.AddForce(direccion, ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
}