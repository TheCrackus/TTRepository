using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorElimina : formulario
{

    void Start()
    {
        ManejadorAudioInterfaz.reproduceAudioAbrirVentana();
        reiniciaBotones();
    }

    public void botonEliminaUsuario()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            if (((componentesGraficosEliminaUsuario) Graficos).PasswordFiled.text.ToString().Equals(Conexion.MiUsuario.datosEjecucion.password)) 
            {
                Conexion.eliminaUsuario();
                PulseBoton = true;
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
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickCerrar();
            EventoReiniciaBotones.invocaFunciones();
            PulseBoton = true;
            Destroy(Graficos.CanvasFormulario);
        }
    }

    public void cierraSesion() 
    {
        EventoCierraSesion.invocaFunciones();
        Destroy(Graficos.CanvasFormulario);
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
}
