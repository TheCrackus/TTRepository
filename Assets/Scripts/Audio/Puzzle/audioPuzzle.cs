using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPuzzle : sistemaAudio
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
        reproduceAudio(audioTomaPieza, velocidadAudioTomaPieza);
    }

    public void reproduceSueltaPieza() 
    {
        reproduceAudio(audioSueltaPieza, velocidadAudioSueltaPieza);
    }

}
