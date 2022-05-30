using System.Collections;
using UnityEngine;

public class ManejadorFormularioLogIn : ManejadorFormulario, ICanvasFormularioRegistro
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
            StartCoroutine(cambiarEscena(escenaMenuPrincipal.valorStringEjecucion));
        }
    }

    public void iniciarSesionBoton()
    {
        if (!PulseBoton) 
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            Conexion.iniciaSesion(graficos.EmailField.text.ToString(), graficos.PasswordFiled.text.ToString() );
            StartCoroutine(esperarDatosInicioSesion());
            bloquearBotones();
        }
    }

    public void registrarUsuarioBoton() 
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickAbrir();
            iniciarCanvasRegistrarUsuario();
            bloquearBotones();
            cerrarGrafico();
        }
    }

    public void cerrarJuegoBoton()
    {
        if (!PulseBoton)
        {
            ManejadorAudioInterfazGrafica.reproduceAudioClickCerrar();
            bloquearBotones();
            Application.Quit();
        }
    }


    private IEnumerator esperarDatosInicioSesion()
    {
        iniciarVentanaEmergente();
        ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Procesando datos...");
        yield return new WaitWhile(() => (Conexion.EstadoActualConexion == estadoConexion.iniciandoSesion));
        if (Conexion.EstadoActualConexion == estadoConexion.termineIniciarSesion)
        {
            ManejadorVentanaEmergente.enviarTextoVentanaEmergente("Datos listos...");
            yield return new WaitForSeconds(1f);
            Conexion.EstadoActualConexion = estadoConexion.ninguno;
            //falta reiniciar guardado
            StartCoroutine(cambiarEscena(escenaMenuPrincipal.valorStringEjecucion));
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

}
