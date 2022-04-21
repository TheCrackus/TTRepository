using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorCorazones : MonoBehaviour
{
    [Header("Numero de imagenes maximas")]
    public Image[] corazones;
    [Header("Sprites para la cantidad de vida")]
    public Sprite corazonLleno;
    public Sprite corazonMitad;
    public Sprite corazonVacio;
    [Header("Estadisticas de salud del player")]
    public valorFlotante corazonesMaximos;
    public valorFlotante vidaActualPlayer;

    // Start is called before the first frame update
    void Start()
    {
        iniciaCorazones();
    }

    public void iniciaCorazones() 
    {
        for (int i = 0; i < corazonesMaximos.valorInicial; i++) 
        {
            corazones[i].gameObject.SetActive(true);
            corazones[i].sprite = corazonLleno;
        }
    }

    public void actualizaCorazones() 
    {
        float vidaTemporal = vidaActualPlayer.valorEjecucion / 2;
        for (int i = 0; i < corazonesMaximos.valorInicial; i++)
        {
            if (i <= (vidaTemporal-1))
            {
                corazones[i].sprite = corazonLleno;
            }
            else 
            {
                if (i >= vidaTemporal) 
                {
                    corazones[i].sprite = corazonVacio;
                }
                else 
                {
                    corazones[i].sprite = corazonMitad;
                }
            }
        }
    }
}
