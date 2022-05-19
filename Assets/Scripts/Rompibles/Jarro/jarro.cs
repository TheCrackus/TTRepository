using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jarro : MonoBehaviour
{

    [Header("Manejador de animaciones")]
    [SerializeField] private Animator jarroAnimator;
    [Header("Clip cuando el jarro se rompe")]
    [SerializeField] private AnimationClip romperJarroClip;

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
