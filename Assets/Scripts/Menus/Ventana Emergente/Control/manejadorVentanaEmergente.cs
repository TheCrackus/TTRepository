using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manejadorVentanaEmergente : manejadorMenu, pulsoBoton, reproduceAudio
{

    private bool pulseBoton;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }

    void Start()
    {
        reproduceAudioAbreVentana();
        reiniciaBotones();
    }

    public void enviaTexto(string texto) 
    {
        ((componentesGraficosVentanaEmergente)Graficos).TextoVentanaEmergente.text = texto;
    }

    public void botonCierraVentanaEmergente() 
    {
        if (!pulseBoton) 
        {
            reproduceAudioClickCerrar();
            pulseBoton = true;
            cierraVentanaEmergente();
        }
    }

    public void cierraVentanaEmergente() 
    {
        if (((componentesGraficosVentanaEmergente)Graficos).ComponenteGraficoPrincipal != null) 
        {
            Destroy(((componentesGraficosVentanaEmergente)Graficos).ComponenteGraficoPrincipal);
        }
    }

    public void reiniciaBotones()
    {
        pulseBoton = false;
    }

    public void reproduceAudioClickCerrar()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
        }
    }

    public void reproduceAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
        }
    }

    public void reproduceAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioAbrirVentana();
        }
    }
}
