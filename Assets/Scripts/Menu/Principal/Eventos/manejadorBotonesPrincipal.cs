using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorBotonesPrincipal : MonoBehaviour
{
    private bool pulseBoton;
    private conexionWeb conexion;
    public GameObject ventanaEmergente;
    public string escenaLogIn;
    public string escenaLaberintos;
    public GameObject manejadorElimina;
    public GameObject manejadorModifica;

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
        if (conexion.miUsuario.datosEjecucion.id_jugador != 0)
        {
            ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Bienvenido: " + conexion.miUsuario.datosEjecucion.Sobrenombre, true);
        }
        else
        {
            StartCoroutine(cambioEscena(escenaLogIn));
        }
    }

    public void botonCierraSesion()
    {
        if (!pulseBoton) 
        {
            conexion.cierraSesion();
            pulseBoton = true;
            StartCoroutine(cambioEscena(escenaLogIn));
        }
    }

    public void botonEliminaUsuario() 
    {
        if (!pulseBoton)
        {
            manejadorElimina.SetActive(true);
            manejadorElimina.GetComponent<manejadorBotonesElimina>().setPulseBoton(false);
            pulseBoton = true;
        }
    }

    public void botonModificaUsuario() 
    {
        if (!pulseBoton)
        {
            manejadorModifica.SetActive(true);
            manejadorModifica.GetComponent<manejadorBotonesModifica>().setPulseBoton(false);
            pulseBoton = true;
        }
    }

    public void botonNuevaPartida() 
    {
        if (!pulseBoton)
        {
            pulseBoton = true;
            StartCoroutine(cambioEscena(escenaLaberintos));
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
