using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneda : incrementoEstadisticas
{

    public inventario inventarioPlayer;

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Player") && colision.isTrigger)
        {
            inventarioPlayer.numeroMonedas += 1;
            eventoIncrementoEstadistica.invocaEventosLista();
            Destroy(this.gameObject);
        }
    }
}
