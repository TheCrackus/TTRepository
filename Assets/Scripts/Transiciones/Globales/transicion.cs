using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum accionContador
{
    inicia,
    reinicia,
    deten,
    ninguno
}

public enum tipoTransicion
{
    cuarto,
    escena
}

public class transicion : MonoBehaviour
{

    private GameObject objetoPanel;

    private Animator panelAnimator;

    private AnimationClip fadeOutClip;

    private AnimationClip fadeInClip;

    private GameObject objetoTextoEscena;

    private TextMeshProUGUI textoEscena;

    private Animator textoEscenaAnimator;

    private AnimationClip mostrarTextoClip;

    private AnimationClip ocultarTextoClip;

    private GameObject nCanvas;

    [Header("Panel para animar la transicion")]
    [SerializeField] private GameObject fadeInFadeOutCanvas;

    [Header("Posicion del Player")]
    [SerializeField] private valorVectorial posicionPlayer;

    [Header("Direccion a la que apuntara el Player")]
    [SerializeField] private Vector2 nuevaDireccionPlayer;

    [Header("Debo mostrar un texto al transicionar?")]
    [SerializeField] private bool debeMostrarTexto;

    [Header("Texto a mostrar")]
    [SerializeField] private string nombreMostrar;

    [Header("Evento que comienza un contador")]
    [SerializeField] private evento contadorRegresivoInicia;

    [Header("Evento que termina un contador")]
    [SerializeField] private evento contadorRegresivoDeten;

    [Header("Evento que reinicia un contador")]
    [SerializeField] private evento contadorRegresivoReinicia;

    [Header("Manejador de audio de la transicion")]
    [SerializeField] private audioTransicion manejadorAudioTransicion;

    [Header("Tipo de interaccion con el contador")]
    [SerializeField] accionContador enumAccionContador;

    [Header("El tipo de transicion")]
    [SerializeField] tipoTransicion enumTipoTransicion;

    [Header("La escena actual en ejecucion")]
    [SerializeField] valorString escenaActual;

    [Header("Nombre de las escenas del videojuego")]
    [SerializeField] valorString[] escenas;

    public accionContador EnumAccionContador { get => enumAccionContador; set => enumAccionContador = value; }
    public tipoTransicion EnumTipoTransicion { get => enumTipoTransicion; set => enumTipoTransicion = value; }
    public GameObject ObjetoPanel { get => objetoPanel; set => objetoPanel = value; }
    public Animator PanelAnimator { get => panelAnimator; set => panelAnimator = value; }
    public AnimationClip FadeOutClip { get => fadeOutClip; set => fadeOutClip = value; }
    public AnimationClip FadeInClip { get => fadeInClip; set => fadeInClip = value; }
    public GameObject ObjetoTextoEscena { get => objetoTextoEscena; set => objetoTextoEscena = value; }
    public TextMeshProUGUI TextoEscena { get => textoEscena; set => textoEscena = value; }
    public Animator TextoEscenaAnimator { get => textoEscenaAnimator; set => textoEscenaAnimator = value; }
    public AnimationClip MostrarTextoClip { get => mostrarTextoClip; set => mostrarTextoClip = value; }
    public AnimationClip OcultarTextoClip { get => ocultarTextoClip; set => ocultarTextoClip = value; }
    public GameObject NCanvas { get => nCanvas; set => nCanvas = value; }
    public GameObject FadeInFadeOutCanvas { get => fadeInFadeOutCanvas; set => fadeInFadeOutCanvas = value; }
    public valorVectorial PosicionPlayer { get => posicionPlayer; set => posicionPlayer = value; }
    public Vector2 NuevaDireccionPlayer { get => nuevaDireccionPlayer; set => nuevaDireccionPlayer = value; }
    public bool DebeMostrarTexto { get => debeMostrarTexto; set => debeMostrarTexto = value; }
    public string NombreMostrar { get => nombreMostrar; set => nombreMostrar = value; }
    public evento ContadorRegresivoInicia { get => contadorRegresivoInicia; set => contadorRegresivoInicia = value; }
    public evento ContadorRegresivoDeten { get => contadorRegresivoDeten; set => contadorRegresivoDeten = value; }
    public evento ContadorRegresivoReinicia { get => contadorRegresivoReinicia; set => contadorRegresivoReinicia = value; }
    public audioTransicion ManejadorAudioTransicion { get => manejadorAudioTransicion; set => manejadorAudioTransicion = value; }
    public valorString EscenaActual { get => escenaActual; set => escenaActual = value; }
    public valorString[] Escenas { get => escenas; set => escenas = value; }

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
                Destroy(nCanvas);
                nCanvas = Instantiate(fadeInFadeOutCanvas, Vector3.zero, Quaternion.identity);
            }
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
}