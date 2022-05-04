using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectil : MonoBehaviour
{

    private float contadorVidaProyectil;
    [Header("La velocidad del proyectil")]
    public float velocidadMovimiento;
    [Header("El tiempo que el proyectil existe")]
    public float tiempoVidaProyectil;
    [Header("El RigidBody que pertenece al proyectil")]
    public Rigidbody2D proyectilRigidBody;

    void Start()
    {
        contadorVidaProyectil = tiempoVidaProyectil;
    }

    void Update()
    {
        contadorVidaProyectil -= Time.deltaTime;
        if (contadorVidaProyectil <= 0) 
        {
            Destroy(gameObject);
        }
    }

    public void arroja(Vector2 direccionInicial) 
    {
        proyectilRigidBody.velocity = direccionInicial * velocidadMovimiento;
    }

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada) 
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger)
        {
            Destroy(gameObject);
        }
        else 
        {
            if (!colisionDetectada.gameObject.CompareTag("Enemigo")
                && !colisionDetectada.isTrigger)
            {
                Destroy(gameObject);
            }
        }
    }
}
