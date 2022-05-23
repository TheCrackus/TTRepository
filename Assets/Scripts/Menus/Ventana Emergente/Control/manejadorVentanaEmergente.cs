using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class manejadorVentanaEmergente : MonoBehaviour
{

    private bool pulseBoton;

    [Header("Componentes graficos de la ventana emergente")]
    [SerializeField] private componentesGraficosVentanaEmergente graficosVentanaEmergente;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfaz manejadorAudioInterfaz;

    void Start()
    {
        manejadorAudioInterfaz.reproduceAudioAbrirVentana();
        pulseBoton = false;
    }

    public void enviaTexto(string texto) 
    {
        graficosVentanaEmergente.TextoVentanaEmergente.text = texto;
    }

    public void botonCierraVentanaEmergente() 
    {
        if (!pulseBoton) 
        {
            manejadorAudioInterfaz.reproduceAudioClickCerrar();
            pulseBoton = true;
            cierraVentanaEmergente();
        }
    }

    public void cierraVentanaEmergente() 
    {
        if (graficosVentanaEmergente.CanvasVentanaEmergente != null) 
        {
            Destroy(graficosVentanaEmergente.CanvasVentanaEmergente);
        }
    }

}
