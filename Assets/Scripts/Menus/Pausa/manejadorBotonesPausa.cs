using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manejadorBotonesPausa : MonoBehaviour
{

    [Header("Pulse un boton de la interfaz?")]
    [SerializeField] private bool pulseBoton;
    [Header("Esta pausado el juego?")]
    [SerializeField] private bool estaPausado;
    [Header("Interfaz grafica que contiene el menu de pausa")]
    [SerializeField] private GameObject panelPausa;
    [Header("Interfaz grafica que contiene el inventario")]
    [SerializeField] private GameObject panelInventario;
    [Header("Nombre de la escena con el menu principal")]
    [SerializeField] private string escenaMenuPrincipal;
    [Header("Los datos de la partida en curso")]
    [SerializeField] private datosJuego datos;
    [Header("Objeto que contiene la informacion del juego en ejecucion")]
    [SerializeField] private cambioEscena estadoCambioEscena;
    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio al cerrar una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioClickCerrar;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioClickCerrar;
    [Header("Audio al abrir una interfaz o presionar un boton")]
    [SerializeField] private AudioSource audioClickAbrir;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioClickAbrir;

    void Start()
    {
        estaPausado = false;
        pulseBoton = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pausa")) 
        {
            if (Input.GetButtonDown("Pausa") && panelInventario.activeInHierarchy)
            {
                reproduceAudio(audioClickCerrar, velocidadAudioClickCerrar);
                abreCierraInventario();
            }
            else 
            {
                if (Input.GetButtonDown("Pausa")) 
                {
                    abreCierraMenuPausa();
                    if (panelPausa.activeInHierarchy)
                    {
                        reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
                    }
                    else 
                    {
                        reproduceAudio(audioClickCerrar, velocidadAudioClickCerrar); 
                    }
                }
            }
        }
        else 
        {
            if (Input.GetButtonDown("Inventario")) 
            {
                abreCierraInventario();
                if (panelInventario.activeInHierarchy)
                {
                    reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
                }
                else
                {
                    reproduceAudio(audioClickCerrar, velocidadAudioClickCerrar);
                }
            }
        }
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

    public void abreCierraMenuPausa() 
    {
        estaPausado = !estaPausado;
        panelPausa.SetActive(estaPausado);
        if (estaPausado)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        if (pulseBoton)
        {
            pulseBoton = false;
        }
    }

    public void abreCierraInventario()
    {
        manejadorBotonesInventario manejadorInventario = panelInventario.GetComponent<manejadorBotonesInventario>();
        manejadorInventario.activaBotonEnviaTexto("", false, null);
        estaPausado = !estaPausado;
        panelInventario.SetActive(estaPausado);
        if (estaPausado)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        if (pulseBoton) 
        {
            pulseBoton = false;
        }
    }

    public void botonRegresar() 
    {
        if (!pulseBoton) 
        {
            reproduceAudio(audioClickCerrar, velocidadAudioClickCerrar);
            abreCierraMenuPausa();
            pulseBoton = true;
        }
    }

    public void botonMenuPrincipal()
    {
        if (!pulseBoton)
        {
            reproduceAudio(audioClickCerrar, velocidadAudioClickCerrar);
            datos.reiniciaObjetosScriptable();
            StartCoroutine(cargaEscena(escenaMenuPrincipal, audioClickCerrar.clip.length));
            pulseBoton = true;
        }
    }

    public void botonInventario() 
    {
        if (!pulseBoton)
        {
            reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
            abreCierraMenuPausa();
            abreCierraInventario();
            pulseBoton = true;
        }
    }

    public void botonReiniciaGuardado() 
    {
        if (!pulseBoton)
        {
            reproduceAudio(audioClickAbrir, velocidadAudioClickAbrir);
            datos.reiniciaObjetosScriptable();
            pulseBoton = true;
        }
    }

    private IEnumerator cargaEscena(string nombreEscena, float tiempoEspera)
    {
        Time.timeScale = 1f;
        yield return new WaitForSeconds(tiempoEspera);
        AsyncOperation accion = SceneManager.LoadSceneAsync(nombreEscena);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
