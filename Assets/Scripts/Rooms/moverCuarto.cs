using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class moverCuarto : MonoBehaviour
{

    public Vector2 cambioCamara;
    public GameObject moverCuartoRef;
    public Vector3 cambioPlayer;
    public GameObject objetoPanel;
    private movimientoCamara movCam;
    private Animator panelAnimator;
    private AnimationClip fadeOutClip;
    private AnimationClip fadeInClip;
    public bool debeMostrarTexto;
    public string nombreCuarto;
    public GameObject objetoTextoCuarto;
    public Text textoCuarto;
    private Animator textoCuartoAnimator;
    private AnimationClip mostrarTextoClip;
    private AnimationClip ocultarTextoClip;
    public bool comienzaContador;
    public bool terminaContador;

    // Start is called before the first frame update
    void Start()
    {
        movCam = Camera.main.GetComponent<movimientoCamara>();
        panelAnimator = objetoPanel.GetComponent<Animator>();
        textoCuartoAnimator = objetoTextoCuarto.GetComponent<Animator>();
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
        foreach (AnimationClip clip in textoCuartoAnimator.runtimeAnimatorController.animationClips) 
        {
            if (clip.name == "mostrarTexto")
            {
                mostrarTextoClip = clip;
            }
            else 
            {
                if (clip.name == "ocultarTexto") 
                {
                    ocultarTextoClip = clip;
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
            StartCoroutine(cambioCuarto(colisionDetectada));
        }
    }

    private IEnumerator cambioCuarto(Collider2D colisionDetectada) 
    {
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeOut");
        movimientoPlayer movP = colisionDetectada.GetComponent<movimientoPlayer>();
        movP.cambiaPermiteMovimientoNegativo();
        yield return new WaitForSeconds(fadeOutClip.length);

        colisionDetectada.transform.position = moverCuartoRef.transform.position + cambioPlayer;
        movCam.posicionMaxima += cambioCamara;
        movCam.posicionMinima += cambioCamara;
        Camera.main.transform.position = (colisionDetectada.transform.position - new Vector3(0, 0, 10));
        yield return new WaitForSeconds(1f);

        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(fadeInClip.length);

        objetoPanel.SetActive(false);
        movP.cambiaPermiteMovimientoPositivo();
        if (debeMostrarTexto)
        {

            objetoTextoCuarto.SetActive(true);
            textoCuarto.text = nombreCuarto;
            textoCuartoAnimator.Play("mostrarTexto");
            yield return new WaitForSeconds(mostrarTextoClip.length);

            textoCuartoAnimator.Play("ocultarTexto");
            yield return new WaitForSeconds(ocultarTextoClip.length);
            objetoTextoCuarto.SetActive(false);

        }

    }
}