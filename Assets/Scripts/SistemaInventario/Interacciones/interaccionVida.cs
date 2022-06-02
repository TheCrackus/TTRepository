using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaccionVida : MonoBehaviour
{
    [Header("La cantidad de contenedores de corazon del jugador")]
    public valorFlotante contenedorCorazones;
    [Header("La cantidad de vida que tiene el Player")]
    public valorFlotante vidaPlayer;
    [Header("Evento que actualiza la cantidad de vida del Player")]
    public evento actualizaVidaPlayer;

    public void Usa(int incrementoMagia)
    {
        vidaPlayer.valorFlotanteEjecucion += incrementoMagia;
        if (vidaPlayer.valorFlotanteEjecucion > (contenedorCorazones.valorFlotanteEjecucion * 2f))
        {
            vidaPlayer.valorFlotanteEjecucion = contenedorCorazones.valorFlotanteEjecucion * 2f;
        }
        actualizaVidaPlayer.invocaFunciones();
    }

}
