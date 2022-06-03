using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEmergente : MonoBehaviour
{
    [Header("Audio generico")]
    [SerializeField] private AudioSource audioReproducir;

    public void reproducirAudio()
    {
        if (audioReproducir)
        {
            StartCoroutine(reproducir());
        }
    }

    private IEnumerator reproducir() 
    {
        audioReproducir.Play();
        yield return new WaitForSeconds(audioReproducir.clip.length);
        Destroy(gameObject);
    }
}
