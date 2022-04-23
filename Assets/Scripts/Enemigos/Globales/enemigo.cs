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
    private float vidaEnemigo;
    [Header("Estadisticas del enemigo")]
    public valorFlotante vidaMaxima;
    public string nombreEnemigo;
    public float velocidadMovimientoEnemigo;
    [Header("Efectos visuales del enemigo")]
    public GameObject efectoMuerteEnemigo;
    public AnimationClip muerteEnemigoClip;

    void Awake()
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

    public void empiezaEmpujaEnemigo(Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza, float vidaMenos) 
    {
        if (estadoActualEnemigo != EnemyState.inactivo
            && estadoActualEnemigo != EnemyState.estuneado
            && estadoActualEnemigo != EnemyState.atacando
            && (estadoActualEnemigo == EnemyState.caminando 
                || estadoActualEnemigo == EnemyState.ninguno 
                || estadoActualEnemigo == EnemyState.durmiendo)) 
        {
            tomaMenosVida(vidaMenos, rigidBodyAfectado, tiempoAplicarFuerza);
        }
    }

    private void tomaMenosVida(float vidaMenos, Rigidbody2D rigidBodyAfectado, float tiempoAplicarFuerza)
    {
        vidaEnemigo -= vidaMenos;
        if (vidaEnemigo <= 0)
        {
            muerteEnemigoAnimacion();
            estadoActualEnemigo = EnemyState.inactivo;
            gameObject.SetActive(false);
        }
        else 
        {
            estadoActualEnemigo = EnemyState.estuneado;
            StartCoroutine(empujaEnemigo(rigidBodyAfectado, tiempoAplicarFuerza));
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
            GameObject efecto = GameObject.Instantiate(efectoMuerteEnemigo, gameObject.transform.position, Quaternion.identity);
            Destroy(efecto, muerteEnemigoClip.length);
        }
    }
}
