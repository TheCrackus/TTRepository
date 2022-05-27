using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorElimina : manejadorFormulario, pulsoBoton
{
    private bool pulseBoton;

    [Header("Nombre de la escena de LogIn")]
    [SerializeField] private valorString escenaLogIn;

    public bool PulseBoton { get => pulseBoton; set => pulseBoton = value; }

    void Start()
    {
        ManejadorAudioInterfazGrafica.reproduceAudioAbrirVentana();
        reiniciaBotones();
    }

    public void iniciaCanvasPrincipal()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasPrincipal"))
        {
            Instantiate(((componentesGraficosEliminaUsuario)Graficos).CanvasMenuPrincipal, Vector3.zero, Quaternion.identity);
        }
    }

    public void botonEliminaUsuario()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            if (((componentesGraficosEliminaUsuario) Graficos).PasswordFiled.text.ToString().Equals(Conexion.MiUsuario.datosEjecucion.password)) 
            {
                Conexion.eliminaUsuario();
                pulseBoton = true;
                StartCoroutine(esperaDatosEliminaUsuario());
            }
            else 
            {
                iniciaVentanaEmergente();
                ManejadorVentanaEmergente.enviaTexto("La contraseña proporcionada es incorrecta.");
            }
        }
    }

    public void botonRegresar()
    {
        if (!pulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
            iniciaCanvasPrincipal();
            pulseBoton = true;
            cierraFormulario();
        }
    }

    public void cierraSesion() 
    {
        Conexion.cierraSesion();
        StartCoroutine(cambioEscena(escenaLogIn.valorStringEjecucion));
    }

    public void cierraFormulario()
    {
        cierraGrafico();
    }

    private IEnumerator esperaDatosEliminaUsuario()
    {
        iniciaVentanaEmergente();
        ManejadorVentanaEmergente.enviaTexto("Procesando datos...");
        yield return new WaitWhile(() => (Conexion.EstadoActualConexion == estadoConexion.iniciandoEliminacion));
        if (Conexion.EstadoActualConexion == estadoConexion.termineEliminacion)
        {
            ManejadorVentanaEmergente.enviaTexto("Eliminación completa...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            cierraSesion();
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleEliminacionConexion)
            {
                ManejadorVentanaEmergente.enviaTexto("Fallo de conexión...");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleEliminacionDatos)
                {
                    ManejadorVentanaEmergente.enviaTexto("El usuario no pudo ser eliminado...");
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
