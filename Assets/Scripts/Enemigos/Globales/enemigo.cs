using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{

    private float contadorEsperaMovimiento;

    private bool puedoMoverme;

    private Vector3 posicionOriginal;

    [Header("El estado en el que se encuentra el enemigo")]
    [SerializeField] private estadoObjeto estadoEnemigo;

    [Header("Nombre del enemigo")]
    [SerializeField] private string nombreEnemigo;

    [Header("Velocidad del enemigo")]
    [SerializeField] private float velocidadMovimientoEnemigo;

    [Header("Tiempo para mover despues de atacar")]
    [SerializeField] private float tiempoEsperaMovimientoAtaque;

    [Header("GameObject para la posicion de este objeto")]
    [SerializeField] private GameObject posicionMapa;

    public estadoObjeto EstadoEnemigo { get => estadoEnemigo; set => estadoEnemigo = value; }
    public float ContadorEsperaMovimiento { get => contadorEsperaMovimiento; set => contadorEsperaMovimiento = value; }
    public bool PuedoMoverme { get => puedoMoverme; set => puedoMoverme = value; }
    public float VelocidadMovimientoEnemigo { get => velocidadMovimientoEnemigo; set => velocidadMovimientoEnemigo = value; }
    public Vector3 PosicionOriginal { get => posicionOriginal; set => posicionOriginal = value; }
    public float TiempoEsperaMovimientoAtaque { get => tiempoEsperaMovimientoAtaque; set => tiempoEsperaMovimientoAtaque = value; }
    public GameObject PosicionMapa { get => posicionMapa; set => posicionMapa = value; }

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
        contadorEsperaMovimiento = tiempoEsperaMovimientoAtaque;
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
                contadorEsperaMovimiento = tiempoEsperaMovimientoAtaque;
                estadoEnemigo.Estado = estadoGenerico.ninguno;
            }
        }
    }

    public void comienzaEmpujaEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza) 
    {
        estadoEnemigo.Estado = estadoGenerico.estuneado;
        StartCoroutine(empujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza));
    }

    private IEnumerator empujaEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza)
    {
        if (rigidBodyAfectado != null)
        {
            yield return new WaitForSeconds(tiempoAplicarFuerza);
            rigidBodyAfectado.velocity = Vector2.zero;
            estadoEnemigo.Estado = estadoGenerico.ninguno;
        }
    }
    
}
