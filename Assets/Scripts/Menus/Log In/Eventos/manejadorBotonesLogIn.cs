using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manejadorBotonesLogIn : formulario
{

    private conexionWeb conexion;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private valorString escenaMenuPrincipal;

    [Header("Canvas que contiene el formulario de registro")]
    [SerializeField] private GameObject canvasRegistro;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField emailField;

    [SerializeField] private InputField passwordFiled;

    [Header("Datos de la partida en curso")]
    [SerializeField] private datosJuego datos;

    void Start()
    {
        reiniciaBotones();
        conexion = gameObject.GetComponent<conexionWeb>();
        if (conexion.getMiUsuario().datosEjecucion.idJugador != 0)
        {
            StartCoroutine(cambioEscena(escenaMenuPrincipal.valorStringEjecucion));
        }
    }

    public void iniciaCanvasRegistro() 
    {
        if (!GameObject.FindGameObjectWithTag("CanvasRegistro"))
        {
            Instantiate(canvasRegistro, Vector3.zero, Quaternion.identity);
        }
    }

    public bool getPulseBoton()
    {
        return PulseBoton;
    }

    public void setPulseBoton(bool pulseBoton)
    {
        this.PulseBoton = pulseBoton;
    }

    public void botonIniciaSesion()
    {
        if (!PulseBoton) 
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            conexion.iniciaSesion(emailField.text.ToString(), passwordFiled.text.ToString());
            StartCoroutine(esperaDatosInicioSesion());
            PulseBoton = true;
        }
    }

    public void botonRegistra() 
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            iniciaCanvasRegistro();
            PulseBoton = true;
        }
    }

    public void botonCierraJuego()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickCerrar();
            PulseBoton = true;
            Application.Quit();
        }
    }

    private IEnumerator esperaDatosInicioSesion()
    {
        iniciaVentanaEmergente();
        ManejadorVentanaEmergente.enviaTexto("Procesando datos...");
        ManejadorVentanaEmergente.reiniciaTiempo();
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoSesion));
        if (conexion.getEstadoActualConexion() == conexionState.termineIniciarSesion)
        {
            ManejadorVentanaEmergente.enviaTexto("Datos listos...");
            ManejadorVentanaEmergente.reiniciaTiempo();
            yield return new WaitForSeconds(1f);
            conexion.setEstadoActualConexion(conexionState.ninguno);
            datos.reiniciaObjetosScriptable();
            StartCoroutine(cambioEscena(escenaMenuPrincipal.valorStringEjecucion));
        }
        else
        {
            if (conexion.getEstadoActualConexion() == conexionState.falleIniciarSesionConexion)
            {
                ManejadorVentanaEmergente.enviaTexto("Fallo de conexión...");
                ManejadorVentanaEmergente.reiniciaTiempo();
                yield return new WaitForSeconds(1f);
                conexion.setEstadoActualConexion(conexionState.ninguno);
            }
            else 
            {
                if (conexion.getEstadoActualConexion() == conexionState.falleIniciarSesionDatos)
                {
                    ManejadorVentanaEmergente.enviaTexto("El usuario no existe...");
                    ManejadorVentanaEmergente.reiniciaTiempo();
                    yield return new WaitForSeconds(1f);
                    conexion.setEstadoActualConexion(conexionState.ninguno);
                }
            }
        }
        reiniciaBotones();
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
