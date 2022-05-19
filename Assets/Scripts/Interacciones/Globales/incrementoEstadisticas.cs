using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class incrementoEstadisticas : MonoBehaviour
{

    [Header("Evento que incrementa una estadistica del player")]
    [SerializeField] private evento eventoIncrementoEstadistica;

    public void setEventoIncrementoEstadistica(evento eventoIncrementoEstadistica) 
    {
        this.eventoIncrementoEstadistica = eventoIncrementoEstadistica;
    }

    public evento getEventoIncrementoEstadistica() 
    {
        return eventoIncrementoEstadistica;
    }

}
