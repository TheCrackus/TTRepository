using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjetoMapa : SistemaAudio
{

    [Header("Audio al recojer item")]
    [SerializeField] private AudioSource audioRecojer;

    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRecojer;

    public void reproducirAudioRecojer() 
    {
        reproducirAudio(audioRecojer, velocidadAudioRecojer);
    }

}
