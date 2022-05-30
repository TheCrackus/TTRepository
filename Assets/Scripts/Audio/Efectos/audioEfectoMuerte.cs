using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioEfectoMuerte : sistemaAudio
{

    [Header("Audio de efecto de muerte")]
    [SerializeField] private AudioSource audioMuerte;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioEfectoMuerte;

    public void reproduceAudioMuerte() 
    {
        reproduceAudio(audioMuerte, velocidadAudioEfectoMuerte);
    }

}