using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorPrincipal : manejadorMenu, pulsoBoton, iniciaVentanaEmergente, reproduceAudio, conexion
{

    private GameObject nCanvasVentanaEmergente;

    private GameObject ventanaEmergente;

    private manejadorVentanaEmergente manejadorVentanaEmergente;

    private bool pulseBoton;

    [Header("Nombre de la escena de LogIn")]
    [SerializeField] private valorString escenaLogIn;

    [Header("Nombre de la escena de Laberintos")]
    [SerializeField] private valorString escenaLaberintos;

    [Header("Nombre de la escena de Mazmorra")]
    [SerializeField] private valorString escenaMazmorra;

    [Header("Nombre de la escena de Laberintos")]
    [SerializeField] private valorString escenaJefeFinal;

    [Header("Escena de la partida en curso")]
    [SerializeField] private valorString escenaActual;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Manejador de conexiones")]
    [SerializeField] private conexionWeb conexion;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }
    public GameObject NCanvasVentanaEmergente { get => nCanvasVentanaEmergente; set => nCanvasVentanaEmergente = value; }
    public GameObject VentanaEmergente { get => ventanaEmergente; set => ventanaEmergente = value; }
    public manejadorVentanaEmergente ManejadorVentanaEmergente { get => manejadorVentanaEmergente; set => manejadorVentanaEmergente = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public conexionWeb Conexion { get => conexion; set => conexion = value; }

    void Start()
    {
        reiniciaBotones();
        iniciaVentanaEmergente();
        if (Conexion.MiUsuario.datosEjecucion.idJugador != 0)
        {
            ManejadorVentanaEmergente.enviaTexto("Bienvenido: " + Conexion.MiUsuario.datosEjecucion.sobrenombre);
        }
        else 
        {
            cierraSesion();
        }
        if (Conexion.MiUsuario.datosEjecucion.verificado != "verificado")
        {
            ManejadorVentanaEmergente.enviaTexto("Tu cuenta no está verificada, por favor, verifícala en tu correo electrónico proporcionado");
        }
    }

    public void iniciaCanvasElimina()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasEliminar"))
        {
            Instantiate(((componentesGraficosPrincipal)Graficos).CanvasElimina, Vector3.zero, Quaternion.identity);
        }
    }

    public void iniciaCanvasModifica()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasModificar"))
        {
            Instantiate(((componentesGraficosPrincipal) Graficos).CanvasModifica, Vector3.zero, Quaternion.identity);
        }
    }

    public void botonCierraSesion()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
            cierraSesion();
            pulseBoton = true;
        }
    }

    public void cierraSesion() 
    {
        Conexion.cierraSesion();
        StartCoroutine(cambioEscena(escenaLogIn.valorStringEjecucion));
    }

    public void botonEliminaUsuario()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            iniciaCanvasElimina();
            pulseBoton = true;
            cierraFormulario();
        }
    }

    public void botonModificaUsuario()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            iniciaCanvasModifica();
            pulseBoton = true;
            cierraFormulario();
        }
    }

    public void botonNuevaPartida()
    {
        if (!pulseBoton
            && escenaLaberintos != null
            && escenaLaberintos.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            //--------------
            if (singletonEventosEscenas.instance != null)
            {
                singletonEventosEscenas.instance.reiniciaScriptable();
            }
            //--------------
            //Reiniciar los datos
            if (Conexion.MiUsuario.datosEjecucion.verificado == "verificado")
            {
                StartCoroutine(cambioEscena(escenaLaberintos.valorStringEjecucion));
            }
            else 
            {
                iniciaVentanaEmergente();
                ManejadorVentanaEmergente.enviaTexto("Tu cuenta no está verificada, por favor, verifícala en tu correo electrónico proporcionado");
            }
            pulseBoton = true;
        }
    }

    public void botonNivel1()
    {
        if (!pulseBoton
            && escenaLaberintos != null
            && escenaLaberintos.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            //--------------
            if (singletonEventosEscenas.instance != null)
            {
                singletonEventosEscenas.instance.reiniciaScriptable();
            }
            //--------------
            StartCoroutine(cambioEscena(escenaLaberintos.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void botonNivel2()
    {
        if (!pulseBoton
            && escenaMazmorra != null
            && escenaMazmorra.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            //--------------
            if (singletonEventosEscenas.instance != null)
            {
                singletonEventosEscenas.instance.reiniciaScriptable();
            }
            //--------------
            StartCoroutine(cambioEscena(escenaMazmorra.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void botonNivel3()
    {
        if (!pulseBoton
            && escenaJefeFinal != null
            && escenaJefeFinal.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            //--------------
            if (singletonEventosEscenas.instance != null)
            {
                singletonEventosEscenas.instance.reiniciaScriptable();
            }
            //--------------
            StartCoroutine(cambioEscena(escenaJefeFinal.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void botonContinuarPartida() 
    {
        if (!pulseBoton
            && escenaActual!= null
            && escenaActual.valorStringEjecucion != ""
            && escenaActual.valorStringEjecucion.Length > 0)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            StartCoroutine(cambioEscena(escenaActual.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void botonCierraJuego()
    {
        if (!pulseBoton) 
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
            pulseBoton = true;
            Application.Quit();
        }
    }

    public void cierraFormulario()
    {
        Graficos.cierraFormulario();
    }

    public void reiniciaBotones()
    {
        pulseBoton = false;
    }

    public void iniciaVentanaEmergente()
    {
        if (nCanvasVentanaEmergente == null)
        {
            if (!GameObject.FindGameObjectWithTag("CanvasVentanaEmergente"))
            {
                nCanvasVentanaEmergente = Instantiate(((componentesGraficosPrincipal)Graficos).CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nCanvasVentanaEmergente = GameObject.FindGameObjectWithTag("CanvasVentanaEmergente").gameObject;
                Destroy(nCanvasVentanaEmergente);
                nCanvasVentanaEmergente = Instantiate(((componentesGraficosPrincipal)Graficos).CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            ventanaEmergente = nCanvasVentanaEmergente.gameObject.transform.Find("VentanaEmergente").gameObject;
            manejadorVentanaEmergente = ventanaEmergente.gameObject.GetComponent<manejadorVentanaEmergente>();
        }
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
