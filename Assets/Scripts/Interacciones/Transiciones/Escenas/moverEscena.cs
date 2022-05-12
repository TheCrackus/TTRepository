using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class moverEscena : MonoBehaviour
{
    [Header("Panel que activa efecto")]
    [SerializeField] private GameObject objetoPanel;
    [Header("Manejador del animador del panel")]
    [SerializeField] private Animator panelAnimator;
    [Header("Clip Fade Out")]
    [SerializeField] private AnimationClip fadeOutClip;
    [Header("Clip Fade In")]
    [SerializeField] private AnimationClip fadeInClip;
    [Header("Contenedor de Texto")]
    [SerializeField] private GameObject objetoTextoEscena;
    [Header("Texto a mostrar en escena")]
    [SerializeField] private TextMeshProUGUI textoEscena;
    [Header("Manejador de animaciones del Texto")]
    [SerializeField] private Animator textoEscenaAnimator;
    [Header("Clip para mostrar el texto")]
    [SerializeField] private AnimationClip mostrarTextoClip;
    [Header("Clip para ocultar el texto")]
    [SerializeField] private AnimationClip ocultarTextoClip;
    [Header("Canvas temporal FadeInOut")]
    [SerializeField] private GameObject nCanvas;
    [Header("Canvas del Player")]
    [SerializeField] private GameObject pCanvas;
    [Header("Escena destino")]
    [SerializeField] private string escenaCarga;
    [Header("Nueva posicion Player")]
    [SerializeField] private Vector3 nuevaPosicionPlayer;
    [Header("Posicion del Player")]
    [SerializeField] private valorVectorial posicionPlayer;
    [Header("Estado actual de la escena")]
    [SerializeField] private cambioEscena estadoCambioEscena;
    [Header("Panel para animar la transicion")]
    [SerializeField] private GameObject fadeInFadeOutCanvas;
    [Header("Debo mostrar un texto al transicionar?")]
    [SerializeField] private bool debeMostrarTexto;
    [Header("Valor del Texto a mostrar")]
    [SerializeField] private string nombreMostrar;
    [Header("Direccion a la que apunta el Player")]
    [SerializeField] private Vector2 direccionPlayer;
    [Header("Debo comenzar un contador?")]
    [SerializeField] private bool comienzaContador;
    [Header("Debo terminar un contador?")]
    [SerializeField] private bool terminaContador;
    [Header("Debo pausar un contador?")]
    [SerializeField] private bool pausaContador;
    [Header("Nombre del objeto que termina una transicion en otra escena")]
    [SerializeField] private string nombreTransicionDestino;
    [Header("Nombre del objeto que comienza una transicion")]
    [SerializeField] private string nombreTransicionActual;
    [Header("Nueva posicion camara")]
    [SerializeField] private Vector3 nuevaPosicionCamara;
    [Header("Nueva posicion camara maxima")]
    [SerializeField] private Vector3 nuevaPosicionCamaraMaxima;
    [Header("Nueva posicion camara minima")]
    [SerializeField] private Vector3 nuevaPosicionCamaraMinima;
    [Header("Posicion camara maxima")]
    [SerializeField] private valorVectorial posicionCamaraMaxima;
    [Header("Posicion camara minima")]
    [SerializeField] private valorVectorial posicionCamaraMinima;
    [Header("Posicion camara")]
    [SerializeField] private valorVectorial posicionCamara;
    [Header("Evento que comienza un contador")]
    [SerializeField] private evento contadorRegresivoInicia;
    [Header("Evento que termina un contador")]
    [SerializeField] private evento contadorRegresivoDeten;
    [Header("Evento que reinicia un contador")]
    [SerializeField] private evento contadorRegresivoReinicia;
    [Header("Objeto con audio generico")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio de la transicion")]
    [SerializeField] private AudioSource audioTransicion;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioTransicion;

    public void Awake()
    {
        if (estadoCambioEscena.cambieEscenaEjecucion && estadoCambioEscena.nombreTansicionDestinoEjecucion == nombreTransicionActual)
        {
            iniciaCanvas();
            StartCoroutine(cambioEscenaIn());
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

    public void iniciaCanvas()
    {
        if (fadeInFadeOutCanvas != null)
        {
            if (!GameObject.FindGameObjectWithTag("CanvasEscenas"))
            {
                nCanvas = Instantiate(fadeInFadeOutCanvas, Vector3.zero, Quaternion.identity);
            }
            else
            {
                nCanvas = GameObject.FindGameObjectWithTag("CanvasEscenas");
            }
            pCanvas = GameObject.FindGameObjectWithTag("CanvasPlayer");
            objetoPanel = nCanvas.transform.Find("Panel").gameObject;
            panelAnimator = objetoPanel.GetComponent<Animator>();
            objetoTextoEscena = nCanvas.transform.Find("TextoEscenas").gameObject;
            textoEscena = objetoTextoEscena.GetComponent<TextMeshProUGUI>();
            textoEscenaAnimator = objetoTextoEscena.GetComponent<Animator>();
            foreach (AnimationClip clip in panelAnimator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "FadeOut")
                {
                    fadeOutClip = clip;
                }
                else
                {
                    if (clip.name == "FadeIn")
                    {
                        fadeInClip = clip;
                    }
                }
            }
            foreach (AnimationClip clip in textoEscenaAnimator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "Mostrar Texto")
                {
                    mostrarTextoClip = clip;
                }
                else
                {
                    if (clip.name == "Ocultar Texto")
                    {
                        ocultarTextoClip = clip;
                    }
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            movimientoPlayer movP = colisionDetectada.GetComponent<movimientoPlayer>();
            iniciaCanvas();
            StartCoroutine(cambioEscenaOut(movP));
        }
    }

    public IEnumerator cambioEscenaIn()
    {
        reproduceAudio(audioTransicion, velocidadAudioTransicion);
        GameObject.FindGameObjectWithTag("Player").GetComponent<movimientoPlayer>().setEstadoPlayer(estadoGenerico.transicionando);
        if (estadoCambioEscena.pausoContadorEjecucion)
        {
            contadorRegresivoInicia.invocaFunciones();
            estadoCambioEscena.pausoContadorEjecucion = false;
        }
        pCanvas.SetActive(false);
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);

        pCanvas.SetActive(true);
        objetoPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<movimientoPlayer>().setEstadoPlayer(estadoGenerico.ninguno);
        if (estadoCambioEscena.muestraTextoEjecucion)
        {
            objetoTextoEscena.SetActive(true);
            textoEscena.text = estadoCambioEscena.nombreTextoCuartoEjecucion;
            textoEscenaAnimator.Play("Mostrar Texto");
            estadoCambioEscena.nombreTextoCuartoEjecucion = "";
            estadoCambioEscena.muestraTextoEjecucion = false;
            yield return new WaitForSeconds(mostrarTextoClip.length);

            textoEscenaAnimator.Play("Ocultar Texto");
            yield return new WaitForSeconds(ocultarTextoClip.length);
            objetoTextoEscena.SetActive(false);
        }
        estadoCambioEscena.cambieEscenaEjecucion = false;
    }

    private IEnumerator cambioEscenaOut(movimientoPlayer movP)
    {
        reproduceAudio(audioTransicion, velocidadAudioTransicion);
        movP.setEstadoPlayer(estadoGenerico.transicionando);
        pCanvas.SetActive(false);
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(fadeOutClip.length);

        posicionPlayer.valorVectorialEjecucion = nuevaPosicionPlayer;
        estadoCambioEscena.cambieEscenaEjecucion = true;
        estadoCambioEscena.nombreTextoCuartoEjecucion = nombreMostrar;
        estadoCambioEscena.muestraTextoEjecucion = debeMostrarTexto;
        estadoCambioEscena.direccionPlayerEjecucion = direccionPlayer;
        estadoCambioEscena.nombreTansicionDestinoEjecucion = nombreTransicionDestino;
        estadoCambioEscena.ultimaEscenaGuardadaEjecucion = escenaCarga;
        posicionCamaraMaxima.valorVectorialEjecucion = nuevaPosicionCamaraMaxima;
        posicionCamaraMinima.valorVectorialEjecucion = nuevaPosicionCamaraMinima;
        posicionCamara.valorVectorialEjecucion = nuevaPosicionCamara;
        if (pausaContador)
        {
            contadorRegresivoDeten.invocaFunciones();
            estadoCambioEscena.pausoContadorEjecucion = true;
        }
        if (terminaContador)
        {
            contadorRegresivoReinicia.invocaFunciones();
        }
        AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga);
        while (!accion.isDone)
        {
            yield return null;
        }
    }
}
