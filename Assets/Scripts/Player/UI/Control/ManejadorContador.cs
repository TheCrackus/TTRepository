using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorContador : ManejadorMenuGenerico
{

    private ComponenteGraficoContador graficos;

    [Header("Duracion del contador")]
    [SerializeField] private ValorFlotante tiempoContadorRegresivo;

    [Header("Manejador de la transicion")]
    [SerializeField] private MoverEscena transicion;

    [Header("El contador regresivo esta en ejecucion?")]
    [SerializeField] private ValorBooleano cuentaTimerRegresivo;

    private void Awake()
    {
        graficos = (ComponenteGraficoContador) ComponenteGrafico;
        if (cuentaTimerRegresivo != null) 
        {
            if (cuentaTimerRegresivo.valorBooleanoEjecucion)
            {
                iniciarContadorRegresivo();
            }
        }
    }

    private void Update()
    {
        if (cuentaTimerRegresivo != null) 
        {
            if (cuentaTimerRegresivo.valorBooleanoEjecucion) 
            {
                if (tiempoContadorRegresivo.valorFlotanteEjecucion > 0)
                {
                    tiempoContadorRegresivo.valorFlotanteEjecucion -= Time.deltaTime;
                    mostrarTiempo();
                }
                else
                {
                    transicion.iniciarTransicionOut();
                    detenerContadorRegresivo();
                }
            }
        }
    }

    public void mostrarTiempo()
    {
        int minutos = Mathf.FloorToInt(tiempoContadorRegresivo.valorFlotanteEjecucion / 60f);
        int segundos = Mathf.FloorToInt(tiempoContadorRegresivo.valorFlotanteEjecucion - minutos * 60f);
        graficos.TextoContador.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void detenerContadorRegresivo() 
    {
        if (cuentaTimerRegresivo != null) 
        {
            cuentaTimerRegresivo.valorBooleanoEjecucion = false;
        }
        graficos.ObjetoTextoContador.SetActive(false);
    }

    public void iniciarContadorRegresivo() 
    {
        if (cuentaTimerRegresivo != null)
        {
            cuentaTimerRegresivo.valorBooleanoEjecucion = true;
        }
        graficos.ObjetoTextoContador.SetActive(true);
    }

    public void reiniciarContadorRegresivo() 
    {
        if (cuentaTimerRegresivo != null)
        {
            cuentaTimerRegresivo.valorBooleanoEjecucion = false;
        }
        graficos.ObjetoTextoContador.SetActive(false);
        tiempoContadorRegresivo.valorFlotanteEjecucion = tiempoContadorRegresivo.valorFlotanteInicial;
    }
}
