using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorFormularioEliminaUsuario : ManejadorFormulario, ICanvasMenuPrincipal
{

    private ComponenteGraficoFormularioEliminaUsuario graficos;

    [Header("Nombre de la escena de LogIn")]
    [SerializeField] private valorString escenaLogIn;


    void Start()
    {
        graficos = (ComponenteGraficoFormularioEliminaUsuario)ComponenteGrafico;
        ManejadorAudioInterfazGrafica.reproducirAudioAbrirVentana();
        reiniciarBotones();
    }

    public void eliminarUsuarioBoton()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            if (graficos.PasswordFiled.text.ToString() == Conexion.MiUsuario.datosEjecucion.password)
            {
                Conexion.eliminarUsuario();
                bloquearBotones();
                StartCoroutine(esperarDatosEliminaUsuario());
            }
            else 
            {
                iniciarVentanaEmergente();
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("La contrase�a proporcionada es incorrecta.");
            }
        }
    }

    public void regresarBoton()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
            iniciarCanvasMenuPrincipal();
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
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Eliminaci�n de usuario completada con �xito, se cerr� la sesi�n.");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            cerrarSesion();
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleEliminacionConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexi�n, comprueba el estado de tu red a internet.");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleEliminacionDatos)
                {
                    ManejadorVentanaEmergente.enviarTextoVentanaEmergente("El usuario no pudo ser eliminado, por favor, verifique su contrase�a.");
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
