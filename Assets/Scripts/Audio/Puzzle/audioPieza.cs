using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioPieza : SistemaAudio
{

    [Header("Audio al soltar una pieza del puzzle")]
    [SerializeField] private AudioSource audioAciertaPieza;

    [Header("Velocidad y agudez del audio")]
    [SerializeField] private float velocidadAudioAciertaPieza;

    public void reproduceAciertaPieza()
    {
        reproducirAudio(audioAciertaPieza, velocidadAudioAciertaPieza);
    }

}
