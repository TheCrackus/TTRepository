using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaccionesEmpujeGlobales : MonoBehaviour
{
    [Header("Estadisticas globales para atacar")]
    public float fuerza;
    public float tiempoAplicarFuerza;
    public float vidaRestar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Rompible") 
            && gameObject.CompareTag("ArmaObjetoPlayer"))
        {
            colisionDetectada.GetComponent<jarro>().Romper();
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
                && gameObject.CompareTag("ProyectilEnemigo")))
            {
                Rigidbody2D rigidBodyAfectado = colisionDetectada.gameObject.GetComponent<Rigidbody2D>();
                if (rigidBodyAfectado != null)
                {
                    Vector2 diferencia = rigidBodyAfectado.transform.position - gameObject.transform.position;
                    Vector2 direccion = diferencia.normalized * fuerza;
                    rigidBodyAfectado.AddForce(direccion, ForceMode2D.Impulse);
                    if (colisionDetectada.gameObject.CompareTag("Enemigo") 
                        && gameObject.CompareTag("ArmaObjetoPlayer")
                        && colisionDetectada.isTrigger)
                    {
                        colisionDetectada.gameObject.GetComponent<enemigo>().empiezaEmpujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza, vidaRestar);
                    }
                    else
                    {
                        if (colisionDetectada.gameObject.CompareTag("Player")
                            && gameObject.CompareTag("Enemigo")
                            && colisionDetectada.isTrigger)
                        {

                            colisionDetectada.gameObject.GetComponent<movimientoPlayer>().comienzaEmpujaPlayer(rigidBodyAfectado, tiempoAplicarFuerza, vidaRestar);
                        }
                        else 
                        {
                            if (colisionDetectada.gameObject.CompareTag("Player")
                                && gameObject.CompareTag("ProyectilEnemigo")
                                && colisionDetectada.isTrigger) 
                            {
                                colisionDetectada.gameObject.GetComponent<movimientoPlayer>().comienzaEmpujaPlayer(rigidBodyAfectado, tiempoAplicarFuerza, vidaRestar);
                            }    
                        }
                    }
                }
            }
        }
    }
}