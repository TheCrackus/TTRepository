using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum estadoConexion
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
    iniciandoModificacion,
    termineModificacion,
    falleModificacionDatos,
    falleModificacionConexion,
    ninguno
}

public class conexionWeb : MonoBehaviour
{

    private string respuestaServidor;

    [Header("Estado de la conexion actual")]
    [SerializeField] private estadoConexion estadoActualConexion;

    [Header("Contenedor del usuario")]
    [SerializeField] private usuario miUsuario;

    public string RespuestaServidor { get => respuestaServidor; set => respuestaServidor = value; }
    public usuario MiUsuario { get => miUsuario; set => miUsuario = value; }
    public estadoConexion EstadoActualConexion { get => estadoActualConexion; set => estadoActualConexion = value; }

    public void Start()
    {
        estadoActualConexion = estadoConexion.ninguno;
    }

    public void iniciaSesion(string email, string password) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoSesion) 
        {
            estadoActualConexion = estadoConexion.iniciandoSesion;
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
            if (respuestaServidor != "ERROR" && respuestaServidor != "NO VERIFICADO")
            {
                string[] datos = web.downloadHandler.text.Split('>');
                miUsuario.datosEjecucion = JsonUtility.FromJson<usuario.datosUsuario>(datos[1]);
                if (miUsuario.datosEjecucion.idJugador == 0)
                {
                    estadoActualConexion = estadoConexion.falleIniciarSesionDatos;
                }
                else
                {
                    estadoActualConexion = estadoConexion.termineIniciarSesion;
                }
            }
            else 
            {
                estadoActualConexion = estadoConexion.falleIniciarSesionDatos;
            }
        }
        else 
        {
            estadoActualConexion = estadoConexion.falleIniciarSesionConexion;
        }
    }

    public void registraUsuario(string email, string password, string sobrenombre, string nacimiento) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoRegistro)
        { 
            estadoActualConexion = estadoConexion.iniciandoRegistro;
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
                estadoActualConexion = estadoConexion.termineRegistro;
            }
            else 
            {
                estadoActualConexion = estadoConexion.falleRegistroDatos;
            }
        }
        else
        {
            estadoActualConexion = estadoConexion.falleRegistroConexion;
        }
    }

    public void eliminaUsuario() 
    {
        if (estadoActualConexion != estadoConexion.iniciandoEliminacion)
        {
            estadoActualConexion = estadoConexion.iniciandoEliminacion;
            StartCoroutine(elimina());
        }
    }

    private IEnumerator elimina() 
    {
        WWWForm forma = new WWWForm();
        forma.AddField("id_jugador", miUsuario.datosEjecucion.idJugador);
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
                estadoActualConexion = estadoConexion.termineEliminacion;
            }
            else
            {
                estadoActualConexion = estadoConexion.falleEliminacionDatos;
            }
        }
        else
        {
            estadoActualConexion = estadoConexion.falleEliminacionConexion;
        }
    }

    public void modificaUsuario(string sobrenombre, string nacimiento, string email, string password) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoModificacion)
        {
            estadoActualConexion = estadoConexion.iniciandoModificacion;
            StartCoroutine(modifica(sobrenombre, nacimiento, email, password));
        }
    }

    private IEnumerator modifica(string email, string password, string sobrenombre, string nacimiento)
    {
        WWWForm forma = new WWWForm();
        forma.AddField("id_jugador", miUsuario.datosEjecucion.idJugador);
        forma.AddField("sobrenombre", sobrenombre);
        forma.AddField("nacimiento", nacimiento);
        forma.AddField("email", email);
        forma.AddField("password", password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/MJ_JSON", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = estadoConexion.termineModificacion;
            }
            else
            {
                estadoActualConexion = estadoConexion.falleModificacionDatos;
            }
        }
        else
        {
            estadoActualConexion = estadoConexion.falleModificacionConexion;
        }
    }

    public void cierraSesion() 
    {
        miUsuario.reiniciaValores();
    }

}
