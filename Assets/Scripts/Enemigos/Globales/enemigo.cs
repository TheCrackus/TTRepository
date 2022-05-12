using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    [Header("El estado en el que se encuentra el enemigo")]
    [SerializeField] private estadoObjeto estadoEnemigo;
    [Header("Contador auxiliar para volver al movimiento")]
    [SerializeField] private float contadorEsperaMovimiento;
    [Header("Puedo volver al movimiento?")]
    [SerializeField] private bool puedoMoverme;
    [Header("Nombre del enemigo")]
    [SerializeField] private string nombreEnemigo;
    [Header("Velocidad del enemigo")]
    [SerializeField] private float velocidadMovimientoEnemigo;
    [Header("Posicion por defecto del enemigo")]
    [SerializeField] private Vector3 posicionOriginal;
    [Header("Tiempo para mover despues de atacar")]
    [SerializeField] private float tiempoEsperaMovimiento;
    [Header("GameObject para la posicion de este objeto")]
    [SerializeField] private GameObject posicionMapa;

    public void setEstadoEnemigo(estadoGenerico estado)
    {
        this.estadoEnemigo.setEstadoActualObjeto(estado);
    }

    public estadoGenerico getEstadoEnemigo()
    {
        return estadoEnemigo.getEstadoActualObjeto();
    }

    public void setPuedoMoverme(bool puedoMoverme)
    {
        this.puedoMoverme = puedoMoverme;
    }

    public bool getPuedoMoverme()
    {
        return puedoMoverme;
    }

    public void setContadorEsperaMovimiento(float contadorEsperaMovimiento)
    {
        this.contadorEsperaMovimiento = contadorEsperaMovimiento;
    }

    public float getContadorEsperaMovimiento()
    {
        return contadorEsperaMovimiento;
    }

    public void setPosicionOriginal(Vector3 posicionOriginal)
    {
        this.posicionOriginal = posicionOriginal;
    }

    public Vector3 getPosicionOriginal()
    {
        return posicionOriginal;
    }

    public void setTiempoEsperaMovimiento(float tiempoEsperaMovimiento)
    {
        this.tiempoEsperaMovimiento = tiempoEsperaMovimiento;
    }

    public float getTiempoEsperaMovimiento()
    {
        return tiempoEsperaMovimiento;
    }

    public void setVelocidadMovimientoEnemigo(float velocidadMovimientoEnemigo)
    {
        this.velocidadMovimientoEnemigo = velocidadMovimientoEnemigo;
    }

    public float getVelocidadMovimientoEnemigo()
    {
        return velocidadMovimientoEnemigo;
    }

    public void setPosicionMapa(GameObject posicionMapa)
    {
        this.posicionMapa = posicionMapa;
    }

    public GameObject getPosicionMapa() 
    {
        return posicionMapa;
    }

    public virtual void Awake()
    {
        posicionOriginal = posicionMapa.gameObject.transform.position;
    }

    public virtual void OnEnable()
    {
        posicionMapa.transform.position = posicionOriginal;
    }

    public virtual void Start()
    {
        contadorEsperaMovimiento = tiempoEsperaMovimiento;
        puedoMoverme = true;
    }

    public virtual void Update()
    {
        if (!puedoMoverme)
        {
            contadorEsperaMovimiento -= Time.deltaTime;
            if (contadorEsperaMovimiento <= 0)
            {
                puedoMoverme = true;
                contadorEsperaMovimiento = tiempoEsperaMovimiento;
                estadoEnemigo.setEstadoActualObjeto(estadoGenerico.ninguno);
            }
        }
    }

    public void comienzaEmpujaEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza) 
    {
        estadoEnemigo.setEstadoActualObjeto(estadoGenerico.estuneado);
        StartCoroutine(empujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza));
    }

    private IEnumerator empujaEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza)
    {
        if (rigidBodyAfectado != null)
        {
            yield return new WaitForSeconds(tiempoAplicarFuerza);
            rigidBodyAfectado.velocity = Vector2.zero;
            estadoEnemigo.setEstadoActualObjeto(estadoGenerico.ninguno);
        }
    }
    
}
