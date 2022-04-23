using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manejadorBotonesLogIn : MonoBehaviour
{
    public InputField email;
    public InputField password;
    public GameObject fondoEmergente;
    public string escenaPrincipal;

    public void botonIniciaSesion()
    {
        conexionWeb conexion = gameObject.GetComponent<conexionWeb>();
        conexion.iniciaSesion(email.text.ToString(), password.text.ToString());
        StartCoroutine(esperaDatos(conexion));
    }

    private IEnumerator esperaDatos(conexionWeb conexion)
    {
        fondoEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoSesion));
        if (conexion.getEstadoActualConexion() == conexionState.termineIniciarSesion)
        {
            fondoEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Datos listos...");
            yield return new WaitForSeconds(1f);
            conexion.setEstadoActualConexion(conexionState.ninguno);
            fondoEmergente.GetComponent<manejadorVentanaEmergente>().cierraVentanaEmergente();
            StartCoroutine(cambioEscena(escenaPrincipal));
        }
        else
        {
            if (conexion.getEstadoActualConexion() == conexionState.falleIniciarSesionConexion)
            {
                fondoEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Fallo de conexión...");
                yield return new WaitForSeconds(1f);
                conexion.setEstadoActualConexion(conexionState.ninguno);
            }
            else 
            {
                if (conexion.getEstadoActualConexion() == conexionState.falleIniciarSesionDatos)
                {
                    fondoEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("El usuario no existe...");
                    yield return new WaitForSeconds(1f);
                    conexion.setEstadoActualConexion(conexionState.ninguno);
                }
            }
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
