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

    private EnemyState estadoActualEnemigo;
    public valorFlotante vidaMaxima;
    private float vidaEnemigo;
    public string nombreEnemigo;
    public int puntosAtaqueEnemigo;
    public float velocidadMovimientoEnemigo;

    private void Awake()
    {
        vidaEnemigo = vidaMaxima.valorInicial;
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

    public void empuja(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza, float vidaMenos) 
    {
        tomaMenosVida(vidaMenos);
        if (estadoActualEnemigo != EnemyState.inactivo
            && estadoActualEnemigo != EnemyState.estuneado
            && estadoActualEnemigo != EnemyState.atacando
            && (estadoActualEnemigo == EnemyState.caminando 
                || estadoActualEnemigo == EnemyState.ninguno 
                || estadoActualEnemigo == EnemyState.durmiendo)) 
        {
            estadoActualEnemigo = EnemyState.estuneado;
            StartCoroutine(empujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza));
        }
    }

    private void tomaMenosVida(float vidaMenos)
    {
        vidaEnemigo -= vidaMenos;
        if (vidaEnemigo <= 0)
        {
            estadoActualEnemigo = EnemyState.inactivo;
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
