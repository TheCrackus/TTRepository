using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaccionMagia : MonoBehaviour
{

    [Header("La cantidad de magia que tiene el Player")]
    public valorFlotante magiaPlayer;
    [Header("La cantidad maxima de magia que puede tener el Player")]
    public valorFlotante magiaMaximaPlayer;
    [Header("Evento que actualiza la cantidad de magia del Player")]
    public evento actualizaMagiaPlayer;

    public void Usa(int incrementoMagia) 
    {
        magiaPlayer.valorFlotanteEjecucion += incrementoMagia;
        if (magiaPlayer.valorFlotanteEjecucion > magiaMaximaPlayer.valorFlotanteEjecucion)
        {
            magiaPlayer.valorFlotanteEjecucion = magiaMaximaPlayer.valorFlotanteEjecucion;
        }
        actualizaMagiaPlayer.invocaFunciones();
    }

}
