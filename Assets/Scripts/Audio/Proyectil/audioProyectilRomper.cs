using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioProyectilRomper : SistemaAudio
{

    [Header("Audio arroja proyectil")]
    [SerializeField] private AudioSource audioRomper;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRomper;

    public void reproduceAudioProyectilRomper()
    {
        reproducirAudio(audioRomper, velocidadAudioRomper);
    }

}
