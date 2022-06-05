using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoverCuarto : Transicion
{

    [Header("Vector suma a la posicion del player")]
    public Vector3 cambioSumaPoscicionPlayer;

    [Header("Lugar destino de la transicion")]
    public GameObject moverCuartoRef;

    public void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            if (EnumAccionContador == accionContador.inicia)
            {
                if (ContadorRegresivoInicia != null)
                {
                    ContadorRegresivoInicia.invocarFunciones();
                }
                if (ContadorRegresivoDeten != null)
                {
                    ContadorRegresivoDeten.invocarFunciones();
                }
                if (EscenaContador != null)
                {
                    string nombreEscenaActual = SceneManager.GetActiveScene().name;
                    EscenaContador.valorStringEjecucion = nombreEscenaActual;
                }
                if (NuevaPosicionPlayerContador != null) 
                {
                    NuevaPosicionPlayerContador.valorVectorialEjecucion = gameObject.transform.position - cambioSumaPoscicionPlayer;
                }
            }
            else 
            {
                if (EnumAccionContador == accionContador.deten)
                {
                    if (ContadorRegresivoDeten != null) 
                    {
                        ContadorRegresivoDeten.invocarFunciones();
                    }
                }
                else 
                {
                    if (EnumAccionContador == accionContador.reinicia)
                    {
                        if (ContadorRegresivoReinicia != null) 
                        {
                            ContadorRegresivoReinicia.invocarFunciones();
                        }
                    }
                }
            }
            iniciarCanvas();
            StartCoroutine(cambiarCuarto(colisionDetectada.gameObject));
        }
    }

    public IEnumerator cambiarCuarto(GameObject player)
    {

        MovimientoPlayer movP = null;

        if (ManejadorAudioTransicion != null
            && player != null
            && ObjetoPanel != null
            && PanelAnimator != null)
        {
            ManejadorAudioTransicion.reproducirAudioTransicion();
            movP = player.GetComponent<MovimientoPlayer>();
            movP.EstadoPlayer.Estado = EstadoGenerico.transicionando;
            ObjetoPanel.SetActive(true);
            PanelAnimator.Play("FadeOut");
            yield return new WaitForSeconds(FadeOutClip.length);
        }

        if (player != null
            && ManejadorAudioTransicion != null
            && PanelAnimator != null) 
        {
            establecerDireccionPlayer(player);
            player.transform.position = moverCuartoRef.transform.position + cambioSumaPoscicionPlayer;
            PosicionPlayer.valorVectorialEjecucion = player.transform.position;
            ManejadorAudioTransicion.reproducirAudioTransicion();
            PanelAnimator.Play("FadeIn");
            yield return new WaitForSeconds(FadeInClip.length);
        }

        if (ObjetoPanel != null
            && movP != null)
        {
            ObjetoPanel.SetActive(false);
            if (EnumAccionContador == accionContador.inicia
                || EnumAccionContador == accionContador.deten)
            {
                ContadorRegresivoInicia.invocarFunciones();
            }
            if (DebeMostrarTexto)
            {
                if (ObjetoTextoEscena != null
                    && TextoEscena != null
                    && TextoEscenaAnimator != null
                    && MostrarTextoClip != null)
                {
                    ObjetoTextoEscena.SetActive(true);
                    TextoEscena.text = NombreMostrar;
                    TextoEscenaAnimator.Play("Mostrar Texto");
                    yield return new WaitForSeconds(MostrarTextoClip.length);
                }

                if (TextoEscenaAnimator != null
                    && OcultarTextoClip != null)
                {
                    TextoEscenaAnimator.Play("Ocultar Texto");
                    yield return new WaitForSeconds(OcultarTextoClip.length);
                }

                if (ObjetoTextoEscena != null)
                {
                    ObjetoTextoEscena.SetActive(false);
                }
            }
            Destroy(NCanvas);
            movP.EstadoPlayer.Estado = EstadoGenerico.ninguno;
        }
    }

    public void establecerDireccionPlayer(GameObject player)
    {
        Animator playerAnimator = null;
        if (player != null) 
        {
            playerAnimator = player.GetComponent<Animator>();
        }
        if (NuevaDireccionPlayer != null) 
        {
            if (NuevaDireccionPlayer.x == 0)
            {
                if (NuevaDireccionPlayer.y > 0)
                {
                    playerAnimator.SetFloat("MovimientoX", 0f);
                    playerAnimator.SetFloat("MovimientoY", 1f);
                }
                else
                {
                    if (NuevaDireccionPlayer.y < 0)
                    {
                        playerAnimator.SetFloat("MovimientoX", 0f);
                        playerAnimator.SetFloat("MovimientoY", -1f);
                    }
                }
            }
            else
            {
                if (NuevaDireccionPlayer.y == 0)
                {
                    if (NuevaDireccionPlayer.x > 0)
                    {
                        playerAnimator.SetFloat("MovimientoX", 1f);
                        playerAnimator.SetFloat("MovimientoY", 0f);
                    }
                    else
                    {
                        if (NuevaDireccionPlayer.x < 0)
                        {
                            playerAnimator.SetFloat("MovimientoX", -1f);
                            playerAnimator.SetFloat("MovimientoY", 0f);
                        }
                    }
                }
            }
        }
    }
}