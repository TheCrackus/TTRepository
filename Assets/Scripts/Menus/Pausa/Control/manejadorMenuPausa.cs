using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorMenuPausa : manejadorMenu, reproduceAudio, ejecutaPausa, pulsoBoton
{

    private bool pausa;

    private bool pulseBoton;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private valorString escenaMenuPrincipal;

    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    [SerializeField] private cambioEscena estadoCambioEscena;

    [Header("Componentes graficos del menu de pausa")]
    [SerializeField] private componentesGraficosMenusPausa graficosPausa;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    public bool Pausa { get => pausa; set => pausa = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public bool PulseBoton { get => pulseBoton; set => pulseBoton= value; }

    void OnEnable()
    {
        reproduceAudioAbreVentana();
        reiniciaBotones();
        pausaJuego();
    }

    void OnDisable()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        reproduceAudioClickCerrar();
        continuaJuego();
    }

    public void iniciaMenuConfiguraciones()
    {

    }

    public void botonRegresar() 
    {
        if (!pulseBoton) 
        {

            pulseBoton = true;
        }
    }

    public void botonInventario()
    {
        if (!pulseBoton)
        {

            pulseBoton = true;
        }
    }

    public void botonConfiguraciones()
    {
        if (!pulseBoton)
        {

            pulseBoton = true;
        }
    }

    public void botonMenuPrincipal()
    {
        if (!pulseBoton)
        {

            pulseBoton = true;
        }
    }

    public void botonReiniciaGuardado()
    {
        if (!pulseBoton)
        {

            pulseBoton = true;
        }
    }

    public void cierraMenu() 
    {
        cierraGrafico();
    }

    public void pausaJuego()
    {
        Time.timeScale = 0f;
    }

    public void continuaJuego()
    {
        Time.timeScale = 1f;
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

    public void reiniciaBotones()
    {
        pulseBoton = false;
    }
}
