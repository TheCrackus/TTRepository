using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioObjetoMapa : SistemaAudio
{

    [Header("Audio al recojer item")]
    [SerializeField] private AudioSource audioRecojer;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRecojer;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger)
        {
            reproducirAudio(audioRecojer, velocidadAudioRecojer);
        }
    }

}
