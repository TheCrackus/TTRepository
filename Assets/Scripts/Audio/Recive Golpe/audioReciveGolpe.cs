using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioReciveGolpe : sistemaAudio
{

    [Header("Audio al recibir da�o")]
    [SerializeField] private AudioSource audioGolpe;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioGolpe;

    public void reproduceAudioRecibeGolpe() 
    {
        reproduceAudio(audioGolpe, velocidadAudioGolpe);
    }

}
