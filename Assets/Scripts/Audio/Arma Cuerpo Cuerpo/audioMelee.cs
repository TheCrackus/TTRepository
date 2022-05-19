using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioMelee : sistemaAudio
{

    [Header("Audio atacar con espada")]
    [SerializeField] private AudioSource audioAtaqueMelee;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAtaqueMelee;

    public void reproduceAudioMelee()
    {
        reproduceAudio(audioAtaqueMelee, velocidadAudioAtaqueMelee);
    }

}
