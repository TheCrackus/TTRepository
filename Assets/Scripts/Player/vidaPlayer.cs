using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPlayer : SistemaVida
{
    [Header("Evento que actualiza la vida en pantalla")]
    [SerializeField] private evento eventoVidaPlayer;

    [Header("Manejador para las escenas")]
    [SerializeField] private MoverEscenaPuntoControl manejadorEscenaPuntoControl;

    public override void quitarVida(float vidaMenos)
    {
        if (ManejadorAudioRecibeGolpe != null)
        {
            ManejadorAudioRecibeGolpe.reproduceAudioRecibeGolpe();
        }
        StartCoroutine(realizarFlash());
        VidaActualObjeto -= vidaMenos;
        if (VidaActualObjeto <= 0)
        {
            if (ManejadorAudioEfectoMuerte != null)
            {
                ManejadorAudioEfectoMuerte.reproduceAudioMuerte();
            }
            VidaActualObjeto = 0;
            animarMuerte();
            procesarLoot();
        }
        if (VidaObjeto != null)
        {
            VidaObjeto.valorFlotanteEjecucion = VidaActualObjeto;
        }
        eventoVidaPlayer.invocaFunciones();
        if (VidaActualObjeto <= 0)
        {
            manejadorEscenaPuntoControl.iniciarTransicionOut();
        }
    }

}
