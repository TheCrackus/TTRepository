using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionVida : MonoBehaviour
{
    [Header("La cantidad de contenedores de corazon del jugador")]
    public ValorFlotante contenedorCorazones;
    [Header("La cantidad de vida que tiene el Player")]
    public ValorFlotante vidaPlayer;
    [Header("Evento que actualiza la cantidad de vida del Player")]
    public Evento actualizaVidaPlayer;

    public void usar(int incrementoMagia)
    {
        vidaPlayer.valorFlotanteEjecucion += incrementoMagia;
        if (vidaPlayer.valorFlotanteEjecucion > (contenedorCorazones.valorFlotanteEjecucion * 2f))
        {
            vidaPlayer.valorFlotanteEjecucion = contenedorCorazones.valorFlotanteEjecucion * 2f;
        }
        actualizaVidaPlayer.invocarFunciones();
    }

}
