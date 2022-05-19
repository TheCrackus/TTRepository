using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorBotonesElimina : formulario
{

    private conexionWeb conexion;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField passwordFiled;

    void Start()
    {
        reiniciaBotones();
        conexion = gameObject.GetComponent<conexionWeb>();
    }

    public void botonEliminaUsuario()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfaz.reproduceAudioClickAbrir();
            if (passwordFiled.text.ToString().Equals(conexion.getMiUsuario().datosEjecucion.password)) 
            {
                conexion.eliminaUsuario();
                PulseBoton = true;
                StartCoroutine(esperaDatosEliminaUsuario());
            }
            else 
            {
                iniciaVentanaEmergente();
                ManejadorVentanaEmergente.enviaTexto("La contraseña proporcionada es incorrecta.");
                ManejadorVentanaEmergente.reiniciaTiempo();
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
            Destroy(CanvasFormulario);
        }
    }

    public void cierraSesion() 
    {
        EventoReiniciaBotones.invocaFunciones();
        EventoCierraSesion.invocaFunciones();
        Destroy(CanvasFormulario);
    }

    private IEnumerator esperaDatosEliminaUsuario()
    {
        iniciaVentanaEmergente();
        ManejadorVentanaEmergente.enviaTexto("Procesando datos...");
        ManejadorVentanaEmergente.reiniciaTiempo();
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoEliminacion));
        if (conexion.getEstadoActualConexion() == conexionState.termineEliminacion)
        {
            ManejadorVentanaEmergente.enviaTexto("Eliminación completa...");
            ManejadorVentanaEmergente.reiniciaTiempo();
            yield return new WaitForSeconds(1f);
            conexion.setEstadoActualConexion(conexionState.ninguno);
            cierraSesion();
        }
        else
        {
            if (conexion.getEstadoActualConexion() == conexionState.falleEliminacionConexion)
            {
                ManejadorVentanaEmergente.enviaTexto("Fallo de conexión...");
                ManejadorVentanaEmergente.reiniciaTiempo();
                yield return new WaitForSeconds(1f);
                conexion.setEstadoActualConexion(conexionState.ninguno);
            }
            else
            {
                if (conexion.getEstadoActualConexion() == conexionState.falleEliminacionDatos)
                {
                    ManejadorVentanaEmergente.enviaTexto("El usuario no pudo ser eliminado...");
                    ManejadorVentanaEmergente.reiniciaTiempo();
                    yield return new WaitForSeconds(1f);
                    conexion.setEstadoActualConexion(conexionState.ninguno);
                }
            }
        }
        reiniciaBotones();
    }
}
