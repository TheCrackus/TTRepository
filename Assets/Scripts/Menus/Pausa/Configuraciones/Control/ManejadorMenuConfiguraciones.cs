using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorMenuConfiguraciones : ManejadorMenuGenerico, IBotonPulso, IReproductorAudio, ICanvasMenuPrincipal
{

    private string nombreEscenaActual;

    private ComponenteGraficoMenuConfiguracion graficos;

    private bool pulseBoton;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Nombre de la escena del menu principal")]
    [SerializeField] private valorString nombreEscenaPrincipal;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }


    private void Start()
    {
        graficos = (ComponenteGraficoMenuConfiguracion) ComponenteGrafico;
    }

    public void regresarBoton() 
    {
        if (!pulseBoton) 
        {
            nombreEscenaActual = SceneManager.GetActiveScene().name;
            if (nombreEscenaActual == nombreEscenaPrincipal.valorStringEjecucion)
            {
                reproducirAudioClickCerrar();
                iniciarCanvasMenuPrincipal();
                bloquearBotones();
                cerrarGrafico();
            }
            else 
            {
                reproducirAudioClickCerrar();
                bloquearBotones();
                cerrarGrafico();
            }
        }
    }

    public void reproducirAudioClickCerrar()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
        }
    }

    public void reproducirAudioClickAbrir()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
        }
    }

    public void reproducirAudioAbreVentana()
    {
        if (manejadorAudioInterfazGrafica != null)
        {
            manejadorAudioInterfazGrafica.reproduceAudioAbrirVentana();
        }
    }

    public void reiniciarBotones()
    {
        pulseBoton = false;
    }

    public void bloquearBotones()
    {
        pulseBoton = true;
    }

    public void iniciarCanvasMenuPrincipal()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasPrincipal"))
        {
            Instantiate(graficos.CanvasMenuPrincipal, Vector3.zero, Quaternion.identity);
        }
    }

}
