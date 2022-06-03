using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : ItemFisico
{

    [Header("Evento que incrementa una estadistica en la interfaz")]
    [SerializeField] private Evento eventoIncrementoInterfaz;

    public override void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        base.OnTriggerEnter2D(colisionDetectada);
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger)
        {
            if (eventoIncrementoInterfaz)
            {
                eventoIncrementoInterfaz.invocarFunciones();
            }
        }
    }
}
