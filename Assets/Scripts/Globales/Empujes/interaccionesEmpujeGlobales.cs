using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaccionesEmpujeGlobales : MonoBehaviour
{

    public float fuerza;
    public float tiempoAplicarFuerza;
    public float vidaMenos;

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
                && gameObject.CompareTag("Enemigo")))
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
                        colisionDetectada.gameObject.GetComponent<enemigo>().empuja(rigidBodyAfectado, tiempoAplicarFuerza, vidaMenos);
                    }
                    else
                    {
                        if (colisionDetectada.gameObject.CompareTag("Player") 
                            && gameObject.CompareTag("Enemigo")
                            && colisionDetectada.isTrigger)
                        {

                            colisionDetectada.gameObject.GetComponent<movimientoPlayer>().empuja(rigidBodyAfectado, tiempoAplicarFuerza, vidaMenos);
                            //gameObject.GetComponent<enemigo>().espera(tiempoAplicarFuerza);
                        }
                    }
                }
            }
        }
    }
}