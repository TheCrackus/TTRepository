using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public enum EstadoConexion
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
    iniciandoRecuperacionContraseña,
    termineRecuperacionContraseña,
    falleRecuperacionContraseñaDatos,
    falleRecuperacionContraseñaConexion,
    iniciandoEnvioPrueba,
    termineEnvioPrueba,
    falleEnvioPruebaeDatos,
    falleEnvioPruebaConexion,
    ninguno
}

public class ConexionWeb : MonoBehaviour
{

    private string[] datosJson;

    private string respuestaServidor;

    [Header("Estado de la conexion actual")]
    [SerializeField] private EstadoConexion estadoActualConexion;

    [Header("Contenedor del usuario")]
    [SerializeField] private Usuario miUsuario;

    public string RespuestaServidor { get => respuestaServidor; set => respuestaServidor = value; }
    public Usuario MiUsuario { get => miUsuario; set => miUsuario = value; }
    public EstadoConexion EstadoActualConexion { get => estadoActualConexion; set => estadoActualConexion = value; }
    public string[] DatosJson { get => datosJson; set => datosJson = value; }

    public void Start()
    {
        estadoActualConexion = EstadoConexion.ninguno;
    }

    public void iniciarSesion(string mail, string password)
    {
        if (estadoActualConexion != EstadoConexion.iniciandoSesion)
        {
            estadoActualConexion = EstadoConexion.iniciandoSesion;
            StartCoroutine(iniciar(mail, password));
        }
    }

