using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jarro : MonoBehaviour
{

    private Animator jarroAnimator;
    private AnimationClip romperJarroClip;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Romper() 
    {
        jarroAnimator.SetBool("Romper", true);
        StartCoroutine(inhabilita(romperJarroClip.length));
    }

    private IEnumerator inhabilita(float tiempo) 
    {
        yield return new WaitForSeconds(tiempo);
        gameObject.SetActive(false);
    }
}
