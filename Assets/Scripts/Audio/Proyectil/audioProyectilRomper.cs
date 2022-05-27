using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioProyectilRomper : sistemaAudio
{

    [Header("Audio arroja proyectil")]
    [SerializeField] private AudioSource audioRomper;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRomper;

    public void reproduceAudioProyectilRomper()
    {
        reproduceAudio(audioRomper, velocidadAudioRomper);
    }

}
