using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moverEscenaPuntoControl : moverEscena
{

    [Header("Los datos de la partida en curso")]
    [SerializeField] private datosJuego datos;

    public override IEnumerator cambioEscenaOut(movimientoPlayer movP)
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
        }

        //datos.cargaObjetosScriptable();

        foreach (valorString nombre in Escenas)
        {
            if (nombre.valorStringEjecucion == EscenaActual.valorStringEjecucion)
            {
                EscenaCarga = nombre;
                break;
            }
        }
        AsyncOperation accion = SceneManager.LoadSceneAsync(EscenaCarga.valorStringEjecucion);
        while (!accion.isDone)
        {
            yield return null;
        }
    }

}
