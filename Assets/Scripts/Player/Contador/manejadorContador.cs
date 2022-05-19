using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class manejadorContador : MonoBehaviour
{
    private bool cuentaTimerRegresivo;

    [Header("Duracion del contador")]
    public valorFlotante tiempoContadorRegresivo;

    [Header("Contenedor del texto donde se mostrara el contador")]
    public GameObject objetoTextoContador;

    [Header("Texto donde se mostrara el contador")]
    public TextMeshProUGUI textoContador;

    [Header("Manejador de la transicion")]
    public moverEscena transicion;

    public void Update()
    {
        if (cuentaTimerRegresivo) 
        {
            if (tiempoContadorRegresivo.valorFlotanteEjecucion > 0)
            {
                tiempoContadorRegresivo.valorFlotanteEjecucion -= Time.deltaTime;
                muestraTiempo();
            }
            else 
            {
                transicion.iniciaTransicionOut();
                detenContadorRegresivo();
            }
        }
    }

    public void muestraTiempo()
    {
        int minutos = Mathf.FloorToInt(tiempoContadorRegresivo.valorFlotanteEjecucion / 60f);
        int segundos = Mathf.FloorToInt(tiempoContadorRegresivo.valorFlotanteEjecucion - minutos * 60f);
        textoContador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void detenContadorRegresivo() 
    {
        cuentaTimerRegresivo = false;
        objetoTextoContador.SetActive(false);
    }

    public void iniciaContadorRegresivo() 
    {
        cuentaTimerRegresivo = true;
        objetoTextoContador.SetActive(true);
    }

    public void reiniciaContadorRegresivo() 
    {
        cuentaTimerRegresivo = false;
        objetoTextoContador.SetActive(false);
        tiempoContadorRegresivo.valorFlotanteEjecucion = tiempoContadorRegresivo.valorFlotanteInicial;
    }
}
