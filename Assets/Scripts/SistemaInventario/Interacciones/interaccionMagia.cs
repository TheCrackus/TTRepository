using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteraccionMagia : MonoBehaviour
{

    [Header("La cantidad de magia que tiene el Player")]
    public ValorFlotante magiaPlayer;
    [Header("La cantidad maxima de magia que puede tener el Player")]
    public ValorFlotante magiaMaximaPlayer;
    [Header("Evento que actualiza la cantidad de magia del Player")]
    public Evento actualizaMagiaPlayer;

    public void usar(int incrementoMagia) 
    {
        magiaPlayer.valorFlotanteEjecucion += incrementoMagia;
        if (magiaPlayer.valorFlotanteEjecucion > magiaMaximaPlayer.valorFlotanteEjecucion)
        {
            magiaPlayer.valorFlotanteEjecucion = magiaMaximaPlayer.valorFlotanteEjecucion;
        }
        actualizaMagiaPlayer.invocarFunciones();
    }

}
