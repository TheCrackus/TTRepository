using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class manejadorBotonesLogIn : MonoBehaviour
{

    [SerializeField] private conexionWeb conexion;
    [Header("Pulse un boton de la interfaz?")]
    [SerializeField] private bool pulseBoton;
    [SerializeField] private InputField emailField;
    [SerializeField] private InputField passwordFiled;
    [SerializeField] private GameObject ventanaEmergente;
    [SerializeField] private string escenaPrincipal;
    [SerializeField] private string escenaRegistra;
    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al abrir una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioClickAbrir;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioClickAbrir;
    [Header("Audio al cerrar una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioClickCerrar;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioClickCerrar;

    public bool getPulseBoton()
    {
        return pulseBoton;
    }

    public void setPulseBoton(bool pulseBoton)
    {
        this.pulseBoton = pulseBoton;
    }

    void Start()
    {
        pulseBoton = false;
        conexion = gameObject.GetComponent<conexionWeb>();
    }

    public void reproduceAudio(AudioSource audio, float velocidad)
    {
        if (audio)
        {
            audioEmergente audioEmergenteTemp = Instantiate(audioEmergente, gameObject.transform.position, Quaternion.identity).GetComponent<audioEmergente>();
            audioEmergenteTemp.GetComponent<AudioSource>().clip = audio.clip;
            audioEmergenteTemp.GetComponent<AudioSource>().pitch = velocidad;
            audioEmergenteTemp.reproduceAudioClick();
        }
    }

    public void botonIniciaSesion()
    {
        if (!pulseBoton) 
        {
            reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
            conexion.iniciaSesion(emailField.text.ToString(), passwordFiled.text.ToString());
            StartCoroutine(esperaDatosInicioSesion());
            pulseBoton = true;
        }
    }

    public void botonRegistra() 
    {
        if (!pulseBoton)
        {
            reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
            StartCoroutine(cambioEscena(escenaRegistra));
            pulseBoton = true;
        }
    }

    private IEnumerator esperaDatosInicioSesion()
    {
        ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Procesando datos...", false);
        yield return new WaitWhile(() => (conexion.getEstadoActualConexion() == conexionState.iniciandoSesion));
        if (conexion.getEstadoActualConexion() == conexionState.termineIniciarSesion)
        {
            ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Datos listos...", false);
            yield return new WaitForSeconds(1f);
            conexion.setEstadoActualConexion(conexionState.ninguno);
            ventanaEmergente.GetComponent<manejadorVentanaEmergente>().cierraVentanaEmergente();
            StartCoroutine(cambioEscena(escenaPrincipal));
        }
        else
        {
            if (conexion.getEstadoActualConexion() == conexionState.falleIniciarSesionConexion)
            {
                ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("Fallo de conexión...", true);
                yield return new WaitForSeconds(1f);
                conexion.setEstadoActualConexion(conexionState.ninguno);
            }
            else 
            {
                if (conexion.getEstadoActualConexion() == conexionState.falleIniciarSesionDatos)
                {
                    ventanaEmergente.GetComponent<manejadorVentanaEmergente>().abreVentanaEmergente("El usuario no existe...", true);
                    yield return new WaitForSeconds(1f);
                    conexion.setEstadoActualConexion(conexionState.ninguno);
                }
            }
        }
        pulseBoton = false;
    }

    private IEnumerator cambioEscena(string escenaCarga) 
    {
        AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
