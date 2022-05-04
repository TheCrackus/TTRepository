using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoCamara : MonoBehaviour
{

    private Animator camaraAnimator;
    private Transform objetivo;
    public float suavizado;
    public valorVectorial posicionCamaraMaxima;
    public valorVectorial posicionCamaraMinima;
    public valorVectorial posicionCamara;
    public cambioEscena estadoCambioEscena;

    void Start()
    {
        objetivo = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
        camaraAnimator = gameObject.GetComponent<Animator>();
        gameObject.transform.position = posicionCamara.valorVectorialEjecucion;
    }

    void FixedUpdate() 
    {
        if (transform.position != objetivo.position) 
        {
            Vector3 posicionObjetivo = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);
            posicionObjetivo.x = Mathf.Clamp(posicionObjetivo.x, posicionCamaraMinima.valorVectorialEjecucion.x, posicionCamaraMaxima.valorVectorialEjecucion.x);
            posicionObjetivo.y = Mathf.Clamp(posicionObjetivo.y, posicionCamaraMinima.valorVectorialEjecucion.y, posicionCamaraMaxima.valorVectorialEjecucion.y);
            transform.position = Vector3.Lerp(transform.position, posicionObjetivo, suavizado);
        }
    }

    public void empiezaAnimacionGolpePLayer() 
    {
        camaraAnimator.SetBool("RecibeGolpePlayer", true);
        StartCoroutine(animacionGolpePLayer());
    }

    private IEnumerator animacionGolpePLayer() 
    {
        yield return null;
        camaraAnimator.SetBool("RecibeGolpePlayer", false);
    }
}
