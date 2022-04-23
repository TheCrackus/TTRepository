using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum conexionState
{
    iniciandoSesion,
    termineIniciarSesion,
    falleIniciarSesionDatos,
    falleIniciarSesionConexion,
    ninguno
}

public class conexionWeb : MonoBehaviour
{
    private conexionState estadoActualConexion;
    public usuario miUsuario;

    public void Start()
    {
        estadoActualConexion = conexionState.ninguno;
    }

    public void iniciaSesion(string email, string password) 
    {
        if (estadoActualConexion != conexionState.iniciandoSesion) 
        {
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
            miUsuario.datosEjecucuion = JsonUtility.FromJson<usuario.datosUsuario>(web.downloadHandler.text);
            if (miUsuario.datosEjecucuion.id_jugador == 0)
            {
                estadoActualConexion = conexionState.falleIniciarSesionDatos;
            }
            else 
            {
                estadoActualConexion = conexionState.termineIniciarSesion;
            }
            
        }
        else 
        {
            Debug.LogError(web.result);
            estadoActualConexion = conexionState.falleIniciarSesionConexion;
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
