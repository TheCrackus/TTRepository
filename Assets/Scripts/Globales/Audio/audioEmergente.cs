using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioEmergente : MonoBehaviour
{
    [Header("Audio generico")]
    [SerializeField] private AudioSource audioReproducir;

    public void reproduceAudioClick()
    {
        if (audioReproducir)
        {
            StartCoroutine(reproduce());
        }
    }

    private IEnumerator reproduce() 
    {
        audioReproducir.Play();
        yield return new WaitForSeconds(audioReproducir.clip.length);
        Destroy(gameObject);
    }
}
