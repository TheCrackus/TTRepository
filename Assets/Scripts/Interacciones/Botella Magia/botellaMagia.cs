using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botellaMagia : incrementoEstadisticas
{

    [Header("La cantidad de magia que tiene el Player")]
    [SerializeField] private valorFlotante magiaPlayer;
    [Header("La cantidad maxima de magia que puede tener el Player")]
    [SerializeField] private valorFlotante magiaMaximaPlayer;
    [Header("Cantidad de magia a aumentar")]
    [SerializeField] private float magiaAumento;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger) 
        {
            magiaPlayer.valorFlotanteEjecucion += magiaAumento;
            if (magiaPlayer.valorFlotanteEjecucion > magiaMaximaPlayer.valorFlotanteEjecucion)
            {
                magiaPlayer.valorFlotanteEjecucion = magiaMaximaPlayer.valorFlotanteEjecucion;
            }
            getEventoIncrementoEstadistica().invocaFunciones();
            Destroy(gameObject);
        }
    }
}
