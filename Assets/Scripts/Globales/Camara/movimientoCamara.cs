using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoCamara : MonoBehaviour
{

    [SerializeField] private Animator camaraAnimator;
    [SerializeField] private Transform objetivoSeguir;
    [SerializeField] private float suavizado;
    [SerializeField] private valorVectorial posicionCamaraMaxima;
    [SerializeField] private valorVectorial posicionCamaraMinima;
    [SerializeField] private valorVectorial posicionCamara;
    [SerializeField] private cambioEscena estadoCambioEscena;

    void Start()
    {
        camaraAnimator = gameObject.GetComponent<Animator>();
        gameObject.transform.position = posicionCamara.valorVectorialEjecucion;
    }

    void FixedUpdate() 
    {
        if (transform.position != objetivoSeguir.position) 
        {
            Vector3 posicionObjetivo = new Vector3(objetivoSeguir.position.x, objetivoSeguir.position.y, gameObject.transform.position.z);
            posicionObjetivo.x = Mathf.Clamp(posicionObjetivo.x, posicionCamaraMinima.valorVectorialEjecucion.x, posicionCamaraMaxima.valorVectorialEjecucion.x);
            posicionObjetivo.y = Mathf.Clamp(posicionObjetivo.y, posicionCamaraMinima.valorVectorialEjecucion.y, posicionCamaraMaxima.valorVectorialEjecucion.y);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, posicionObjetivo, suavizado);
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
