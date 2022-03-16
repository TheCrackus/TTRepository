using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverCuarto : MonoBehaviour
{

    public Vector2 cambioCamara;
    public GameObject moverCuartoRef;
    public Vector3 cambioPlayer;
    public GameObject canvasPanel;
    private movimientoCamara movCam;
    private Animator panelAnimator;
    private AnimationClip fadeOutClip;
    private AnimationClip fadeInClip;
    private movimientoPlayer movP;

    // Start is called before the first frame update
    void Start()
    {
        movCam = Camera.main.GetComponent<movimientoCamara>();
        panelAnimator = canvasPanel.GetComponent<Animator>();
        foreach (AnimationClip clip in panelAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "FadeOut")
            {
                fadeOutClip = clip;
            }
            else 
            {
                if (clip.name == "FadeIn")
                {
                    fadeInClip = clip;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player"))
        {
            StartCoroutine(esperaFadeOut(colisionDetectada,fadeOutClip.length));
            panelAnimator.Play("FadeOut");
            movP = colisionDetectada.GetComponent<movimientoPlayer>();
            movP.cambiaPermiteMovimientoNegativo();
        }
    }

    IEnumerator esperaFadeOut(Collider2D colisionDetectada, float time) 
    {
        yield return new WaitForSeconds(time);
        colisionDetectada.transform.position = moverCuartoRef.transform.position + cambioPlayer;
        movCam.posicionMaxima += cambioCamara;
        movCam.posicionMinima += cambioCamara;
        Camera.main.transform.position = (colisionDetectada.transform.position - new Vector3(0,0,10));
        StartCoroutine(esperaFadeIn(colisionDetectada,1f));
    }

    IEnumerator esperaFadeIn(Collider2D colisionDetectada, float time) 
    {
        yield return new WaitForSeconds(time);
        panelAnimator.Play("FadeIn");
        movP.cambiaPermiteMovimientoPositivo();
    }

}