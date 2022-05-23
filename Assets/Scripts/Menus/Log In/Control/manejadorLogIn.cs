using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manejadorLogIn : formulario
{

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private valorString escenaMenuPrincipal;

    [Header("Datos de la partida en curso")]
    [SerializeField] private datosJuego datos;

    void Start()
    {
        reiniciaBotones();
        if (Conexion.MiUsuario.datosEjecucion.idJugador != 0)
        {
            StartCoroutine(cambioEscena(escenaMenuPrincipal.valorStringEjecucion));
        }
        
    }

    public void iniciaCanvasRegistro() 
    {
        if (!GameObject.FindGameObjectWithTag("CanvasRegistro"))
        {
            Instantiate(((componentesGraficosLogIn) Graficos).CanvasRegistro, Vector3.zero, Quaternion.identity);
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
            Conexion.iniciaSesion(((componentesGraficosLogIn)Graficos).EmailField.text.ToString(), 
                ((componentesGraficosLogIn)Graficos).PasswordFiled.text.ToString());
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
        yield return new WaitWhile(() => (Conexion.EstadoActualConexion == estadoConexion.iniciandoSesion));
        if (Conexion.EstadoActualConexion == estadoConexion.termineIniciarSesion)
        {
            ManejadorVentanaEmergente.enviaTexto("Datos listos...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            //falta reiniciar guardado
            StartCoroutine(cambioEscena(escenaMenuPrincipal.valorStringEjecucion));
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleIniciarSesionConexion)
            {
                ManejadorVentanaEmergente.enviaTexto("Fallo de conexión...");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else 
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleIniciarSesionDatos)
                {
                    ManejadorVentanaEmergente.enviaTexto("El usuario no existe...");
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = estadoConexion.ninguno;
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
