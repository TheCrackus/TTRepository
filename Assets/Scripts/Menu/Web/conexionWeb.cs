using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using miUsuario = usuario;

public enum conexionState
{
    iniciandoSesion,
    termineIniciarSesion,
    ninguno
}

public class conexionWeb : MonoBehaviour
{
    
    private conexionState estadoActualConexion;

    public void Start()
    {
        estadoActualConexion = conexionState.ninguno;
    }

    public void iniciaSesion(string email, string password) 
    {
        if (estadoActualConexion != conexionState.iniciandoSesion) 
        {
            Debug.Log("Iniciando sesion...");
            estadoActualConexion = conexionState.iniciandoSesion;
            StartCoroutine(inicia(email, password));
        }
    }

    private IEnumerator inicia(string email, string password) 
    {
        WWWForm forma = new WWWForm();
        forma.AddField("email", email);
        forma.AddField("password", password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/ISJ_JSON", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Recibi resultados...");
            miUsuario.setDatosUsuario(JsonUtility.FromJson<miUsuario.datosUsuario>(web.downloadHandler.text));
            estadoActualConexion = conexionState.termineIniciarSesion;
        }
        else 
        {
            Debug.Log("No recibi resultados...");
            Debug.LogError(web.result);
            estadoActualConexion = conexionState.ninguno;
        }
    }

    public void setEstadoActualConexion(conexionState nuevoEstado)
    {
        if (estadoActualConexion != nuevoEstado)
        {
            estadoActualConexion = nuevoEstado;
        }
    }

    public conexionState getEstadoActualConexion() 
    {
        return estadoActualConexion;
    }
}
