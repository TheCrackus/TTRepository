using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState 
{
    ninguno,
    caminando,
    atacando,
    estuneado,
    durmiendo,
    inactivo
}

public class enemigo : MonoBehaviour
{
    public EnemyState estadoActualEnemigo;
    private float vidaEnemigo;
    private float contadorEsperaMovimiento;
    private bool puedoMoverme;
    [Header("Vida del enemigo")]
    public valorFlotante vidaMaxima;
    [Header("Nombre del enemigo")]
    public string nombreEnemigo;
    [Header("Velocidad del enemigo")]
    public float velocidadMovimientoEnemigo;
    [Header("Posicion por defecto del enemigo")]
    public Vector3 posicionOriginal;
    [Header("Objeto contenedor de efecto")]
    public GameObject efectoMuerteEnemigo;
    [Header("Clip de muerte del enemigo")]
    public AnimationClip muerteEnemigoClip;
    [Header("Evento para cuartos con enemigos (abre puerta)")]
    public evento estadoEnemigosCuarto;
    [Header("Objetos que dejara al morir")]
    public tablaLoot miLoot;
    [Header("Tiempo para mover despues de atacar")]
    public float tiempoEsperaMovimiento;
    [Header("GameObject para la posicion de este objeto")]
    public GameObject miPosicionMapa;

    public void setEstadoActualEnemigo(EnemyState estadoActualEnemigo)
    {
        if (this.estadoActualEnemigo != estadoActualEnemigo)
        {
            this.estadoActualEnemigo = estadoActualEnemigo;
        }
    }

    public EnemyState getEstadoActualEnemigo()
    {
        return estadoActualEnemigo;
    }

    public void setVidaEnemigo(float vidaEnemigo) 
    {
        this.vidaEnemigo = vidaEnemigo;
    }

    public float getVidaEnemigo() 
    {
        return vidaEnemigo;
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
                estadoActualEnemigo = EnemyState.ninguno;
            }
        }
    }

    public virtual void Awake()
    {
        vidaEnemigo = vidaMaxima.valorFlotanteInicial;
    }

    public virtual void OnEnable()
    {
        miPosicionMapa.transform.position = posicionOriginal;
        vidaEnemigo = vidaMaxima.valorFlotanteInicial;

    }

    public void comienzaEmpujaEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza, float vidaMenos) 
    {
        estadoActualEnemigo = EnemyState.estuneado;
        StartCoroutine(empujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza));
        tomaMenosVida(vidaMenos);
    }

    private void tomaMenosVida(float vidaMenos)
    {
        vidaEnemigo -= vidaMenos;
        if (vidaEnemigo <= 0)
        {
            muerteEnemigoAnimacion();
            procesaLoot();
            estadoActualEnemigo = EnemyState.inactivo;
            if (estadoEnemigosCuarto != null) 
            {
                estadoEnemigosCuarto.invocaFunciones();
            }
            gameObject.SetActive(false);
        }
    }

    private IEnumerator empujaEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza)
    {
        if (rigidBodyAfectado != null)
        {
            yield return new WaitForSeconds(tiempoAplicarFuerza);
            rigidBodyAfectado.velocity = Vector2.zero;
            estadoActualEnemigo = EnemyState.ninguno;
        }
    }

    private void muerteEnemigoAnimacion() 
    {
        if (efectoMuerteEnemigo != null) 
        {
            GameObject efecto = Instantiate(efectoMuerteEnemigo, gameObject.transform.position, Quaternion.identity);
            Destroy(efecto, muerteEnemigoClip.length);
        }
    }

    private void procesaLoot() 
    {
        if (miLoot != null) 
        {
            incrementoEstadisticas incrementoActual = miLoot.lootIncrementoEstadisticas();
            if (incrementoActual != null) 
            {
                Instantiate(incrementoActual.gameObject, gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
