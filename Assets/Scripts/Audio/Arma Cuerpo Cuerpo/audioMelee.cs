using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMelee : SistemaAudio
{

    [Header("Audio atacar con espada")]
    [SerializeField] private AudioSource audioAtaqueMelee;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioAtaqueMelee;

    public void reproducirAudioMelee()
    {
        reproducirAudio(audioAtaqueMelee, velocidadAudioAtaqueMelee);
    }

}
