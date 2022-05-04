using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contenedorCorazon : incrementoEstadisticas
{

    [Header("Numero de corazones que posee el jugador")]
    public valorFlotante corazonesMaximos;
    [Header("La vida actual del jugador")]
    public valorFlotante vidaActualPlayer;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger) 
        {
            corazonesMaximos.valorFlotanteEjecucion += 1;
            vidaActualPlayer.valorFlotanteEjecucion = corazonesMaximos.valorFlotanteEjecucion * 2;
            eventoIncrementoEstadistica.invocaFunciones();
            Destroy(gameObject);
        }
    }
}
