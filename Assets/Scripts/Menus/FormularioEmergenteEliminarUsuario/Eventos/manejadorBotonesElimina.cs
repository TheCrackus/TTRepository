using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorBotonesElimina : MonoBehaviour
{
    private bool pulseBoton;
    private conexionWeb conexion;
    public GameObject ventanaEmergente;
    public InputField passwordFiled;
    public GameObject manejadorPrincipal;

    public bool getPulseBoton()
    {
        return pulseBoton;
    }

    public void setPulseBoton(bool pulseBoton)
    {
        this.pulseBoton = pulseBoton;
    }

    void Start()
    {
        pulseBoton = false;
        conexion = gameObject.GetComponent<conexionWeb>();
    }

    public void botonRegresar()
    {
        if (!pulseBoton)
        {
            pulseBoton = true;
            gameObject.SetActive(false);
            manejadorPrincipal.GetComponent<manejadorBotonesPrincipal>().setPulseBoton(false);
        }
    }

    public void botonEliminaUsuario()
    {
        if (!pulseBoton)
        {
            if (passwordFiled.text.ToString().Equals(conexion.miUsuario.datosEjecucion.password)) 
            {
                conexion.eliminaUsuario();
                pulseBoton = true;
                StartCoroutine(esperaDatosEliminaUsuario());
            }
            else 
            {
                ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("La contraseña proporcionada es incorrecta.", true);
            }
        }
    }

    private IEnumerator esperaDatosEliminaUsuario()
    {
        ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Procesando datos...", false);
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoEliminacion));
        if (conexion.getEstadoActualConexion() == conexionState.termineEliminacion)
        {
            ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Eliminación completa...", false);
            yield return new WaitForSeconds(1f);
            conexion.setEstadoActualConexion(conexionState.ninguno);
            ventanaEmergente.GetComponent<manejadorVentanaEmergente>().cierraVentanaEmergente();
            manejadorPrincipal.GetComponent<manejadorBotonesPrincipal>().setPulseBoton(false);
            manejadorPrincipal.GetComponent<manejadorBotonesPrincipal>().botonCierraSesion();
        }
        else
        {
            if (conexion.getEstadoActualConexion() == conexionState.falleEliminacionConexion)
            {
                ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Fallo de conexión...", true);
                yield return new WaitForSeconds(1f);
                conexion.setEstadoActualConexion(conexionState.ninguno);
            }
            else
            {
                if (conexion.getEstadoActualConexion() == conexionState.falleEliminacionDatos)
                {
                    ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("El usuario no pudo ser eliminado...", true);
                    yield return new WaitForSeconds(1f);
                    conexion.setEstadoActualConexion(conexionState.ninguno);
                }
            }
        }
        pulseBoton = false;
    }
}
