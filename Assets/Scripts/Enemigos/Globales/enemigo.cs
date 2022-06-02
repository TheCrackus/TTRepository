using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    private float contadorEsperaMovimiento;

    private bool puedoMoverme;

    private Vector3 posicionOriginal;

    [Header("El estado en el que se encuentra el enemigo")]
    [SerializeField] private EstadoObjeto estadoEnemigo;

    [Header("Nombre del enemigo")]
    [SerializeField] private string nombreEnemigo;

    [Header("Velocidad del enemigo")]
    [SerializeField] private float velocidadMovimientoEnemigo;

    [Header("Tiempo para mover despues de atacar")]
    [SerializeField] private float tiempoEsperaMovimientoAtaque;

    [Header("GameObject para la posicion de este objeto")]
    [SerializeField] private GameObject posicionMapa;

    public EstadoObjeto EstadoEnemigo { get => estadoEnemigo; set => estadoEnemigo = value; }
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
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>().EstadoPlayer.Estado == EstadoGenerico.transicionando) 
        {
            puedoMoverme = false;
        }
        if (!puedoMoverme
            && (GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>().EstadoPlayer.Estado != EstadoGenerico.transicionando))
        {
            contadorEsperaMovimiento -= Time.deltaTime;
            if (contadorEsperaMovimiento <= 0)
            {
                puedoMoverme = true;
                contadorEsperaMovimiento = tiempoEsperaMovimientoAtaque;
                estadoEnemigo.Estado = EstadoGenerico.ninguno;
            }
        }
    }

    public void iniciarEmpujaEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza) 
    {
        estadoEnemigo.Estado = EstadoGenerico.estuneado;
        StartCoroutine(empujarEnemigo(rigidBodyAfectado, tiempoAplicarFuerza));
    }

    private IEnumerator empujarEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza)
    {
        if (rigidBodyAfectado != null)
        {
            yield return new WaitForSeconds(tiempoAplicarFuerza);
            rigidBodyAfectado.velocity = Vector2.zero;
            estadoEnemigo.Estado = EstadoGenerico.ninguno;
        }
    }
    
}
