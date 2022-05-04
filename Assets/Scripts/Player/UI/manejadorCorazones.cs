using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorCorazones : MonoBehaviour
{
    [Header("Numero de imagenes maximas")]
    public Image[] imagenesCorazones;
    [Header("Sprites para la cantidad de vida")]
    public Sprite corazonLleno;
    public Sprite corazonMitad;
    public Sprite corazonVacio;
    [Header("Numero de corazones que posee el jugador")]
    public valorFlotante corazonesMaximos;
    [Header("La vida actual del jugador")]
    public valorFlotante vidaActualPlayer;

    // Start is called before the first frame update
    void Start()
    {
        iniciaCorazones();
        actualizaCorazones();
    }

    public void iniciaCorazones() 
    {
        if (corazonesMaximos.valorFlotanteEjecucion >= imagenesCorazones.Length)
        {
            corazonesMaximos.valorFlotanteEjecucion = imagenesCorazones.Length;
        }
        else 
        {
            if (corazonesMaximos.valorFlotanteEjecucion <= 1)
            {
                corazonesMaximos.valorFlotanteEjecucion = 1;
            }
        }
        for (int i = 0; i < corazonesMaximos.valorFlotanteEjecucion; i++) 
        {
            imagenesCorazones[i].gameObject.SetActive(true);
            imagenesCorazones[i].sprite = corazonLleno;
        }
    }

    public void actualizaCorazones() 
    {
        iniciaCorazones();
        float vidaTemporal = vidaActualPlayer.valorFlotanteEjecucion / 2;
        for (int i = 0; i < corazonesMaximos.valorFlotanteEjecucion; i++)
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
