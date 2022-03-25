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
            StartCoroutine(esperaFadeOut(colisionDetectada,fadeOutClip.length));
        }
    }

    private IEnumerator esperaFadeOut(Collider2D colisionDetectada, float tiempo) 
    {
        objetoPanel.SetActive(true);
        panelAnimator.Play("FadeOut");
        movimientoPlayer movP = colisionDetectada.GetComponent<movimientoPlayer>();
        movP.cambiaPermiteMovimientoNegativo();
        yield return new WaitForSeconds(tiempo);
        StartCoroutine(esperaFadeIn(colisionDetectada,1f));
    }

    private IEnumerator esperaFadeIn(Collider2D colisionDetectada, float tiempo) 
    {
        colisionDetectada.transform.position = moverCuartoRef.transform.position + cambioPlayer;
        movCam.posicionMaxima += cambioCamara;
        movCam.posicionMinima += cambioCamara;
        Camera.main.transform.position = (colisionDetectada.transform.position - new Vector3(0, 0, 10));
        yield return new WaitForSeconds(tiempo);
        StartCoroutine(ocultaPanel(colisionDetectada,fadeInClip.length));
    }

    private IEnumerator ocultaPanel(Collider2D colisionDetectada, float tiempo) 
    {
        panelAnimator.Play("FadeIn");
        yield return new WaitForSeconds(tiempo);
        objetoPanel.SetActive(false);
        movimientoPlayer movP = colisionDetectada.GetComponent<movimientoPlayer>();
        movP.cambiaPermiteMovimientoPositivo();
        if (debeMostrarTexto)
        {
            StartCoroutine(muestraNombreCuarto(mostrarTextoClip.length));
        }
    }

    private IEnumerator muestraNombreCuarto(float tiempo) 
    {
        objetoTextoCuarto.SetActive(true);
        textoCuarto.text = nombreCuarto;
        textoCuartoAnimator.Play("mostrarTexto");
        yield return new WaitForSeconds(tiempo);
        StartCoroutine(ocultaNombreCuarto(ocultarTextoClip.length));
    }

    private IEnumerator ocultaNombreCuarto(float tiempo) 
    {
        textoCuartoAnimator.Play("ocultarTexto");
        yield return new WaitForSeconds(tiempo);
        objetoTextoCuarto.SetActive(false);
    }
}