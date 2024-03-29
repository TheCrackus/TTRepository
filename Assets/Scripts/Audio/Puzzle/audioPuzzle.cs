using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPuzzle : SistemaAudio
{

    [Header("Audio al tomar una pieza del puzzle")]
    [SerializeField] private AudioSource audioTomaPieza;

    [Header("Velocidad y agudez del audio")]
    [SerializeField] private float velocidadAudioTomaPieza;

    [Header("Audio al soltar una pieza del puzzle")]
    [SerializeField] private AudioSource audioSueltaPieza;

    [Header("Velocidad y agudez del audio")]
    [SerializeField] private float velocidadAudioSueltaPieza;

    public void reproduceTomaPieza() 
    {
        reproducirAudio(audioTomaPieza, velocidadAudioTomaPieza);
    }

    public void reproduceSueltaPieza() 
    {
        reproducirAudio(audioSueltaPieza, velocidadAudioSueltaPieza);
    }

}
