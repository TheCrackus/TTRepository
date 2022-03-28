using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using miUsuario = usuario;

public class manejadorBotones : MonoBehaviour
{
    public InputField email;
    public InputField password;
    public miUsuario.datosUsuario datos;
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
        miUsuario.vaciar();
        datos = miUsuario.getDatosUsuario();
        fondoEmergente.SetActive(false);
    }

    private IEnumerator esperaDatos(conexionWeb conexion) {
        Debug.Log("Procesando datos...");
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoSesion));
        if (conexion.getEstadoActualConexion() == conexionState.termineIniciarSesion)
        {
            Debug.Log("Datos listos...");
            datos = miUsuario.getDatosUsuario();
            conexion.setEstadoActualConexion(conexionState.ninguno);
            textoVentanaEmergente.text = "ID jugador: " + datos.id_jugador.ToString() 
                + "\nSobrenombre: " + datos.Sobrenombre
                + "\nNacimineto: " + datos.nacimiento
                + "\nEmail: " + datos.nacimiento;
            fondoEmergente.SetActive(true);
        }
    }
}
