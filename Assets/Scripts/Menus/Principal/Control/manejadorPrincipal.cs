using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorPrincipal : formulario
{

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

    [Header("Datos de la partida en curso")]
    [SerializeField] private datosJuego datos;

    void Start()
    {
        reiniciaBotones();
        iniciaVentanaEmergente();
        if (Conexion.MiUsuario.datosEjecucion.idJugador != 0)
        {
            ManejadorVentanaEmergente.enviaTexto("Bienvenido: " + Conexion.MiUsuario.datosEjecucion.sobrenombre);
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
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickCerrar();
            cierraSesion();
            PulseBoton = true;
        }
    }

    public void cierraSesion() 
    {
        Conexion.cierraSesion();
        StartCoroutine(cambioEscena(escenaLogIn.valorStringEjecucion));
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
        if (!PulseBoton
            && escenaLaberintos != null
            && escenaLaberintos.valorStringEjecucion != "")
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            //--------------
            datos.reiniciaValoresScriptable();
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
            PulseBoton = true;
        }
    }

    public void botonNivel1()
    {
        if (!PulseBoton
            && escenaLaberintos != null
            && escenaLaberintos.valorStringEjecucion != "")
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            //--------------
            datos.reiniciaValoresScriptable();
            //--------------
            StartCoroutine(cambioEscena(escenaLaberintos.valorStringEjecucion));
            PulseBoton = true;
        }
    }

    public void botonNivel2()
    {
        if (!PulseBoton
            && escenaMazmorra != null
            && escenaMazmorra.valorStringEjecucion != "")
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            //--------------
            datos.reiniciaValoresScriptable();
            //--------------
            StartCoroutine(cambioEscena(escenaMazmorra.valorStringEjecucion));
            PulseBoton = true;
        }
    }

    public void botonNivel3()
    {
        if (!PulseBoton
            && escenaJefeFinal != null
            && escenaJefeFinal.valorStringEjecucion != "")
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            //--------------
            datos.reiniciaValoresScriptable();
            //--------------
            StartCoroutine(cambioEscena(escenaJefeFinal.valorStringEjecucion));
            PulseBoton = true;
        }
    }

    public void botonContinuarPartida() 
    {
        if (!PulseBoton
            && escenaActual!= null
            && escenaActual.valorStringEjecucion != ""
            && escenaActual.valorStringEjecucion.Length > 0)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            StartCoroutine(cambioEscena(escenaActual.valorStringEjecucion));
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

    private IEnumerator cambioEscena(string escenaCarga)
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
