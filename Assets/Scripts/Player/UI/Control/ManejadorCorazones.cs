using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorCorazones : ManejadorMenuGenerico
{

    private ComponenteGraficoCorazones graficos;

    [Header("Numero de corazones que posee el jugador")]
    [SerializeField] private ValorFlotante contenedorCorazonesMaximos;

    [Header("La vida actual del jugador")]
    [SerializeField] private ValorFlotante vidaActualPlayer;

    private void Start()
    {
        graficos = (ComponenteGraficoCorazones) ComponenteGrafico;
        actualizarCorazones();
    }

    public void iniciarCorazones() 
    {
        if (contenedorCorazonesMaximos != null) 
        {
            if (contenedorCorazonesMaximos.valorFlotanteEjecucion >= graficos.ImagenesCorazones.Length)
            {
                contenedorCorazonesMaximos.valorFlotanteEjecucion = graficos.ImagenesCorazones.Length;
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
                graficos.ImagenesCorazones[i].gameObject.SetActive(true);
                graficos.ImagenesCorazones[i].sprite = graficos.CorazonLleno;
            }
        }
    }

    public void actualizarCorazones() 
    {
        iniciarCorazones();
        float vidaTemporal = vidaActualPlayer.valorFlotanteEjecucion / 2;
        for (int i = 0; i < contenedorCorazonesMaximos.valorFlotanteEjecucion; i++)
        {
            if (i <= (vidaTemporal-1))
            {
                graficos.ImagenesCorazones[i].sprite = graficos.CorazonLleno;
            }
            else 
            {
                if (i >= vidaTemporal) 
                {
                    graficos.ImagenesCorazones[i].sprite = graficos.CorazonVacio;
                }
                else 
                {
                    graficos.ImagenesCorazones[i].sprite = graficos.CorazonMitad;
                }
            }
        }
    }
}
