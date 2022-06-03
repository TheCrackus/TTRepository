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

public class conexionWeb : MonoBehaviour
{

    private string[] datosJson;

    private string respuestaServidor;

    [Header("Estado de la conexion actual")]
    [SerializeField] private estadoConexion estadoActualConexion;

    [Header("Contenedor del usuario")]
    [SerializeField] private Usuario miUsuario;

    public string RespuestaServidor { get => respuestaServidor; set => respuestaServidor = value; }
    public Usuario MiUsuario { get => miUsuario; set => miUsuario = value; }
    public estadoConexion EstadoActualConexion { get => estadoActualConexion; set => estadoActualConexion = value; }
    public string[] DatosJson { get => datosJson; set => datosJson = value; }

    public void Start()
    {
        estadoActualConexion = estadoConexion.ninguno;
    }

    public void iniciarSesion(string mail, string password) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoSesion) 
        {
            estadoActualConexion = estadoConexion.iniciandoSesion;
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

    public void registrarUsuario(string mail, string password, string sobrenombre, string nacimiento) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoRegistro)
        { 
            estadoActualConexion = estadoConexion.iniciandoRegistro;
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

    public void recuperarContraseña(string mail) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoRecuperacionContraseña)
        {
            estadoActualConexion = estadoConexion.iniciandoRecuperacionContraseña;
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
                estadoActualConexion = estadoConexion.termineRecuperacionContraseña;
            }
            else
            {
                estadoActualConexion = estadoConexion.falleRecuperacionContraseñaDatos;
            }
        }
        else
        {
            estadoActualConexion = estadoConexion.falleRecuperacionContraseñaConexion;
        }
    }

    public void enviarPrueba(string respuesta1, string respuesta2, string respuesta3, string respuesta4, string respuesta5, string respuesta6) 
    {
        if (estadoActualConexion != estadoConexion.iniciandoEnvioPrueba)
        {
            estadoActualConexion = estadoConexion.iniciandoEnvioPrueba;
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
                estadoActualConexion = estadoConexion.termineEnvioPrueba;
            }
            else
            {
                estadoActualConexion = estadoConexion.falleEnvioPruebaeDatos;
            }
        }
        else
        {
            estadoActualConexion = estadoConexion.falleEnvioPruebaConexion;
        }
    }

    public void cierraSesion() 
    {
        miUsuario.reiniciarValores();
    }

}
