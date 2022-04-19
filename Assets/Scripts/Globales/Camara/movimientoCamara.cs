using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoCamara : MonoBehaviour
{

    public Transform objetivo;
    public float suavizado;
    private Vector3 posicionMaxima;
    private Vector3 posicionMinima;
    public cambioEscena estadoCambioEscena;

    void Start()
    {
        posicionMaxima = estadoCambioEscena.camaraPosicionMaximaEjecucion;
        posicionMinima = estadoCambioEscena.camaraPosicionMinimaEjecucion;
        gameObject.transform.position = estadoCambioEscena.camaraPosicionEjecucion;
    }

    void FixedUpdate() 
    {
        if (transform.position != objetivo.position) 
        {
            Vector3 posicionObjetivo = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);
            posicionObjetivo.x = Mathf.Clamp(posicionObjetivo.x, posicionMinima.x, posicionMaxima.x);
            posicionObjetivo.y = Mathf.Clamp(posicionObjetivo.y, posicionMinima.y, posicionMaxima.y);
            transform.position = Vector3.Lerp(transform.position, posicionObjetivo, suavizado);
        }
    }

    public void setPosicionMaxima(Vector3 posicionMaxima) 
    {
        this.posicionMaxima = posicionMaxima;
;   }

    public Vector3 getPosicionMaxima() 
    {
        return posicionMaxima;
    }

    public void setPosicionMinima(Vector3 posicionMinima)
    {
        this.posicionMinima = posicionMinima;
        ;
    }

    public Vector3 getPosicionMinima()
    {
        return posicionMinima;
    }
}
