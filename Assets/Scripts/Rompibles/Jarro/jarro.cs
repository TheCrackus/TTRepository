using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jarro : MonoBehaviour
{

    private Animator jarroAnimator;

    private AnimationClip clipRomperJarro;

    [Header("Objetos que dejara al morir")]
    [SerializeField] private TablaLoot loot;

    [Header("Manejador de audio rompibles")]
    [SerializeField] private AudioRompible manejadorAudioRompible;

    private void Start()
    {
        jarroAnimator = GetComponent<Animator>();
        foreach (AnimationClip clip in jarroAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Romper Jarro")
            {
                clipRomperJarro = clip;
            }
        }
    }

    public void romperJarro() 
    {
        manejadorAudioRompible.reproducirAudioRomperObjeto();
        jarroAnimator.SetBool("Romper", true);
        StartCoroutine(inhabilitar(clipRomperJarro.length));
    }

    private IEnumerator inhabilitar(float tiempo) 
    {
        yield return new WaitForSeconds(tiempo);
        gameObject.SetActive(false);
        procesarLoot();
    }

    public void procesarLoot()
    {
        if (loot != null)
        {
            IncrementoEstadisticas incrementoActual = loot.generarLootIncrementoEstadisticas();
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
