using System.Collections;
using UnityEngine;

public class ManejadorFormularioLogIn : ManejadorFormulario, ICanvasFormularioRegistro, ICanvasFormularioEnlaceProfesor
{

    private ComponenteGraficoLogIn graficos;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private valorString escenaMenuPrincipal;

    void Start()
    {
        graficos = (ComponenteGraficoLogIn)ComponenteGrafico;
        reiniciarBotones();
        if (Conexion.MiUsuario.datosEjecucion.idJugador != 0)
        {
            Conexion.iniciarSesion(Conexion.MiUsuario.datosEjecucion.mail, Conexion.MiUsuario.datosEjecucion.password);
            StartCoroutine(esperarDatosInicioSesion());
        }
    }

    public void iniciarSesionBoton()
    {
        if (!PulseBoton) 
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            Conexion.iniciarSesion(graficos.EmailField.text.ToString(), graficos.PasswordFiled.text.ToString());
            StartCoroutine(esperarDatosInicioSesion());
            bloquearBotones();
        }
    }

    public void registrarUsuarioBoton() 
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            iniciarCanvasRegistrarUsuario();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void cerrarJuegoBoton()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickCerrar();
            bloquearBotones();
            Application.Quit();
        }
    }

    public void comprobarEstadoEnlaceProfesor(string dato) 
    {
        if (dato == "False")
        {
            iniciarCanvasFormularioEnlaceProfesor();
            cerrarGrafico();
        }
        else
        {
            StartCoroutine(cambiarEscena(escenaMenuPrincipal.valorStringEjecucion));
        }
    }

    private IEnumerator esperarDatosInicioSesion()
    {
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => Conexion.EstadoActualConexion == estadoConexion.iniciandoSesion);
        if (Conexion.EstadoActualConexion == estadoConexion.termineIniciarSesion)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Datos listos...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            comprobarEstadoEnlaceProfesor(Conexion.DatosJson[0]);
        }
        else
        {
            if (Conexion.EstadoActualConexion == estadoConexion.falleIniciarSesionConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexión...");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = estadoConexion.ninguno;
            }
            else 
            {
                if (Conexion.EstadoActualConexion == estadoConexion.falleIniciarSesionDatos)
                {
                    if (Conexion.RespuestaServidor == "NO VERIFICADO")
                    {
                        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Tu usuario no está verificado, por favor, " +
                            "verifica tu cuenta ingresando al correo electrónico registrado...");
                    }
                    else 
                    {
                        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("El usuario no existe...");
                    }
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = estadoConexion.ninguno;
                }
            }
        }
        reiniciarBotones();
    }

    public void iniciarCanvasRegistrarUsuario()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasRegistro"))
        {
            Instantiate(graficos.CanvasFormularioRegistroUsuario, Vector3.zero, Quaternion.identity);
        }
    }

    public void iniciarCanvasFormularioEnlaceProfesor()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasEnlaceProfesor"))
        {
            Instantiate(graficos.CanvasFormularioEnlaceProfesor, Vector3.zero, Quaternion.identity);
        }
    }
}
