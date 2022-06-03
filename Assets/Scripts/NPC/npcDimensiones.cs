using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcDimensiones : ControladorDialogos
{

    private Vector3 vectorDireccion;

    private Transform npcTransform;

    private Rigidbody2D npcRigidBody;

    private Animator npcAnimator;

    private float contadorMovimiento;

    private float contadorEspera;

    private bool enMovimiento;

    [Header("La velocidad con la que se mueve este objeto")]
    public float velocidad;

    [Header("La colision limite del NPC")]
    public Collider2D colisionLimite;

    [Header("Tiempo maximo en el que el NPC se movera")]
    public float tiempoMovimientoMaximo;

    [Header("Tiempo minimo en el que el NPC se movera")]
    public float tiempoMovimientoMinimo;

    [Header("Tiempo maximo en el que el NPC estara estatico")]
    public float tiempoEsperaMaximo;

    [Header("Tiempo minimo en el que el NPC estara estatico")]
    public float tiempoEsperaMinimo;


    void Start()
    {
        npcTransform = gameObject.GetComponent<Transform>();
        npcRigidBody = gameObject.GetComponent<Rigidbody2D>();
        npcAnimator = gameObject.GetComponent<Animator>();
        contadorMovimiento = Random.Range(tiempoMovimientoMinimo, tiempoMovimientoMaximo);
        contadorEspera = Random.Range(tiempoEsperaMinimo, tiempoEsperaMaximo);
        enMovimiento = true;
        cambiarDireccion();
    }

    public override void Update()
    {
        base.Update();
        if (enMovimiento)
        {
            contadorMovimiento -= Time.deltaTime;
            if (contadorMovimiento <= 0)
            {
                contadorMovimiento = Random.Range(tiempoMovimientoMinimo, tiempoMovimientoMaximo); ;
                enMovimiento = false;
            }
            if (!PlayerEnRango)
            {
                mover();
            }
        }
        else 
        {
            contadorEspera -= Time.deltaTime;
            if (contadorEspera <= 0)
            {
                elegirNuevaDireccion();
                contadorEspera = Random.Range(tiempoEsperaMinimo, tiempoEsperaMaximo);
                enMovimiento = true;
            }
        }        
    }

    private void mover() 
    {
        Vector3 vectorTemporal = npcTransform.position + vectorDireccion * velocidad * Time.fixedDeltaTime;
        if (colisionLimite.bounds.Contains(vectorTemporal)) 
        {
            npcRigidBody.MovePosition(vectorTemporal);
        }
        else
        {
            cambiarDireccion();    
        }
    }

    private void cambiarDireccion()
    {
        int direccion = Random.Range(0, 4);
        switch (direccion) 
        {
            case 0:
                vectorDireccion = Vector3.right;
                break;
            case 1:
                vectorDireccion = Vector3.up;
                break;
            case 2:
                vectorDireccion = Vector3.left;
                break;
            case 3:
                vectorDireccion = Vector3.down;
                break;
            default:
                break;
        }
        cambiaAnimaciones();
    }

    private void cambiaAnimaciones() 
    {
        npcAnimator.SetFloat("MovimientoX", vectorDireccion.x);
        npcAnimator.SetFloat("MovimientoY", vectorDireccion.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        elegirNuevaDireccion();
    }

    private void elegirNuevaDireccion() 
    {
        Vector3 vectorTemporal = vectorDireccion;
        cambiarDireccion();
        int ciclo = 0;
        while (vectorTemporal == vectorDireccion && ciclo < 100)
        {
            ciclo++;
            cambiarDireccion();
        }
    }

    public override void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        base.OnTriggerEnter2D(colisionDetectada);
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            npcRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public override void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        base.OnTriggerExit2D(colisionDetectada);
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            npcRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

}
