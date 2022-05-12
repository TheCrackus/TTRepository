using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vidaPlayer : sistemaVida
{
    [Header("Evento que actualiza la vida en pantalla")]
    [SerializeField] private evento eventoVidaPlayer;
    [Header("Color cuando golpean este objeto")]
    [SerializeField] private Color colorFlash;
    [Header("Color normal de este objeto")]
    [SerializeField] private Color colorNormal;
    [Header("Tiempo que dura el efecto de golpe")]
    [SerializeField] private float tiempoFlash;
    [Header("Numero de veces que cambia de color este objeto")]
    [SerializeField] private int numeroFlash;
    [Header("La colision que  activa el efecto de golpe")]
    [SerializeField] private Collider2D colisionTrigger;
    [Header("El manejador de Sprites de este objeto")]
    [SerializeField] private SpriteRenderer spritePlayer;
    [Header("Audio al recibir daño")]
    [SerializeField] private AudioSource audioDaño;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioDaño;
    [Header("Audio de la transicion")]
    [SerializeField] private AudioSource audioTransicion;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioTransicion;
    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Panel que activa efecto")]
    [SerializeField] private GameObject objetoPanel;
    [Header("Manejador del animador del panel")]
    [SerializeField] private Animator panelAnimator;
    [Header("Clip Fade Out")]
    [SerializeField] private AnimationClip fadeOutClip;
    [Header("Canvas temporal FadeInOut")]
    [SerializeField] private GameObject nCanvas;
    [Header("Canvas del Player")]
    [SerializeField] private GameObject pCanvas;
    [Header("Estado actual de la escena")]
    [SerializeField] private cambioEscena estadoCambioEscena;
    [Header("Panel para animar la transicion")]
    [SerializeField] private GameObject fadeInFadeOutCanvas;
    [Header("Los datos de la partida en curso")]
    [SerializeField] private datosJuego datos;

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

    public override void quitaVida(float vidaMenos)
    {
        reproduceAudio(audioDaño, velocidadAudioDaño);
        base.quitaVida(vidaMenos);
        getVidaMaxima().valorFlotanteEjecucion = getVidaActual();
        eventoVidaPlayer.invocaFunciones();
        if (getVidaActual() <= 0)
        {
            movimientoPlayer movP = gameObject.GetComponentInParent<movimientoPlayer>();
            iniciaCanvas();
            StartCoroutine(cambioEscenaOut(movP));
        }
    }

    public IEnumerator flash()
    {
        int numeroFlashTemporal = 0;
        colisionTrigger.enabled = false;
        while (numeroFlashTemporal < numeroFlash)
        {
            spritePlayer.color = colorFlash;
            yield return new WaitForSeconds(tiempoFlash);
            spritePlayer.color = colorNormal;
            yield return new WaitForSeconds(tiempoFlash);
            numeroFlashTemporal++;
        }
        colisionTrigger.enabled = true;
    }

    public void actualizaVida()
    {
        setVidaActual(getVidaMaxima().valorFlotanteEjecucion);
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
            foreach (AnimationClip clip in panelAnimator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "FadeOut")
                {
                    fadeOutClip = clip;
                }
            }
        }
    }

    private IEnumerator cambioEscenaOut(movimientoPlayer movP)
    {
        reproduceAudio(audioTransicion, velocidadAudioTransicion);
        movP.setEstadoPlayer(estadoGenerico.inactivo);
        pCanvas.SetActive(false);
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(fadeOutClip.length);

        AsyncOperation accion = SceneManager.LoadSceneAsync(estadoCambioEscena.ultimaEscenaGuardadaEjecucion);
        while (!accion.isDone)
        {
            datos.reiniciaObjetosScriptable();
            getObjeto().SetActive(false);
            yield return null;
        }
    }

}
