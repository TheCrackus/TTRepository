using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState 
{
    ninguno,
    caminando,
    atacando,
    estuneado
}

public class enemigo : MonoBehaviour
{

    private EnemyState estadoActualEnemigo;
    public int vidaEnemigo;
    public string nombreEnemigo;
    public int puntosAtaqueEnemigo;
    public float velocidadMovimientoEnemigo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void activarEnemigoEstuneado() 
    {
        estadoActualEnemigo = EnemyState.estuneado;   
    }

    public void activarEnemigoNinguno()
    {
        estadoActualEnemigo = EnemyState.ninguno;
    }

    public void cambiarEstado(EnemyState nuevoEstado)
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
}
