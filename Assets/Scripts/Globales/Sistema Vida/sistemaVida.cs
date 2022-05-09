using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sistemaVida : MonoBehaviour
{
    [Header("La vida que posee este objeto")]
    [SerializeField] private valorFlotante vidaMaxima;
    [Header("La vida actual del objeto")]
    [SerializeField] private float vidaActual;
    [Header("Objeto contenedor de efecto")]
    [SerializeField] private GameObject efectoMuerte;
    [Header("Clip de muerte del enemigo")]
    [SerializeField] private AnimationClip muerteClip;
    [Header("Objetos que dejara al morir")]
    [SerializeField] private tablaLoot miLoot;
    [Header("Objeto que posee esta vida")]
    [SerializeField] private GameObject objeto;

    public void setVidaMaxima(valorFlotante vidaMaxima) 
    {
        this.vidaMaxima = vidaMaxima;
    }

    public valorFlotante getVidaMaxima() 
    {
        return vidaMaxima;
    }

    public void setVidaActual(float vidaActual) 
    {
        this.vidaActual = vidaActual;
    }

    public float getVidaActual() 
    {
        return vidaActual;
    }

    public void setObjeto(GameObject objeto) 
    {
        this.objeto = objeto;
    }

    public GameObject getObjeto() 
    {
        return objeto;
    }

    private void OnEnable()
    {
        vidaActual = vidaMaxima.valorFlotanteEjecucion;
    }

    public virtual void agregaVida(float vidaExtra)
    {
        vidaActual += vidaExtra;
        if (vidaActual > vidaMaxima.valorFlotanteEjecucion)
        {
            vidaActual = vidaMaxima.valorFlotanteEjecucion;
        }
    }

    public virtual void vidaLlena()
    {
        vidaActual = vidaMaxima.valorFlotanteEjecucion;
    }

    public virtual void quitaVida(float vidaMenos) 
    {
        vidaActual -= vidaMenos;
        if (vidaActual <= 0)
        {
            vidaActual = 0;
        }
    }

    public virtual void muerte() 
    {
        vidaActual = 0;
    }

    public void muerteAnimacion()
    {
        if (efectoMuerte != null)
        {
            GameObject efecto = Instantiate(efectoMuerte, gameObject.transform.position, Quaternion.identity);
            Destroy(efecto, muerteClip.length);
        }
    }

    public void procesaLoot()
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
