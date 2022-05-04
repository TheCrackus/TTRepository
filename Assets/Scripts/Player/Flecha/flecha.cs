using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flecha : MonoBehaviour
{
    private float contadorVidaFlecha;
    [Header("Velocidad de movimiento de la flecha")]
    public float rapidez;
    [Header("Objeto para modificar las fisicas de la flecha")]
    public Rigidbody2D rigidbodyFlecha;
    [Header("Tiempo de existencia de la flecha")]
    public float tiempoVidaFlecha;
    [Header("Cantidad de magia que decrementa al jugador")]
    public float costoMagia;

    void Start()
    {
        contadorVidaFlecha = tiempoVidaFlecha;    
    }

    void Update()
    {
        contadorVidaFlecha -= Time.deltaTime;
        if (contadorVidaFlecha <= 0) 
        {
            Destroy(gameObject);
        }
    }

    public void setUp(Vector2 velocidad, Vector3 direccion) 
    {
        rigidbodyFlecha.velocity = velocidad.normalized * rapidez;
        gameObject.transform.rotation = Quaternion.Euler(direccion);
    }

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Enemigo")
            && colisionDetectada.isTrigger)
        {
            Destroy(gameObject);
        }
        else 
        {
            if (!colisionDetectada.gameObject.CompareTag("Player")
                && !colisionDetectada.isTrigger)
            {
                Destroy(gameObject);
            }
        }
    }

}
