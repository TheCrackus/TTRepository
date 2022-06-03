using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorMenuPausa : ManejadorMenuGenerico, IReproductorAudioInterfazGrafica, IPausa, IBotonPulso, ICanvasMenuInventario, ICanvasMenuConfiguraciones
{

    private ComponenteGraficoMenuPausa graficos;

    private bool condicionPausa;

    private bool pulseBoton;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private ValorString nombreEscenaMenuPrincipal;

    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    [SerializeField] private CambioEscena estadoCambioEscena;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private AudioInterfazGrafica manejadorAudioInterfazGrafica;

    public bool CondicionPausa { get => condicionPausa; set => condicionPausa = value; }
    public AudioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public bool PulseBoton { get => pulseBoton; set => pulseBoton= value; }

    private void OnEnable()
    {
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

    private void Start()
    {
        graficos = (ComponenteGraficoMenuPausa) ComponenteGrafico;
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
            cerrarGrafico();
        }
    }

    public void reiniciarGuardadoBoton()
    {
        if (!pulseBoton)
        {
            //--------------
            if (SingletonEventosEscenas.instance != null)
            {
                SingletonEventosEscenas.instance.reiniciarScriptable();
            }
            //--------------
            //Reiniciar los datos
            StartCoroutine(cambiarEscena(nombreEscenaMenuPrincipal.valorStringEjecucion));
            bloquearBotones();
            cerrarGrafico();
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
