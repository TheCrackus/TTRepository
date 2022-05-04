using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class moverCuarto : MonoBehaviour
{
    private movimientoCamara movCam;
    private Animator panelAnimator;
    private AnimationClip fadeOutClip;
    private AnimationClip fadeInClip;
    private Animator textoCuartoAnimator;
    private AnimationClip mostrarTextoClip;
    private AnimationClip ocultarTextoClip;
    [Header("Valores para limites de la camara")]
    public Vector3 cambioCamara;
    public valorVectorial posicionCamaraMaxima;
    public valorVectorial posicionCamaraMinima;
    public valorVectorial posicionCamara;
    [Header("Objeto de destino en la transicion")]
    public GameObject moverCuartoRef;
    [Header("Valores para la posicion del player")]
    public Vector3 cambioPoscicionPlayer;
    public Vector2 direccionPlayer;
    public valorVectorial posicionPlayer;
    [Header("Panel para animar la transicion")]
    public GameObject objetoPanel;
    [Header("Valores para mostrar el titulo de los escenarios")]
    public bool debeMostrarTexto;
    public string nombreCuarto;
    public GameObject objetoTextoCuarto;
    public TextMeshProUGUI textoCuarto;
    [Header("Tipo de interaccion con el contador")]
    public bool comienzaContador;
    public bool terminaContador;
    public bool pausaContador;
    [Header("Eventos para controlar el ontador")]
    public evento contadorRegresivoInicia;
    public evento contadorRegresivoDeten;
    public evento contadorRegresivoReinicia;


    public void Start()
    {
        movCam = Camera.main.GetComponent<movimientoCamara>();
        panelAnimator = objetoPanel.GetComponent<Animator>();
        textoCuartoAnimator = objetoTextoCuarto.GetComponent<Animator>();
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
        foreach (AnimationClip clip in textoCuartoAnimator.runtimeAnimatorController.animationClips) 
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

    public void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            movimientoPlayer movP = colisionDetectada.GetComponent<movimientoPlayer>();
            movP.setEstadoActualPlayer(PlayerState.interactuando);
            if (comienzaContador)
            {
                contadorRegresivoInicia.invocaFunciones();
            }
            if (pausaContador)
            {
                contadorRegresivoDeten.invocaFunciones();
            }
            if (terminaContador)
            {
                contadorRegresivoReinicia.invocaFunciones();
            }
            StartCoroutine(cambioCuarto(colisionDetectada.gameObject));
        }
    }

    public IEnumerator cambioCuarto(GameObject player) 
    {
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeOut");
        yield return new WaitForSeconds(fadeOutClip.length);

        estableceDireccionPlayer(player);
        player.transform.position = moverCuartoRef.transform.position + cambioPoscicionPlayer;
        posicionPlayer.valorVectorialEjecucion = player.transform.position;
        posicionCamaraMaxima.valorVectorialEjecucion = posicionCamaraMaxima.valorVectorialEjecucion += cambioCamara;
        posicionCamaraMinima.valorVectorialEjecucion = posicionCamaraMinima.valorVectorialEjecucion += cambioCamara;
        movCam.gameObject.transform.position = new Vector3(
        movCam.gameObject.transform.position.x + cambioCamara.x,
        movCam.gameObject.transform.position.y + cambioCamara.y,
        -10);
        posicionCamara.valorVectorialEjecucion = movCam.gameObject.transform.position;
        yield return new WaitForSeconds(1f);

        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);

        objetoPanel.SetActive(false);
        if (pausaContador) 
        {
            contadorRegresivoInicia.invocaFunciones();
        }
        player.GetComponent<movimientoPlayer>().setEstadoActualPlayer(PlayerState.ninguno);
        if (debeMostrarTexto)
        {

            objetoTextoCuarto.SetActive(true);
            textoCuarto.text = nombreCuarto;
            textoCuartoAnimator.Play("Mostrar Texto");
            yield return new WaitForSeconds(mostrarTextoClip.length);

            textoCuartoAnimator.Play("Ocultar Texto");
            yield return new WaitForSeconds(ocultarTextoClip.length);
            objetoTextoCuarto.SetActive(false);

        }
    }

    public void estableceDireccionPlayer(GameObject player) 
    {
        Animator playerAnimator = player.GetComponent<Animator>();
        if (direccionPlayer.x == 0)
        {
            if (direccionPlayer.y > 0)
            {
                playerAnimator.SetFloat("MovimientoX", 0f);
                playerAnimator.SetFloat("MovimientoY", 1f);
            }
            else
            {
                if (direccionPlayer.y < 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 0f);
                    playerAnimator.SetFloat("MovimientoY", -1f);
                }
            }
        }
        else
        {
            if (direccionPlayer.y == 0)
            {
                if (direccionPlayer.x > 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 1f);
                    playerAnimator.SetFloat("MovimientoY", 0f);
                }
                else
                {
                    if (direccionPlayer.x < 0)
                    {
                        playerAnimator.SetFloat("MovimientoX", -1f);
                        playerAnimator.SetFloat("MovimientoY", 0f);
                    }
                }
            }
        }
    }
}