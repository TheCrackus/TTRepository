using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manejadorBarraMagica : MonoBehaviour
{
    [Header("Objeto que representa la barra de magia")]
    public Slider barraMagica;
    [Header("Inventario del jugador")]
    public inventario inventarioPlayer;

    void Start()
    {
        barraMagica.maxValue = inventarioPlayer.magiaInicial;
        barraMagica.value = inventarioPlayer.magiaEjecucion;
    }

    public void actualizaMagia() 
    {
        barraMagica.value = inventarioPlayer.magiaEjecucion;
    }
}