    private IEnumerator iniciar(string mail, string password)
    {
        WWWForm forma = new WWWForm();
        forma.AddField("email", mail);
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
                miUsuario.DatosEjecucion = JsonUtility.FromJson<Usuario.DatosUsuario>(datosJson[1]);
                if (miUsuario.DatosEjecucion.idJugador == 0)
                {
                    estadoActualConexion = EstadoConexion.falleIniciarSesionDatos;
                }
                else
                {
                    estadoActualConexion = EstadoConexion.termineIniciarSesion;
                }
            }
            else
            {
                estadoActualConexion = EstadoConexion.falleIniciarSesionDatos;
            }
        }
        else
        {
            estadoActualConexion = EstadoConexion.falleIniciarSesionConexion;
        }
    }

    public void registrarUsuario(string mail, string password, string sobrenombre, string nacimiento)
    {
        if (estadoActualConexion != EstadoConexion.iniciandoRegistro)
        {
            estadoActualConexion = EstadoConexion.iniciandoRegistro;
            StartCoroutine(registrar(mail, password, sobrenombre, nacimiento));
        }
    }

    private IEnumerator registrar(string mail, string password, string sobrenombre, string nacimiento)
    {
        WWWForm forma = new WWWForm();
        forma.AddField("sobrenombre", sobrenombre);
        forma.AddField("nacimiento", nacimiento);
        forma.AddField("email", mail);
        forma.AddField("password", password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/RJ_JSON", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = EstadoConexion.termineRegistro;
            }
            else
            {
                estadoActualConexion = EstadoConexion.falleRegistroDatos;
            }
        }
        else
        {
            estadoActualConexion = EstadoConexion.falleRegistroConexion;
        }
    }

    public void eliminarUsuario()
    {
        if (estadoActualConexion != EstadoConexion.iniciandoEliminacion)
        {
            estadoActualConexion = EstadoConexion.iniciandoEliminacion;
            StartCoroutine(eliminar());
        }
    }

    private IEnumerator eliminar()
    {
        WWWForm forma = new WWWForm();
        forma.AddField("id_jugador", miUsuario.DatosEjecucion.idJugador);
        forma.AddField("email", miUsuario.DatosEjecucion.mail);
        forma.AddField("password", miUsuario.DatosEjecucion.password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/EJ_JSON", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = EstadoConexion.termineEliminacion;
            }
            else
            {
                estadoActualConexion = EstadoConexion.falleEliminacionDatos;
            }
        }
        else
        {
            estadoActualConexion = EstadoConexion.falleEliminacionConexion;
        }
    }

    public void modificarUsuario(string sobrenombre, string nacimiento, string email, string password)
    {
        if (estadoActualConexion != EstadoConexion.iniciandoModificacion)
        {
            estadoActualConexion = EstadoConexion.iniciandoModificacion;
            StartCoroutine(modificar(sobrenombre, nacimiento, email, password));
        }
    }

    private IEnumerator modificar(string mail, string password, string sobrenombre, string nacimiento)
    {
        WWWForm forma = new WWWForm();
        forma.AddField("id_jugador", miUsuario.DatosEjecucion.idJugador);
        forma.AddField("sobrenombre", sobrenombre);
        forma.AddField("nacimiento", nacimiento);
        forma.AddField("email", mail);
        forma.AddField("password", password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/MJ_JSON", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = EstadoConexion.termineModificacion;
            }
            else
            {
                estadoActualConexion = EstadoConexion.falleModificacionDatos;
            }
        }
        else
        {
            estadoActualConexion = EstadoConexion.falleModificacionConexion;
        }
    }

    public void enlazarUsuario(string password)
    {
        if (estadoActualConexion != EstadoConexion.iniciandoEnlace)
        {
            estadoActualConexion = EstadoConexion.iniciandoEnlace;
            StartCoroutine(enlazar(password));
        }
    }

    private IEnumerator enlazar(string password)
    {
        WWWForm forma = new WWWForm();
        forma.AddField("idJugador", miUsuario.DatosEjecucion.idJugador);
        forma.AddField("gPass", password);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/enlaceProfesor", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = EstadoConexion.termineEnlace;
            }
            else
            {
                estadoActualConexion = EstadoConexion.falleEnlaceDatos;
            }
        }
        else
        {
            estadoActualConexion = EstadoConexion.falleEnlaceConexion;
        }
    }

    public void recuperarContraseña(string mail)
    {
        if (estadoActualConexion != EstadoConexion.iniciandoRecuperacionContraseña)
        {
            estadoActualConexion = EstadoConexion.iniciandoRecuperacionContraseña;
            StartCoroutine(recuperar(mail));
        }
    }

    private IEnumerator recuperar(string mail)
    {
        WWWForm forma = new WWWForm();
        forma.AddField("email", mail);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/recuperarContraJugador", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = EstadoConexion.termineRecuperacionContraseña;
            }
            else
            {
                estadoActualConexion = EstadoConexion.falleRecuperacionContraseñaDatos;
            }
        }
        else
        {
            estadoActualConexion = EstadoConexion.falleRecuperacionContraseñaConexion;
        }
    }

    public void enviarPrueba(string respuesta1, string respuesta2, string respuesta3, string respuesta4, string respuesta5, string respuesta6)
    {
        if (estadoActualConexion != EstadoConexion.iniciandoEnvioPrueba)
        {
            estadoActualConexion = EstadoConexion.iniciandoEnvioPrueba;
            StartCoroutine(enviar(respuesta1, respuesta2, respuesta3, respuesta4, respuesta5, respuesta6));
        }
    }

    private IEnumerator enviar(string respuesta1, string respuesta2, string respuesta3, string respuesta4, string respuesta5, string respuesta6)
    {
        WWWForm forma = new WWWForm();
        forma.AddField("idJugador", miUsuario.DatosEjecucion.idJugador);
        forma.AddField("res1", respuesta1);
        forma.AddField("res2", respuesta2);
        forma.AddField("res3", respuesta3);
        forma.AddField("res4", respuesta4);
        forma.AddField("res5", respuesta5);
        forma.AddField("res6", respuesta6);
        UnityWebRequest web = UnityWebRequest.Post("https://tt2021-a015.herokuapp.com/guardarPruebaJugador", forma);
        yield return web.SendWebRequest();
        if (web.result != UnityWebRequest.Result.ProtocolError)
        {
            respuestaServidor = web.downloadHandler.text;
            Debug.Log(respuestaServidor);
            if (respuestaServidor.Contains("Correcto"))
            {
                estadoActualConexion = EstadoConexion.termineEnvioPrueba;
            }
            else
            {
                estadoActualConexion = EstadoConexion.falleEnvioPruebaeDatos;
            }
        }
        else
        {
            estadoActualConexion = EstadoConexion.falleEnvioPruebaConexion;
        }
    }

}
