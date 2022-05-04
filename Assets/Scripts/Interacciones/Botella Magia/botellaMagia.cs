using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botellaMagia : incrementoEstadisticas
{

    [Header("Inventario del jugador")]
    public inventario inventarioPlayer;
    [Header("Cantidad de magia a aumentar")]
    public float magiaAumento;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger) 
        {
            inventarioPlayer.aumentaMagia(magiaAumento);
            eventoIncrementoEstadistica.invocaFunciones();
            Destroy(gameObject);
        }
    }
}
