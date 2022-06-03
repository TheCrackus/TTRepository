using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MoverEscena : Transicion
{

    [Header("Nombre del objeto que termina una transicion en otra escena")]
    [SerializeField] private ValorString nombreTransicionDestino;

    [Header("Nombre del objeto que comienza una transicion")]
    [SerializeField] private ValorString nombreTransicionActual;

    [Header("Estado actual de la escena")]
    [SerializeField] private CambioEscena estadoCambioEscena;

    [Header("Escena destino")]
    [SerializeField] private ValorString escenaCarga;

    [Header("Nueva posicion Player")]
    [SerializeField] private Vector3 nuevaPosicionPlayer;

    public ValorString NombreTransicionDestino { get => nombreTransicionDestino; set => nombreTransicionDestino = value; }
    public ValorString NombreTransicionActual { get => nombreTransicionActual; set => nombreTransicionActual = value; }
    public CambioEscena EstadoCambioEscena { get => estadoCambioEscena; set => estadoCambioEscena = value; }
    public ValorString EscenaCarga { get => escenaCarga; set => escenaCarga = value; }
    public Vector3 NuevaPosicionPlayer { get => nuevaPosicionPlayer; set => nuevaPosicionPlayer = value; }

    public virtual void Start()
    {
        iniciarTransicionIn();
    }

    public virtual void iniciarTransicionIn()
    {
        if (estadoCambioEscena != null && nombreTransicionActual != null) 
        {
            if (estadoCambioEscena.cambieEscenaEjecucion && estadoCambioEscena.nombreTansicionDestinoEjecucion == nombreTransicionActual.valorStringEjecucion)
            {
                iniciarCanvas();
                MovimientoPlayer movP = null;
                if (GameObject.FindGameObjectWithTag("Player"))
                {
                    movP = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
                }
                StartCoroutine(cambiarEscenaIn(movP));
            }
        }
    }

    public virtual void iniciarTransicionOut() 
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
            if (estadoCambioEscena != null)
            {
                estadoCambioEscena.pausoContadorEjecucion = true;
            }
            EscenaActual.valorStringEjecucion = SceneManager.GetActiveScene().name;
            ManejadorContador manejadorC = GameObject.FindGameObjectWithTag("ManejadorContador").GetComponent<ManejadorContador>();
            MoverEscena moverE = manejadorC.GetComponent<MoverEscena>();
            foreach (ValorString nombre in Escenas)
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
                    ContadorRegresivoDeten.invocarFunciones();
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
                        ContadorRegresivoReinicia.invocarFunciones();
                    }
                }
            }
        }
        iniciarCanvas();
        MovimientoPlayer movP = null;
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            movP = GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>();
        }
        StartCoroutine(cambiarEscenaOut(movP));
    }

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            iniciarTransicionOut();
        }
    }

    public virtual IEnumerator cambiarEscenaIn(MovimientoPlayer movP)
    {
        if (ManejadorAudioTransicion != null
            && movP != null
            && estadoCambioEscena != null
            && ObjetoPanel != null
            && PanelAnimator != null
            && FadeInClip != null) 
        {
            ManejadorAudioTransicion.reproducirAudioTransicion();
            movP.EstadoPlayer.Estado = EstadoGenerico.transicionando;
            if (estadoCambioEscena.pausoContadorEjecucion)
            {
                if (ContadorRegresivoInicia != null)
                {
                    ContadorRegresivoInicia.invocarFunciones();
                }
                estadoCambioEscena.pausoContadorEjecucion = false;
            }
            ObjetoPanel.SetActive(true);
            PanelAnimator.Play("FadeIn");
            yield return new WaitForSeconds(FadeInClip.length);
        }

        if (ObjetoPanel != null
            && movP != null
            && estadoCambioEscena != null) 
        {
            ObjetoPanel.SetActive(false);
            if (estadoCambioEscena.cambieEscenaEjecucion)
            {
                estadoCambioEscena.cambieEscenaEjecucion = false;
            }
            if (estadoCambioEscena.muestraTextoEjecucion)
            {
                if (ObjetoTextoEscena != null 
                    && TextoEscena != null 
                    && TextoEscenaAnimator != null)
                {
                    ObjetoTextoEscena.SetActive(true);
                    TextoEscena.text = estadoCambioEscena.nombreTextoCuartoEjecucion;
                    TextoEscenaAnimator.Play("Mostrar Texto");
                    estadoCambioEscena.nombreTextoCuartoEjecucion = "";
                    estadoCambioEscena.muestraTextoEjecucion = false;
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

    public virtual IEnumerator cambiarEscenaOut(MovimientoPlayer movP)
    {
        if (ManejadorAudioTransicion != null
            && movP != null
            && ObjetoPanel != null
            && PanelAnimator != null
            && FadeOutClip != null)
        {
            ManejadorAudioTransicion.reproducirAudioTransicion();
            movP.EstadoPlayer.Estado = EstadoGenerico.transicionando;
            ObjetoPanel.SetActive(true);
            PanelAnimator.Play("FadeOut");
            yield return new WaitForSeconds(FadeOutClip.length);   
        }

        if (PosicionPlayer != null 
            && estadoCambioEscena != null 
            && EscenaActual != null
            && escenaCarga != null)
        {
            PosicionPlayer.valorVectorialEjecucion = nuevaPosicionPlayer;
            estadoCambioEscena.cambieEscenaEjecucion = true;
            estadoCambioEscena.nombreTextoCuartoEjecucion = NombreMostrar;
            estadoCambioEscena.muestraTextoEjecucion = DebeMostrarTexto;
            estadoCambioEscena.direccionPlayerEjecucion = NuevaDireccionPlayer;
            estadoCambioEscena.nombreTansicionDestinoEjecucion = nombreTransicionDestino.valorStringEjecucion;
            EscenaActual.valorStringEjecucion = escenaCarga.valorStringEjecucion;
            AsyncOperation accion = SceneManager.LoadSceneAsync(escenaCarga.valorStringEjecucion);
            while (!accion.isDone)
            {
                yield return null;
            }
        }
        
    }

}
