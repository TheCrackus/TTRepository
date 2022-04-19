using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moverCuarto : MonoBehaviour
{

    public Vector3 cambioCamara;
    public GameObject moverCuartoRef;
    public Vector3 cambioPoscicionPlayer;
    public Vector2 direccionPlayer;
    public GameObject objetoPanel;
    private movimientoCamara movCam;
    private Animator panelAnimator;
    private AnimationClip fadeOutClip;
    private AnimationClip fadeInClip;
    public bool debeMostrarTexto;
    public string nombreCuarto;
    public GameObject objetoTextoCuarto;
    public Text textoCuarto;
    private Animator textoCuartoAnimator;
    private AnimationClip mostrarTextoClip;
    private AnimationClip ocultarTextoClip;
    public bool comienzaContador;
    public bool terminaContador;
    public bool pausaContador;
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
            if (clip.name == "mostrarTexto")
            {
                mostrarTextoClip = clip;
            }
            else 
            {
                if (clip.name == "ocultarTexto") 
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
                contadorRegresivoInicia.invocaEventosLista();
            }
            if (pausaContador)
            {
                contadorRegresivoDeten.invocaEventosLista();
            }
            if (terminaContador)
            {
                contadorRegresivoReinicia.invocaEventosLista();
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
        Vector3 posMaxCam = movCam.getPosicionMaxima();
        Vector3 posMinCam = movCam.getPosicionMinima();
        movCam.setPosicionMaxima(posMaxCam += cambioCamara);
        movCam.setPosicionMinima(posMinCam += cambioCamara);
        movCam.gameObject.transform.position = new Vector3(
            movCam.gameObject.transform.position.x + cambioCamara.x,
            movCam.gameObject.transform.position.y + cambioCamara.y,
            -10);
        yield return new WaitForSeconds(1f);

        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);

        objetoPanel.SetActive(false);
        if (pausaContador) 
        {
            contadorRegresivoInicia.invocaEventosLista();
        }
        player.GetComponent<movimientoPlayer>().setEstadoActualPlayer(PlayerState.ninguno);
        if (debeMostrarTexto)
        {

            objetoTextoCuarto.SetActive(true);
            textoCuarto.text = nombreCuarto;
            textoCuartoAnimator.Play("mostrarTexto");
            yield return new WaitForSeconds(mostrarTextoClip.length);

            textoCuartoAnimator.Play("ocultarTexto");
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