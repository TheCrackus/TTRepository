using System.Collections;
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
    iniciandoEnlace,
    termineEnlace,
    falleEnlaceDatos,
    falleEnlaceConexion,
    ninguno
}

public class conexionWeb : MonoBehaviour
{

    private string[] datosJson;

    private string respuestaServidor;

    [Header("Estado de la conexion actual")]
    [SerializeField] private estadoConexion estadoActualConexion;

    [Header("Contenedor del usuario")]
    [SerializeField] private usuario miUsuario;

    public string RespuestaServidor { get => respuestaServidor; set => respuestaServidor = value; }
    public usuario MiUsuario { get => miUsuario; set => miUsuario = value; }
    public estadoConexion EstadoActualConexion { get => estadoActualConexion; set => estadoActualConexion = value; }
    public string[] DatosJson { get => datosJson; set => datosJson = value; }

    public void Start()
    {
        estadoActualConexion = estadoConexion.ninguno;
    }

    public void iniciarSesion(string email, string password) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoSesion) 
        {
            estadoActualConexion = estadoConexion.iniciandoSesion;
            StartCoroutine(iniciar(email, password));
        }
    }

    private IEnumerator iniciar(string email, string password) 
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
                datosJson = web.downloadHandler.text.Split('>');
                miUsuario.datosEjecucion = JsonUtility.FromJson<usuario.datosUsuario>(datosJson[1]);
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

    public void registrarUsuario(string email, string password, string sobrenombre, string nacimiento) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoRegistro)
        { 
            estadoActualConexion = estadoConexion.iniciandoRegistro;
            StartCoroutine(registrar(email, password, sobrenombre, nacimiento));
        } 
    }

    private IEnumerator registrar(string email, string password, string sobrenombre, string nacimiento) 
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

    public void eliminarUsuario() 
    {
        if (estadoActualConexion != estadoConexion.iniciandoEliminacion)
        {
            estadoActualConexion = estadoConexion.iniciandoEliminacion;
            StartCoroutine(eliminar());
        }
    }

    private IEnumerator eliminar() 
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

    public void modificarUsuario(string sobrenombre, string nacimiento, string email, string password) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoModificacion)
        {
            estadoActualConexion = estadoConexion.iniciandoModificacion;
            StartCoroutine(modificar(sobrenombre, nacimiento, email, password));
        }
    }

    private IEnumerator modificar(string email, string password, string sobrenombre, string nacimiento)
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

    public void enlazarUsuario(string password)
    {
        if (estadoActualConexion != estadoConexion.iniciandoEnlace)
        {
            estadoActualConexion = estadoConexion.iniciandoEnlace;
            StartCoroutine(enlazar(password));
        }
    }

    private IEnumerator enlazar(string password)
    {
        WWWForm forma = new WWWForm();
        forma.AddField("idJugador", miUsuario.datosEjecucion.idJugador);
        forma.AddField("gPass", password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/enlaceProfesor", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = estadoConexion.termineEnlace;
            }
            else
            {
                estadoActualConexion = estadoConexion.falleEnlaceDatos;
            }
        }
        else
        {
            estadoActualConexion = estadoConexion.falleEnlaceConexion;
        }
    }

    public void cierraSesion() 
    {
        miUsuario.reiniciaValores();
    }

}
