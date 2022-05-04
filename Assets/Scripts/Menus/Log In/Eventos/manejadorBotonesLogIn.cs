using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manejadorBotonesLogIn : MonoBehaviour
{
    private conexionWeb conexion;
    private bool pulseBoton;
    public InputField emailField;
    public InputField passwordFiled;
    public GameObject ventanaEmergente;
    public string escenaPrincipal;
    public string escenaRegistra;

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

    public void botonIniciaSesion()
    {
        if (!pulseBoton) 
        {
            conexion.iniciaSesion(emailField.text.ToString(), passwordFiled.text.ToString());
            pulseBoton = true;
            StartCoroutine(esperaDatosInicioSesion());
        }
    }

    public void botonRegistra() 
    {
        if (!pulseBoton)
        {
            pulseBoton = true;
            StartCoroutine(cambioEscena(escenaRegistra));
        }
    }

    private IEnumerator esperaDatosInicioSesion()
    {
        ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Procesando datos...", false);
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoSesion));
        if (conexion.getEstadoActualConexion() == conexionState.termineIniciarSesion)
        {
            ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Datos listos...", false);
            yield return new WaitForSeconds(1f);
            conexion.setEstadoActualConexion(conexionState.ninguno);
            ventanaEmergente.GetComponent<manejadorVentanaEmergente>().cierraVentanaEmergente();
            StartCoroutine(cambioEscena(escenaPrincipal));
        }
        else
        {
            if (conexion.getEstadoActualConexion() == conexionState.falleIniciarSesionConexion)
            {
                ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Fallo de conexión...", true);
                yield return new WaitForSeconds(1f);
                conexion.setEstadoActualConexion(conexionState.ninguno);
            }
            else 
            {
                if (conexion.getEstadoActualConexion() == conexionState.falleIniciarSesionDatos)
                {
                    ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("El usuario no existe...", true);
                    yield return new WaitForSeconds(1f);
                    conexion.setEstadoActualConexion(conexionState.ninguno);
                }
            }
        }
        pulseBoton = false;
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
