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
        if (estadoCambioEscena != null && nombreTransicionActual != null) 
        {
            if (estadoCambioEscena.cambieEscenaEjecucion && estadoCambioEscena.nombreTansicionDestinoEjecucion == nombreTransicionActual.valorStringEjecucion)
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
            if (estadoCambioEscena != null)
            {
                estadoCambioEscena.pausoContadorEjecucion = true;
            }
            EscenaActual.valorStringEjecucion = SceneManager.GetActiveScene().name;
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
                if (estadoCambioEscena != null)
                {
                    estadoCambioEscena.pausoContadorEjecucion = true;
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
            movP.EstadoPlayer.Estado = estadoGenerico.transicionando;
        }
        if (estadoCambioEscena != null)
        {
            if (estadoCambioEscena.pausoContadorEjecucion)
            {
                if (ContadorRegresivoInicia != null)
                {
                    ContadorRegresivoInicia.invocaFunciones();
                }
                estadoCambioEscena.pausoContadorEjecucion = false;
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
                movP.EstadoPlayer.Estado = estadoGenerico.ninguno;
            }
            if (estadoCambioEscena != null)
            {
                if (estadoCambioEscena.muestraTextoEjecucion)
                {
                    if (ObjetoTextoEscena != null && TextoEscena != null && TextoEscenaAnimator != null)
                    {
                        ObjetoTextoEscena.SetActive(true);
                        TextoEscena.text = estadoCambioEscena.nombreTextoCuartoEjecucion;
                        TextoEscenaAnimator.Play("Mostrar Texto");
                        estadoCambioEscena.nombreTextoCuartoEjecucion = "";
                        estadoCambioEscena.muestraTextoEjecucion = false;
                        yield return new WaitForSeconds(MostrarTextoClip.length);

                        TextoEscenaAnimator.Play("Ocultar Texto");
                        yield return new WaitForSeconds(OcultarTextoClip.length);
                        ObjetoTextoEscena.SetActive(false);
                    }
                }
                if (estadoCambioEscena != null) 
                {
                    if (estadoCambioEscena.cambieEscenaEjecucion)
                    {
                        estadoCambioEscena.cambieEscenaEjecucion = false;
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
            movP.EstadoPlayer.Estado = estadoGenerico.transicionando;
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
                PosicionPlayer.valorVectorialEjecucion = nuevaPosicionPlayer;
                if (EstadoCambioEscena != null)
                {
                    estadoCambioEscena.cambieEscenaEjecucion = true;
                    estadoCambioEscena.nombreTextoCuartoEjecucion = NombreMostrar;
                    estadoCambioEscena.muestraTextoEjecucion = DebeMostrarTexto;
                    estadoCambioEscena.direccionPlayerEjecucion = NuevaDireccionPlayer;
                    estadoCambioEscena.nombreTansicionDestinoEjecucion = nombreTransicionDestino.valorStringEjecucion;
                    EscenaActual.valorStringEjecucion = escenaCarga.valorStringEjecucion;
                }
            }
        }

        AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga.valorStringEjecucion);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

}
