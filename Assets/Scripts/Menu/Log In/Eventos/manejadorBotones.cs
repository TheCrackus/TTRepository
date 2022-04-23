using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorBotones : MonoBehaviour
{
    public InputField email;
    public InputField password;
    public usuario miUsuario;
    public GameObject fondoEmergente;
    public Text textoVentanaEmergente;

    public void botonIniciaSesion() 
    {
        conexionWeb conexion = gameObject.GetComponent<conexionWeb>();
        conexion.iniciaSesion(email.text.ToString(), password.text.ToString());
        StartCoroutine(esperaDatos(conexion));
    }

    public void cierraVentanaEmergente() 
    {
        textoVentanaEmergente.text = "Advertencias:\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-\n-";
        miUsuario.datosEjecucuion = miUsuario.datosReset;
        fondoEmergente.SetActive(false);
    }

    private IEnumerator esperaDatos(conexionWeb conexion) {
        Debug.Log("Procesando datos...");
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoSesion));
        if (conexion.getEstadoActualConexion() == conexionState.termineIniciarSesion)
        {
            Debug.Log("Datos listos...");
            conexion.setEstadoActualConexion(conexionState.ninguno);
            textoVentanaEmergente.text = "ID jugador: " + miUsuario.datosEjecucuion.id_jugador.ToString()
                + "\nSobrenombre: " + miUsuario.datosEjecucuion.Sobrenombre
                + "\nNacimineto: " + miUsuario.datosEjecucuion.nacimiento
                + "\nEmail: " + miUsuario.datosEjecucuion.nacimiento;
            fondoEmergente.SetActive(true);
        }
    }
}
