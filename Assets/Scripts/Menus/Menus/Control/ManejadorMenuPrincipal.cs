using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorMenuPrincipal : ManejadorMenuGenerico, IBotonPulso, IReproductorAudioInterfazGrafica, IConexion, ICanvasFormularioModificarUsuario, ICanvasFormularioEliminarUsuario, ICanvasMenuConfiguraciones, ICanvasFormularioEnvioPrueba
{

    ComponenteGraficoMenuPrincipal graficos;

    private GameObject nuevoCanvasVentanaEmergente;

    private GameObject ventanaEmergente;

    private ManejadorVentanaEmergente manejadorVentanaEmergente;

    private bool pulseBoton;

    [Header("Nombre de la escena de LogIn")]
    [SerializeField] private ValorString nombreEscenaLogIn;

    [Header("Nombre de la escena de Laberintos")]
    [SerializeField] private ValorString nombreEscenaLaberintos;

    [Header("Nombre de la escena de Mazmorra")]
    [SerializeField] private ValorString nombreEscenaMazmorra;

    [Header("Nombre de la escena de Jefe final")]
    [SerializeField] private ValorString nombreEscenaJefeFinal;

    [Header("Escena de la partida en curso")]
    [SerializeField] private ValorString nombreEscenaActual;

    [Header("Manejador de audio de interfaces")]
    [SerializeField] private AudioInterfazGrafica manejadorAudioInterfazGrafica;

    [Header("Manejador de conexiones")]
    [SerializeField] private ConexionWeb conexion;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }
    public GameObject NuevoCanvasVentanaEmergente { get => nuevoCanvasVentanaEmergente; set => nuevoCanvasVentanaEmergente = value; }
    public GameObject VentanaEmergente { get => ventanaEmergente; set => ventanaEmergente = value; }
    public ManejadorVentanaEmergente ManejadorVentanaEmergente { get => manejadorVentanaEmergente; set => manejadorVentanaEmergente = value; }
    public AudioInterfazGrafica ManejadorAudioInterfazGrafica { get => manejadorAudioInterfazGrafica; set => manejadorAudioInterfazGrafica = value; }
    public ConexionWeb Conexion { get => conexion; set => conexion = value; }

    private void Start()
    {
        graficos = (ComponenteGraficoMenuPrincipal)ComponenteGrafico;
        reiniciarBotones();
        if (Conexion.MiUsuario.DatosEjecucion.idJugador != 0)
        {
            if (graficos.TextoNombreJugador != null) 
            {
                graficos.TextoNombreJugador.text = Conexion.MiUsuario.DatosEjecucion.sobrenombre;
            }
        }
        else 
        {
            cerrarSesion();
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
            ManejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
            cerrarSesion();
            bloquearBotones();
        }
    }

    public void iniciarFormularioEliminaUsuarioBoton()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            iniciarCanvasFormularioEliminarUsuario();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void iniciarFormularioModificaUsuarioBoton()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            iniciarCanvasFormularioModificarUsuario();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void iniciarFormularioEnvioPruebaBoton()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            iniciarCanvasFormularioEnvioPrueba();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void iniciarNuevaPartidaBoton()
    {
        if (!pulseBoton
            && nombreEscenaLaberintos != null
            && nombreEscenaLaberintos.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            //--------------
            if (SingletonEventosEscenas.instance != null)
            {
                SingletonEventosEscenas.instance.reiniciarScriptable();
            }
            //--------------
            //Reiniciar los datos

            StartCoroutine(cambiarEscena(nombreEscenaLaberintos.valorStringEjecucion));
            bloquearBotones();
        }
    }

    public void iniciarNivel1Boton()
    {
        if (!pulseBoton
            && nombreEscenaLaberintos != null
            && nombreEscenaLaberintos.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            //--------------
            if (SingletonEventosEscenas.instance != null)
            {
                SingletonEventosEscenas.instance.reiniciarScriptable();
            }
            //--------------
            //Reiniciar los datos
            StartCoroutine(cambiarEscena(nombreEscenaLaberintos.valorStringEjecucion));
            bloquearBotones();
        }
    }

    public void iniciarNivel2Boton()
    {
        if (!pulseBoton
            && nombreEscenaMazmorra != null
            && nombreEscenaMazmorra.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            //--------------
            if (SingletonEventosEscenas.instance != null)
            {
                SingletonEventosEscenas.instance.reiniciarScriptable();
            }
            //--------------
            //Reiniciar los datos
            StartCoroutine(cambiarEscena(nombreEscenaMazmorra.valorStringEjecucion));
            bloquearBotones();
        }
    }

    public void iniciarNivel3Boton()
    {
        if (!pulseBoton
            && nombreEscenaJefeFinal != null
            && nombreEscenaJefeFinal.valorStringEjecucion != "")
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            //--------------
            if (SingletonEventosEscenas.instance != null)
            {
                SingletonEventosEscenas.instance.reiniciarScriptable();
            }
            //--------------
            //Reiniciar los datos
            StartCoroutine(cambiarEscena(nombreEscenaJefeFinal.valorStringEjecucion));
            bloquearBotones();
        }
    }

    public void continuarPartidaBoton() 
    {
        if (!pulseBoton
            && nombreEscenaActual!= null
            && nombreEscenaActual.valorStringEjecucion != ""
            && nombreEscenaActual.valorStringEjecucion.Length > 0)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            StartCoroutine(cambiarEscena(nombreEscenaActual.valorStringEjecucion));
            bloquearBotones();
        }
    }

    public void iniciarMenuConfiguracionesBoton() 
    {
        if (!PulseBoton) 
        {
            iniciarCanvasMenuConfiguraciones();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void cerrarJuegoBoton()
    {
        if (!pulseBoton) 
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
            bloquearBotones();
            Application.Quit();
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

    public void iniciarCanvasMenuConfiguraciones()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasConfiguraciones"))
        {
            Instantiate(graficos.CanvasMenuConfiguraciones, Vector3.zero, Quaternion.identity);
        }
    }

    public void iniciarCanvasFormularioEnvioPrueba()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasPrueba"))
        {
            Instantiate(graficos.CanvasFormularioEnvioPrueba, Vector3.zero, Quaternion.identity);
        }
    }

}
