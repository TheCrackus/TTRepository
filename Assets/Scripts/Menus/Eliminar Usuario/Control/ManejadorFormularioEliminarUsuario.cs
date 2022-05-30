using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorFormularioEliminarUsuario : ManejadorFormulario, ICanvasMenuPrincipal
{

    private ComponenteGraficoEliminaUsuario graficos;

    [Header("Nombre de la escena de LogIn")]
    [SerializeField] private valorString escenaLogIn;


    void Start()
    {
        graficos = (ComponenteGraficoEliminaUsuario)ComponenteGrafico;
        ManejadorAudioInterfazGrafica.reproduceAudioAbrirVentana();
        reiniciarBotones();
    }

    public void eliminarUsuarioBoton()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            if (graficos.PasswordFiled.text.ToString().Equals(Conexion.MiUsuario.datosEjecucion.password)) 
            {
                Conexion.eliminaUsuario();
                bloquearBotones();
                StartCoroutine(esperarDatosEliminaUsuario());
            }
            else 
            {
                iniciarVentanaEmergente();
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("La contraseña proporcionada es incorrecta.");
            }
        }
    }

    public void regresarBoton()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
            //iniciarCanvasPrincipal();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void cerrarSesion() 
    {
        Conexion.cierraSesion();
        StartCoroutine(cambiarEscena(escenaLogIn.valorStringEjecucion));
    }

    private IEnumerator esperarDatosEliminaUsuario()
    {
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => (Conexion.EstadoActualConexion == estadoConexion.iniciandoEliminacion));
        if (Conexion.EstadoActualConexion == estadoConexion.termineEliminacion)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Eliminación completa...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            cerrarSesion();
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleEliminacionConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexión...");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleEliminacionDatos)
                {
                    ManejadorVentanaEmergente.enviarTextoVentanaEmergente("El usuario no pudo ser eliminado...");
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = estadoConexion.ninguno;
                }
            }
        }
        reiniciarBotones();
    }

    public void iniciarCanvasMenuPrincipal()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasPrincipal"))
        {
            Instantiate(graficos.CanvasMenuPrincipal, Vector3.zero, Quaternion.identity);
        }
    }

}
