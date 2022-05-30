using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorMenuPrincipal : ManejadorMenuGenerico, IBotonPulso, ICanvasVentanaEmergente, IReproductorAudio, IConexion, ICanvasFormularioModificarUsuario, ICanvasFormularioEliminarUsuario
{

    ComponenteGraficoMenuPrincipal graficos;

    private GameObject nuevoCanvasVentanaEmergente;

    private GameObject ventanaEmergente;

    private ManejadorVentanaEmergente manejadorVentanaEmergente;

    private bool pulseBoton;

    [Header("Nombre de la escena de LogIn")]
    [SerializeField] private valorString nombreEscenaLogIn;

    [Header("Nombre de la escena de Laberintos")]
    [SerializeField] private valorString nombreEscenaLaberintos;

    [Header("Nombre de la escena de Mazmorra")]
    [SerializeField] private valorString nombreEscenaMazmorra;

    [Header("Nombre de la escena de Laberintos")]
    [SerializeField] private valorString nombreEscenaJefeFinal;

    [Header("Escena de la partida en curso")]
    [SerializeField] private valorString nombreEscenaActual;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private audioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Manejador de conexiones")]
    [SerializeField] private conexionWeb conexion;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }
    public GameObject NuevoCanvasVentanaEmergente { get => nuevoCanvasVentanaEmergente; set => nuevoCanvasVentanaEmergente = value; }
    public GameObject VentanaEmergente { get => ventanaEmergente; set => ventanaEmergente = value; }
    public ManejadorVentanaEmergente ManejadorVentanaEmergente { get => manejadorVentanaEmergente; set => manejadorVentanaEmergente = value; }
    public audioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public conexionWeb Conexion { get => conexion; set => conexion = value; }

    private void Start()
    {
        graficos = (ComponenteGraficoMenuPrincipal)ComponenteGrafico;
        reiniciarBotones();
        iniciarVentanaEmergente();
        if (Conexion.MiUsuario.datosEjecucion.idJugador != 0)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Bienvenido: " + Conexion.MiUsuario.datosEjecucion.sobrenombre);
        }
        else 
        {
            cerrarSesion();
        }
    }

    public void iniciarCanvasFormularioEliminarUsuario()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasEliminar"))
        {
            Instantiate(graficos.CanvasFormularioEliminacionUsuario, Vector3.zero, Quaternion.identity);
        }
    }

    public void iniciarCanvasFormularioModificarUsuario()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasModificar"))
        {
            Instantiate(graficos.CanvasFormularioModificacionUsuario, Vector3.zero, Quaternion.identity);
        }
    }

    public void cerrarSesion()
    {
        Conexion.cierraSesion();
        StartCoroutine(cambiarEscena(nombreEscenaLogIn.valorStringEjecucion));
    }

    public void cerrarSesionBoton()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
            cerrarSesion();
            pulseBoton = true;
        }
    }

    public void eliminarUsuarioBoton()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            iniciarCanvasFormularioEliminarUsuario();
            pulseBoton = true;
            cerrarFormulario();
        }
    }

    public void modificarUsuarioBoton()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            iniciarCanvasFormularioModificarUsuario();
            pulseBoton = true;
            cerrarFormulario();
        }
    }

    public void iniciarNuevaPartidaBoton()
    {
        if (!pulseBoton
            && nombreEscenaLaberintos != null
            && nombreEscenaLaberintos.valorStringEjecucion != "")
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
                StartCoroutine(cambiarEscena(nombreEscenaLaberintos.valorStringEjecucion));
            }
            else 
            {
                iniciarVentanaEmergente();
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Tu cuenta no est� verificada, por favor, verif�cala en tu correo electr�nico proporcionado");
            }
            pulseBoton = true;
        }
    }

    public void iniciarNivel1Boton()
    {
        if (!pulseBoton
            && nombreEscenaLaberintos != null
            && nombreEscenaLaberintos.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            //--------------
            if (singletonEventosEscenas.instance != null)
            {
                singletonEventosEscenas.instance.reiniciaScriptable();
            }
            //--------------
            //Reiniciar los datos
            StartCoroutine(cambiarEscena(nombreEscenaLaberintos.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void iniciarNivel2Boton()
    {
        if (!pulseBoton
            && nombreEscenaMazmorra != null
            && nombreEscenaMazmorra.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            //--------------
            if (singletonEventosEscenas.instance != null)
            {
                singletonEventosEscenas.instance.reiniciaScriptable();
            }
            //--------------
            //Reiniciar los datos
            StartCoroutine(cambiarEscena(nombreEscenaMazmorra.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void iniciarNivel3Boton()
    {
        if (!pulseBoton
            && nombreEscenaJefeFinal != null
            && nombreEscenaJefeFinal.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            //--------------
            if (singletonEventosEscenas.instance != null)
            {
                singletonEventosEscenas.instance.reiniciaScriptable();
            }
            //--------------
            //Reiniciar los datos
            StartCoroutine(cambiarEscena(nombreEscenaJefeFinal.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void continuarPartidaBoton() 
    {
        if (!pulseBoton
            && nombreEscenaActual!= null
            && nombreEscenaActual.valorStringEjecucion != ""
            && nombreEscenaActual.valorStringEjecucion.Length > 0)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            StartCoroutine(cambiarEscena(nombreEscenaActual.valorStringEjecucion));
            pulseBoton = true;
        }
    }

    public void cerrarJuegoBoton()
    {
        if (!pulseBoton) 
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
            pulseBoton = true;
            Application.Quit();
        }
    }

    public void cerrarFormulario()
    {
        cerrarGrafico();
    }

    public void reiniciarBotones()
    {
        pulseBoton = false;
    }

    public void bloquearBotones()
    {
        pulseBoton = true;
    }
    public void iniciarVentanaEmergente()
    {
        if (nuevoCanvasVentanaEmergente == null)
        {
            if (!GameObject.FindGameObjectWithTag("CanvasVentanaEmergente"))
            {
                nuevoCanvasVentanaEmergente = Instantiate(graficos.CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nuevoCanvasVentanaEmergente = GameObject.FindGameObjectWithTag("CanvasVentanaEmergente").gameObject;
                Destroy(nuevoCanvasVentanaEmergente);
                nuevoCanvasVentanaEmergente = Instantiate(graficos.CanvasVentanaEmergente, Vector3.zero, Quaternion.identity);
            }
            ventanaEmergente = nuevoCanvasVentanaEmergente.gameObject.transform.Find("VentanaEmergente").gameObject;
            manejadorVentanaEmergente = ventanaEmergente.gameObject.GetComponent<ManejadorVentanaEmergente>();
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
}