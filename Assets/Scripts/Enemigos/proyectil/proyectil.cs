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

    public virtual void Start()
    {
        contadorVidaProyectil = tiempoVidaProyectil;
    }

    public virtual void Update()
    {
        contadorVidaProyectil -= Time.deltaTime;
        if (contadorVidaProyectil <= 0) 
        {
            Destroy(gameObject);
        }
    }

    public virtual void arroja(Vector2 direccionInicial) 
    {
        proyectilRigidBody.velocity = direccionInicial * velocidadMovimiento;
    }

    public virtual void dispara(Vector2 direccionInicial, Vector3 rotacion)
    {
        Debug.Log(" direccion: "+direccionInicial+" rotacion: "+rotacion);
        proyectilRigidBody.velocity = direccionInicial * velocidadMovimiento;
        gameObject.transform.rotation = Quaternion.Euler(rotacion);
    }
}
