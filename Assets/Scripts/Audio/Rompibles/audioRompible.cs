using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioRompible : sistemaAudio
{

    [Header("Audio cuando se rompe el objeto")]
    [SerializeField] private AudioSource audioRomperObjeto;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRomperObjeto;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("ArmaObjetoPlayer")
            && colisionDetectada.isTrigger)
        {
            reproduceAudio(audioRomperObjeto, velocidadAudioRomperObjeto);
        }
    }

}
