using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorBarraMagica : ManejadorMenuGenerico
{

    private ComponenteGraficoBarraMagica graficos;

    [Header("La cantidad de magia que tiene el Player")]
    public ValorFlotante magiaPlayer;

    [Header("La cantidad maxima de magia que tiene el Player")]
    public ValorFlotante magiaPlayerMaxima;

    private void Start()
    {
        graficos = (ComponenteGraficoBarraMagica) ComponenteGrafico;
        inciarMagia();
        actualizarMagia();
    }

    public void inciarMagia() 
    {
        graficos.BarraMagica.maxValue = magiaPlayerMaxima.valorFlotanteEjecucion;
    }

    public void actualizarMagia() 
    {

        graficos.BarraMagica.value = magiaPlayer.valorFlotanteEjecucion;
    }
}
