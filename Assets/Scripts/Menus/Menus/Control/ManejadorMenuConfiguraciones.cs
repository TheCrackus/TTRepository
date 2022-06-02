using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorMenuConfiguraciones : ManejadorMenuGenerico, IBotonPulso, IReproductorAudioInterfazGrafica, ICanvasMenuPrincipal, IPausa
{

    private string nombreEscenaActual;

    private ComponenteGraficoMenuConfiguracion graficos;

    private bool condicionPausa;

    private bool pulseBoton;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private AudioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Nombre de la escena del menu principal")]
    [SerializeField] private valorString nombreEscenaPrincipal;

    public bool CondicionPausa { get => condicionPausa; set => condicionPausa = value; }
    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }
    public AudioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }

    private void OnEnable()
    {
        reproducirAudioAbreVentana();
        reiniciarBotones();
        nombreEscenaActual = SceneManager.GetActiveScene().name;
        if (nombreEscenaActual != nombreEscenaPrincipal.valorStringEjecucion) 
        {
            pausarJuego();
        }
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        reproducirAudioClickCerrar();
        if (nombreEscenaActual != nombreEscenaPrincipal.valorStringEjecucion)
        {
            continuarJuego();
        }
    }

    private void Start()
    {
        graficos = (ComponenteGraficoMenuConfiguracion) ComponenteGrafico;
    }

    public void regresarBoton() 
    {
        if (!pulseBoton) 
        {
            if (nombreEscenaActual != nombreEscenaPrincipal.valorStringEjecucion)
            {
                reproducirAudioClickCerrar();
                bloquearBotones();
                cerrarGrafico();
            }
            else 
            {
                reproducirAudioClickCerrar();
                iniciarCanvasMenuPrincipal();
                bloquearBotones();
                cerrarGrafico();
            }
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

    public void reiniciarBotones()
    {
        pulseBoton = false;
    }

    public void bloquearBotones()
    {
        pulseBoton = true;
    }

    public void pausarJuego()
    {
        Time.timeScale = 0f;
    }

    public void continuarJuego()
    {
        Time.timeScale = 1f;
    }
    public void iniciarCanvasMenuPrincipal()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasPrincipal"))
        {
            Instantiate(graficos.CanvasMenuPrincipal, Vector3.zero, Quaternion.identity);
        }
    }

}
