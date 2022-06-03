using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementoEstadisticas : MonoBehaviour
{

    [Header("Evento que incrementa una estadistica del player")]
    [SerializeField] private Evento eventoIncrementoEstadistica;

    [Header("Manejador de audio del objeto")]
    [SerializeField] private AudioObjetoMapa manejadorAudioObjetoMapa;

    public Evento EventoIncrementoEstadistica { get => eventoIncrementoEstadistica; set => eventoIncrementoEstadistica = value; }
    public AudioObjetoMapa ManejadorAudioObjetoMapa { get => manejadorAudioObjetoMapa; set => manejadorAudioObjetoMapa = value; }
}
