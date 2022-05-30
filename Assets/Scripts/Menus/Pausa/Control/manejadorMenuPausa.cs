using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorMenuPausa : ManejadorMenuGenerico, IReproductorAudio, IPausa, IBotonPulso, ICanvasMenuInventario, ICanvasMenuConfiguraciones
{

    private ComponenteGraficoMenuPausa graficos;

    private bool condicionPausa;

    private bool pulseBoton;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private valorString nombreEscenaMenuPrincipal;

    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    [SerializeField] private cambioEscena estadoCambioEscena;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    public bool CondicionPausa { get => condicionPausa; set => condicionPausa = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public bool PulseBoton { get => pulseBoton; set => pulseBoton= value; }

    private void OnEnable()
    {
        graficos = (ComponenteGraficoMenuPausa)ComponenteGrafico;
        reproducirAudioAbreVentana();
        reiniciarBotones();
        pausarJuego();
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        reproducirAudioClickCerrar();
        continuarJuego();
    }

    public void regresarBoton() 
    {
        if (!pulseBoton) 
        {
            cerrarGrafico();
            bloquearBotones();
        }
    }

    public void abrirInventarioBoton()
    {
        if (!pulseBoton)
        {
            iniciarCanvasMenuInventario();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void abrirConfiguracionesBoton()
    {
        if (!pulseBoton)
        {
            iniciarCanvasMenuConfiguraciones();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void salirMenuPrincipalBoton()
    {
        if (!pulseBoton)
        {
            StartCoroutine(cambiarEscena(nombreEscenaMenuPrincipal.valorStringEjecucion));
            bloquearBotones();
        }
    }

    public void reiniciarGuardadoBoton()
    {
        if (!pulseBoton)
        {
            //--------------
            if (singletonEventosEscenas.instance != null)
            {
                singletonEventosEscenas.instance.reiniciaScriptable();
            }
            //--------------
            //Reiniciar los datos
            StartCoroutine(cambiarEscena(nombreEscenaMenuPrincipal.valorStringEjecucion));
            bloquearBotones();
        }
    }

    public void pausarJuego()
    {
        Time.timeScale = 0f;
    }

    public void continuarJuego()
    {
        Time.timeScale = 1f;
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

    public void iniciarCanvasMenuInventario()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasInventario"))
        {
            Instantiate(graficos.CanvasMenuInventario, Vector3.zero, Quaternion.identity);
        }
    }

    public void iniciarCanvasMenuConfiguraciones()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasConfiguraciones"))
        {
            Instantiate(graficos.CanvasMenuConfiguraciones, Vector3.zero, Quaternion.identity);
        }
    }
}
