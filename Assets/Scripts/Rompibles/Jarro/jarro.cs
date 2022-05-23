using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jarro : MonoBehaviour
{

    private Animator jarroAnimator;

    private AnimationClip romperJarroClip;

    [Header("Objetos que dejara al morir")]
    [SerializeField] private tablaLoot miLoot;

    [Header("Manejador de audio rompibles")]
    [SerializeField] private audioRompible manejadorAudioRompible;

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

    public void romperJarro() 
    {
        manejadorAudioRompible.reproduceAudioRomperObjeto();
        jarroAnimator.SetBool("Romper", true);
        StartCoroutine(inhabilita(romperJarroClip.length));
    }

    private IEnumerator inhabilita(float tiempo) 
    {
        yield return new WaitForSeconds(tiempo);
        gameObject.SetActive(false);
        procesaLoot();
    }

    public void procesaLoot()
    {
        if (miLoot != null)
        {
            incrementoEstadisticas incrementoActual = miLoot.lootIncrementoEstadisticas();
            if (incrementoActual != null)
            {
                Instantiate(incrementoActual.gameObject, gameObject.transform.position, Quaternion.identity);
            }
        }
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
