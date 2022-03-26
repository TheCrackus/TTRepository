using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState 
{
    ninguno,
    caminando,
    atacando,
    estuneado,
    durmiendo
}

public class enemigo : MonoBehaviour
{

    private EnemyState estadoActualEnemigo;
    private valorFlotante vidaMaxima;
    private float vidaEnemigo;
    public string nombreEnemigo;
    public int puntosAtaqueEnemigo;
    public float velocidadMovimientoEnemigo;

    // Start is called before the first frame update
    void Start()
    {
        vidaEnemigo = vidaMaxima.valorInicial;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setEstadoActualEnemigo(EnemyState nuevoEstado)
    {
        if(estadoActualEnemigo != nuevoEstado)
        {
            estadoActualEnemigo = nuevoEstado;
        }
    }

    public EnemyState getEstadoActualEnemigo() 
    {
        return estadoActualEnemigo;    
    }

    public void empuja(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza) 
    {
        estadoActualEnemigo = EnemyState.estuneado;
        StartCoroutine(empujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza));
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

    public void espera(float tiempoEspera) 
    {
        estadoActualEnemigo = EnemyState.atacando;
        StartCoroutine(esperaMovimiento(tiempoEspera));
    }

    private IEnumerator esperaMovimiento(float tiempoEspera)
    {
           yield return new WaitForSeconds(tiempoEspera);

           estadoActualEnemigo = EnemyState.ninguno;
    }
}
