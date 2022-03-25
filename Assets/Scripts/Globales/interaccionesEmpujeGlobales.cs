using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaccionesEmpujeGlobales : MonoBehaviour
{

    public float fuerza;
    public float tiempoAplicarFuerza;

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
        if (colisionDetectada.gameObject.CompareTag("Rompible") && this.gameObject.CompareTag("Player"))
        {
            colisionDetectada.GetComponent<jarro>().Romper();
        }
        else
        {
            if ( (colisionDetectada.gameObject.CompareTag("Enemigo") && this.gameObject.CompareTag("Player"))
                || (colisionDetectada.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Enemigo")))
            {
                Rigidbody2D rigidBodyAfectado = colisionDetectada.gameObject.GetComponent<Rigidbody2D>();
                if (rigidBodyAfectado != null)
                {
                    Vector2 diferencia = rigidBodyAfectado.transform.position - transform.position;
                    diferencia = diferencia.normalized * fuerza;
                    rigidBodyAfectado.AddForce(diferencia, ForceMode2D.Impulse);
                    if (colisionDetectada.gameObject.CompareTag("Enemigo"))
                    {
                        colisionDetectada.gameObject.GetComponent<enemigo>().activarEnemigoEstuneado();
                        colisionDetectada.gameObject.GetComponent<enemigo>().empuja(rigidBodyAfectado, tiempoAplicarFuerza);
                    }
                    else
                    {
                        if (colisionDetectada.gameObject.CompareTag("Player"))
                        {
                            colisionDetectada.gameObject.GetComponent<movimientoPlayer>().activarPlayerEstuneado();
                            colisionDetectada.gameObject.GetComponent<movimientoPlayer>().empuja(rigidBodyAfectado, tiempoAplicarFuerza);
                        }
                    }
                }
            }
        }
    }
}