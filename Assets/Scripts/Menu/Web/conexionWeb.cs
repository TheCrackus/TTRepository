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
    cerreSesion,
    iniciandoRegistro,
    termineRegistro,
    falleRegistroConexion,
    falleRegistroDatos,
    iniciandoEliminacion,
    termineEliminacion,
    falleEliminacionDatos,
    falleEliminacionConexion,
    ninguno
}

public class conexionWeb : MonoBehaviour
{
    private string respuestaServidor;
    private conexionState estadoActualConexion;
    public usuario miUsuario;

    public void Start()
    {
        estadoActualConexion = conexionState.ninguno;
    }

    public string getRespuestaServidor() 
    {
        return respuestaServidor;
    }

    public void setRespuestaServidor(string respuestaServidor) 
    {
        this.respuestaServidor = respuestaServidor;
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
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            miUsuario.datosEjecucion = JsonUtility.FromJson<usuario.datosUsuario>(web.downloadHandler.text);
            if (miUsuario.datosEjecucion.id_jugador == 0)
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
            estadoActualConexion = conexionState.falleIniciarSesionConexion;
        }
    }

    public void registraUsuario(string email, string password, string sobrenombre, string nacimiento) 
    {
        if (estadoActualConexion != conexionState.iniciandoRegistro)
        { 
            estadoActualConexion = conexionState.iniciandoRegistro;
            StartCoroutine(registra(email, password, sobrenombre, nacimiento));
        } 
    }

    private IEnumerator registra(string email, string password, string sobrenombre, string nacimiento) 
    {
        WWWForm forma = new WWWForm();
        forma.AddField("sobrenombre", sobrenombre);
        forma.AddField("nacimiento", nacimiento);
        forma.AddField("email", email);
        forma.AddField("password", password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/RJ_JSON", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = conexionState.termineRegistro;
            }
            else 
            {
                estadoActualConexion = conexionState.falleRegistroDatos;
            }
        }
        else
        {
            estadoActualConexion = conexionState.falleRegistroConexion;
        }
    }

    public void eliminaUsuario() 
    {
        if (estadoActualConexion != conexionState.iniciandoEliminacion)
        {
            estadoActualConexion = conexionState.iniciandoEliminacion;
            StartCoroutine(elimina());
        }
    }

    private IEnumerator elimina() 
    {
        WWWForm forma = new WWWForm();
        forma.AddField("id_jugador", miUsuario.datosEjecucion.id_jugador);
        forma.AddField("email", miUsuario.datosEjecucion.mail);
        forma.AddField("password", miUsuario.datosEjecucion.password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/EJ_JSON", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = conexionState.termineEliminacion;
            }
            else
            {
                estadoActualConexion = conexionState.falleEliminacionDatos;
            }
        }
        else
        {
            estadoActualConexion = conexionState.falleEliminacionConexion;
        }
    }

    public void cierraSesion() 
    {
        miUsuario.datosEjecucion = miUsuario.datosReset;
        estadoActualConexion = conexionState.cerreSesion;
    }
}
