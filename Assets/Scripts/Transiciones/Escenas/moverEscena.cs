using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class moverEscena : transicion
{

    [Header("Nombre del objeto que termina una transicion en otra escena")]
    [SerializeField] private valorString nombreTransicionDestino;

    [Header("Nombre del objeto que comienza una transicion")]
    [SerializeField] private valorString nombreTransicionActual;

    [Header("Estado actual de la escena")]
    [SerializeField] private cambioEscena estadoCambioEscena;

    [Header("Escena destino")]
    [SerializeField] private valorString escenaCarga;

    [Header("Nueva posicion Player")]
    [SerializeField] private Vector3 nuevaPosicionPlayer;

    public valorString NombreTransicionDestino { get => nombreTransicionDestino; set => nombreTransicionDestino = value; }
    public valorString NombreTransicionActual { get => nombreTransicionActual; set => nombreTransicionActual = value; }
    public cambioEscena EstadoCambioEscena { get => estadoCambioEscena; set => estadoCambioEscena = value; }
    public valorString EscenaCarga { get => escenaCarga; set => escenaCarga = value; }
    public Vector3 NuevaPosicionPlayer { get => nuevaPosicionPlayer; set => nuevaPosicionPlayer = value; }

    public virtual void Awake()
    {
        iniciaTransicionIn();
    }

    public void iniciaTransicionIn()
    {
        if (EstadoCambioEscena != null && NombreTransicionActual != null) 
        {
            if (EstadoCambioEscena.cambieEscenaEjecucion && EstadoCambioEscena.nombreTansicionDestinoEjecucion == NombreTransicionActual.valorStringEjecucion)
            {
                iniciaCanvas();
                movimientoPlayer movP = null;
                if (GameObject.FindGameObjectWithTag("Player"))
                {
                    movP = GameObject.FindGameObjectWithTag("Player").GetComponent<movimientoPlayer>();
                }
                StartCoroutine(cambioEscenaIn(movP));
            }
        }
    }

    public void iniciaTransicionOut() 
    {
        if (EnumAccionContador == accionContador.inicia)
        {
            if (ContadorRegresivoInicia != null)
            {
                ContadorRegresivoInicia.invocaFunciones();
            }
            if (ContadorRegresivoDeten != null)
            {
                ContadorRegresivoDeten.invocaFunciones();
            }
            if (EstadoCambioEscena != null)
            {
                EstadoCambioEscena.pausoContadorEjecucion = true;
            }
            manejadorContador manejadorC = GameObject.FindGameObjectWithTag("ManejadorContador").GetComponent<manejadorContador>();
            moverEscena moverE = manejadorC.GetComponent<moverEscena>();
            foreach (valorString nombre in Escenas)
            {
                if (nombre.valorStringEjecucion == EscenaActual.valorStringEjecucion)
                {
                    moverE.EscenaCarga = nombre;
                    break;
                }
            }
            moverE.NuevaDireccionPlayer = new Vector2(NuevaDireccionPlayer.x * (-1), NuevaDireccionPlayer.y * (-1));
            moverE.NuevaPosicionPlayer = gameObject.transform.position + new Vector3(NuevaDireccionPlayer.x * (-1), NuevaDireccionPlayer.y * (-1), 0);
        }
        else
        {
            if (EnumAccionContador == accionContador.deten)
            {
                if (ContadorRegresivoDeten != null)
                {
                    ContadorRegresivoDeten.invocaFunciones();
                }
                if (EstadoCambioEscena != null)
                {
                    EstadoCambioEscena.pausoContadorEjecucion = true;
                }
            }
            else
            {
                if (EnumAccionContador == accionContador.reinicia)
                {
                    if (ContadorRegresivoReinicia != null)
                    {
                        ContadorRegresivoReinicia.invocaFunciones();
                    }
                }
            }
        }
        iniciaCanvas();
        movimientoPlayer movP = null;
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            movP = GameObject.FindGameObjectWithTag("Player").GetComponent<movimientoPlayer>();
        }
        StartCoroutine(cambioEscenaOut(movP));
    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            iniciaTransicionOut();
        }
    }

    public virtual IEnumerator cambioEscenaIn(movimientoPlayer movP)
    {
        if (ManejadorAudioTransicion != null) 
        {
            ManejadorAudioTransicion.reproduceAudioTransicion();
        }
        if (movP != null)
        {
            movP.setEstadoPlayer(estadoGenerico.transicionando);
        }
        if (EstadoCambioEscena != null)
        {
            if (EstadoCambioEscena.pausoContadorEjecucion)
            {
                if (ContadorRegresivoInicia != null)
                {
                    ContadorRegresivoInicia.invocaFunciones();
                }
                EstadoCambioEscena.pausoContadorEjecucion = false;
            }
        }
        if (ObjetoPanel != null && PanelAnimator != null && FadeInClip != null)
        {
            ObjetoPanel.SetActive(true);
            PanelAnimator.Play("FadeIn");
            yield return new WaitForSeconds(FadeInClip.length);

            ObjetoPanel.SetActive(false);
            if (movP)
            {
                movP.setEstadoPlayer(estadoGenerico.ninguno);
            }
            if (EstadoCambioEscena != null)
            {
                if (EstadoCambioEscena.muestraTextoEjecucion)
                {
                    if (ObjetoTextoEscena != null && TextoEscena != null && TextoEscenaAnimator != null)
                    {
                        ObjetoTextoEscena.SetActive(true);
                        TextoEscena.text = EstadoCambioEscena.nombreTextoCuartoEjecucion;
                        TextoEscenaAnimator.Play("Mostrar Texto");
                        EstadoCambioEscena.nombreTextoCuartoEjecucion = "";
                        EstadoCambioEscena.muestraTextoEjecucion = false;
                        yield return new WaitForSeconds(MostrarTextoClip.length);

                        TextoEscenaAnimator.Play("Ocultar Texto");
                        yield return new WaitForSeconds(OcultarTextoClip.length);
                        ObjetoTextoEscena.SetActive(false);
                    }
                }
                if (EstadoCambioEscena != null) 
                {
                    if (EstadoCambioEscena.cambieEscenaEjecucion)
                    {
                        EstadoCambioEscena.cambieEscenaEjecucion = false;
                    }
                }
            }
        }
        Destroy(NCanvas);
    }

    public virtual IEnumerator cambioEscenaOut(movimientoPlayer movP)
    {
        if (ManejadorAudioTransicion != null)
        {
            ManejadorAudioTransicion.reproduceAudioTransicion();
        }
        if (movP)
        {
            movP.setEstadoPlayer(estadoGenerico.transicionando);
        }
        if (ObjetoPanel != null
             && PanelAnimator != null
             && FadeOutClip != null)
        {
            ObjetoPanel.SetActive(true);
            PanelAnimator.Play("FadeOut");
            yield return new WaitForSeconds(FadeOutClip.length);

            if (PosicionPlayer != null)
            {
                PosicionPlayer.valorVectorialEjecucion = NuevaPosicionPlayer;
                if (EstadoCambioEscena != null)
                {
                    EstadoCambioEscena.cambieEscenaEjecucion = true;
                    EstadoCambioEscena.nombreTextoCuartoEjecucion = NombreMostrar;
                    EstadoCambioEscena.muestraTextoEjecucion = DebeMostrarTexto;
                    EstadoCambioEscena.direccionPlayerEjecucion = NuevaDireccionPlayer;
                    EstadoCambioEscena.nombreTansicionDestinoEjecucion = NombreTransicionDestino.valorStringEjecucion;
                    EscenaActual.valorStringEjecucion = EscenaCarga.valorStringEjecucion;
                }
            }
        }

        AsyncOperation accion = SceneManager.LoadSceneAsync(EscenaCarga.valorStringEjecucion);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

}
