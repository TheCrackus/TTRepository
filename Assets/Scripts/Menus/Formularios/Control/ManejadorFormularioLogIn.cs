using System.Collections;
using UnityEngine;

public class ManejadorFormularioLogIn : ManejadorFormulario, ICanvasFormularioRegistro, ICanvasFormularioEnlaceProfesor, ICanvasFormularioRecuperacionContraseña
{

    private ComponenteGraficoFormularioLogIn graficos;

    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private ValorString escenaMenuPrincipal;

    void Start()
    {
        graficos = (ComponenteGraficoFormularioLogIn)ComponenteGrafico;
        reiniciarBotones();
        if (Conexion.MiUsuario.DatosEjecucion.idJugador != 0)
        {
            Conexion.iniciarSesion(Conexion.MiUsuario.DatosEjecucion.mail, Conexion.MiUsuario.DatosEjecucion.password);
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

    public void recuperarContraseñaBoton() 
    {
        if (!PulseBoton) 
        {
            ManejadorAudioInterfazGrafica.reproducirAudioClickAbrir();
            iniciarCanvasFormularioRecuperacionContraseña();
            bloquearBotones();
            cerrarGrafico();
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
        yield return new WaitWhile(() => Conexion.EstadoActualConexion == EstadoConexion.iniciandoSesion);
        if (Conexion.EstadoActualConexion == EstadoConexion.termineIniciarSesion)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Datos listos...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = EstadoConexion.ninguno;
            comprobarEstadoEnlaceProfesor(Conexion.DatosJson[0]);
        }
        else
        {
            if (Conexion.EstadoActualConexion == EstadoConexion.falleIniciarSesionConexion)
            {
                ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Fallo de conexión, comprueba el estado de tu red de internet.");
                yield return new WaitForSeconds(1f);
                Conexion.EstadoActualConexion = EstadoConexion.ninguno;
            }
            else 
            {
                if (Conexion.EstadoActualConexion == EstadoConexion.falleIniciarSesionDatos)
                {
                    if (Conexion.RespuestaServidor == "NO VERIFICADO")
                    {
                        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Tu usuario no está verificado, por favor, " +
                            "verifica tu cuenta ingresando al correo electrónico registrado.");
                    }
                    else 
                    {
                        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Los datos ingresados no pertenecen a ninguna cuenta registrada en el sistema.");
                    }
                    yield return new WaitForSeconds(1f);
                    Conexion.EstadoActualConexion = EstadoConexion.ninguno;
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

    public void iniciarCanvasFormularioRecuperacionContraseña()
    {
        if (!GameObject.FindGameObjectWithTag("CanvasRecuperacion"))
        {
            Instantiate(graficos.CanvasFormularioRecuperacionContraseña, Vector3.zero, Quaternion.identity);
        }
    }
}
