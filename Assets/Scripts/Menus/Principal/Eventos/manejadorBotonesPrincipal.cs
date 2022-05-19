using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorBotonesPrincipal : formulario
{

    private conexionWeb conexion;

    [Header("Canvas que contiene el formulario de eliminacion")]
    [SerializeField] private GameObject canvasElimina;

    [Header("Canvas que contiene el formulario de modificacion")]
    [SerializeField] private GameObject canvasModifica;

    [Header("Nombre de la escena de LogIn")]
    [SerializeField] private valorString escenaLogIn;

    [Header("Nombre de la escena de Laberintos")]
    [SerializeField] private valorString escenaLaberintos;

    [Header("Datos de la partida en curso")]
    [SerializeField] private datosJuego datos;

    void Start()
    {
        reiniciaBotones();
        conexion = gameObject.GetComponent<conexionWeb>();
        if (conexion.getMiUsuario().datosEjecucion.idJugador != 0)
        {
            iniciaVentanaEmergente();
            ManejadorVentanaEmergente.enviaTexto("Bienvenido: " + conexion.getMiUsuario().datosEjecucion.sobrenombre);
            ManejadorVentanaEmergente.reiniciaTiempo();
        }
        else
        {
            botonCierraSesion();
        }
    }

    public void iniciaCanvasElimina()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasEliminar"))
        {
            Instantiate(canvasElimina, Vector3.zero, Quaternion.identity);
        }
    }

    public void iniciaCanvasModifica()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasModificar"))
        {
            Instantiate(canvasModifica, Vector3.zero, Quaternion.identity);
        }
    }

    public void botonCierraSesion()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickCerrar();
            conexion.cierraSesion();
            StartCoroutine(cambioEscena(escenaLogIn.valorStringEjecucion));
            PulseBoton = true;
        }
    }

    public void botonEliminaUsuario()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            iniciaCanvasElimina();
            PulseBoton = true;
        }
    }

    public void botonModificaUsuario()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            iniciaCanvasModifica();
            PulseBoton = true;
        }
    }

    public void botonNuevaPartida()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            //datos.reiniciaObjetosScriptable();
            //datos.guardaObjetosScriptable();
            StartCoroutine(cambioEscena(escenaLaberintos.valorStringEjecucion));
            PulseBoton = true;
        }
    }

    public void botonContinuarPartida() 
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            PulseBoton = true;
        }
    }

    private IEnumerator cambioEscena(string escenaCarga)
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
