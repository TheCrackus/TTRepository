using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorBarraMagica : MonoBehaviour
{
    [Header("Objeto que representa la barra de magia")]
    public Slider barraMagica;
    [Header("La cantidad de magia que tiene el Player")]
    public valorFlotante magiaPlayer;
    [Header("La cantidad maxima de magia que tiene el Player")]
    public valorFlotante magiaPlayerMaxima;

    void Start()
    {
        inciaMagia();
        actualizaMagia();
    }

    public void inciaMagia() 
    {
        barraMagica.maxValue = magiaPlayerMaxima.valorFlotanteEjecucion;
    }

    public void actualizaMagia() 
    {
        
        barraMagica.value = magiaPlayer.valorFlotanteEjecucion;
    }
}
