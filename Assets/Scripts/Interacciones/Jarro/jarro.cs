using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jarro : MonoBehaviour
{

    [Header("Manejador de animaciones")]
    [SerializeField] private Animator jarroAnimator;
    [Header("Clip cuando el jarro se rompe")]
    [SerializeField] private AnimationClip romperJarroClip;
    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio cuando se rompa el jarro")]
    [SerializeField] private AudioSource audioRomperJarron;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioRomperJarron;

    void Start()
    {
        jarroAnimator = GetComponent<Animator>();
        foreach (AnimationClip clip in jarroAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Romper Jarro")
            {
                romperJarroClip = clip;
            }
        }
    }

    public void reproduceAudio(AudioSource audio, float velocidad)
    {
        if (audio)
        {
            audioEmergente audioEmergenteTemp = Instantiate(audioEmergente, gameObject.transform.position, Quaternion.identity).GetComponent<audioEmergente>();
            audioEmergenteTemp.GetComponent<AudioSource>().clip = audio.clip;
            audioEmergenteTemp.GetComponent<AudioSource>().pitch = velocidad;
            audioEmergenteTemp.reproduceAudioClick();
        }
    }

    public void romperJarro() 
    {
        reproduceAudio(audioRomperJarron, velocidadAudioRomperJarron);
        jarroAnimator.SetBool("Romper", true);
        StartCoroutine(inhabilita(romperJarroClip.length));
    }

    private IEnumerator inhabilita(float tiempo) 
    {
        yield return new WaitForSeconds(tiempo);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("ArmaObjetoPlayer")
            && colisionDetectada.isTrigger) 
        {
            romperJarro();
        }
    }
}
