using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorCorazones : MonoBehaviour
{
    [Header("Numero de imagenes maximas")]
    [SerializeField] private Image[] imagenesCorazones;

    [Header("Sprites para la cantidad de vida")]
    [SerializeField] private Sprite corazonLleno;

    [SerializeField] private Sprite corazonMitad;

    [SerializeField] private Sprite corazonVacio;

    [Header("Numero de corazones que posee el jugador")]
    [SerializeField] private valorFlotante contenedorCorazonesMaximos;

    [Header("La vida actual del jugador")]
    [SerializeField] private valorFlotante vidaActualPlayer;

    // Start is called before the first frame update
    void Start()
    {
        actualizaCorazones();
    }

    public void iniciaCorazones() 
    {
        if (contenedorCorazonesMaximos != null) 
        {
            if (contenedorCorazonesMaximos.valorFlotanteEjecucion >= imagenesCorazones.Length)
            {
                contenedorCorazonesMaximos.valorFlotanteEjecucion = imagenesCorazones.Length;
            }
            else
            {
                if (contenedorCorazonesMaximos.valorFlotanteEjecucion <= 1)
                {
                    contenedorCorazonesMaximos.valorFlotanteEjecucion = 1;
                }
            }
            for (int i = 0; i < contenedorCorazonesMaximos.valorFlotanteEjecucion; i++)
            {
                imagenesCorazones[i].gameObject.SetActive(true);
                imagenesCorazones[i].sprite = corazonLleno;
            }
        }
    }

    public void actualizaCorazones() 
    {
        iniciaCorazones();
        float vidaTemporal = vidaActualPlayer.valorFlotanteEjecucion / 2;
        for (int i = 0; i < contenedorCorazonesMaximos.valorFlotanteEjecucion; i++)
        {
            if (i <= (vidaTemporal-1))
            {
                imagenesCorazones[i].sprite = corazonLleno;
            }
            else 
            {
                if (i >= vidaTemporal) 
                {
                    imagenesCorazones[i].sprite = corazonVacio;
                }
                else 
                {
                    imagenesCorazones[i].sprite = corazonMitad;
                }
            }
        }
    }
}
