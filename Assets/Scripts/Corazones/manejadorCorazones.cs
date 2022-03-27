using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorCorazones : MonoBehaviour
{

    public Image[] corazones;
    public Sprite corazonLleno;
    public Sprite corazonMitad;
    public Sprite corazonVacio;
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
