using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectil : MonoBehaviour
{

    private float tiempoVidaSegundos;
    [Header("La velocidad del proyectil")]
    public float velocidadMovimiento;
    [Header("El tiempo que el proyectil existe")]
    public float tiempoVida;
    [Header("El RigidBody que pertenece al proyectil")]
    public Rigidbody2D proyectilRigidBody;

    void Start()
    {
        tiempoVidaSegundos = tiempoVida;
    }

    void Update()
    {
        tiempoVidaSegundos -= Time.deltaTime;
        if (tiempoVidaSegundos <= 0) 
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
        if (!colisionDetectada.gameObject.CompareTag("Enemigo"))
        {
            Destroy(gameObject);
        }
    }
}
