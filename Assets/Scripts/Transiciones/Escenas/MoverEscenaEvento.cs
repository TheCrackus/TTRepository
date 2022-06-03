using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEscenaEvento : MoverEscena
{

    [Header("Evento que activa estra transicion")]
    [SerializeField] private Evento evento;

    public override void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            evento.invocarFunciones();
        }
    }

}
