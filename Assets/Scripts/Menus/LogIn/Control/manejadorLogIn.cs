using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manejadorLogIn : manejadorFormulario, pulsoBoton
{

    private bool pulseBoton;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private valorString escenaMenuPrincipal;

    [Header("Datos de la partida en curso")]
    [SerializeField] private datosJuego datos;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }

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
            Instantiate( ((componentesGraficosLogIn)Graficos).CanvasRegistro, Vector3.zero, Quaternion.identity);
        }
    }

    public void botonIniciaSesion()
    {
        if (!pulseBoton) 
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            Conexion.iniciaSesion( ((componentesGraficosLogIn) Graficos).EmailField.text.ToString(), 
                ((componentesGraficosLogIn)Graficos).PasswordFiled.text.ToString() );
            StartCoroutine(esperaDatosInicioSesion());
            pulseBoton = true;
        }
    }

    public void botonRegistro() 
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            iniciaCanvasRegistro();
            pulseBoton = true;
            cierraFormulario();
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
        cierraGrafico();
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
                    if (Conexion.RespuestaServidor == "NO VERIFICADO")
                    {
                        ManejadorVentanaEmergente.enviaTexto("Tu usuario no está verificado, por favor, " +
                            "verifica tu cuenta ingresando al correo electrónico registrado...");
                    }
                    else 
                    {
                        ManejadorVentanaEmergente.enviaTexto("El usuario no existe...");
                    }
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = estadoConexion.ninguno;
                }
            }
        }
        reiniciaBotones();
    }

    public void reiniciaBotones()
    {
        pulseBoton = false;
    }
}
