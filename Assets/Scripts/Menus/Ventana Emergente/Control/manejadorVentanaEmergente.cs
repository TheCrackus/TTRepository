using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorVentanaEmergente : ManejadorMenuGenerico, IReproductorAudioInterfazGrafica
{

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private AudioInterfazGrafica manejadorAudioInterfazGrafica;

    public AudioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }

    void Start()
    {
        reproducirAudioAbreVentana();
    }

    public void enviarTextoVentanaEmergente(string texto) 
    {
        ((ComponenteGraficoVentanaEmergente)ComponenteGrafico).TextoVentanaEmergente.text = texto;
    }

    public void cerrarVentanaEmergenteBoton() 
    {
        reproducirAudioClickCerrar();
        cerrarVentanaEmergente();
    }

    public void cerrarVentanaEmergente() 
    {
        if (((ComponenteGraficoVentanaEmergente)ComponenteGrafico).ComponenteGraficoPrincipal != null) 
        {
            Destroy(((ComponenteGraficoVentanaEmergente)ComponenteGrafico).ComponenteGraficoPrincipal);
        }
    }

    public void reproducirAudioClickCerrar()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
        }
    }

    public void reproducirAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
        }
    }

    public void reproducirAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproducirAudioAbrirVentana();
        }
    }
}
