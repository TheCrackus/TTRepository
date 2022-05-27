using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioProyectil : sistemaAudio
{

    [Header("Audio arroja proyectil")]
    [SerializeField] private AudioSource audioArrojaProyectil;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioArrojaProyectil;

    public void reproduceAudioProyectil()
    {
        reproduceAudio(audioArrojaProyectil, velocidadAudioArrojaProyectil);
    }

}
