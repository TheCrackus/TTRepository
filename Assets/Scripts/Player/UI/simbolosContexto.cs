using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimbolosContexto : MonoBehaviour
{
    [Header("Objeto que mostrara una imagen")]
    public GameObject simboloContexto;
    private  bool simboloEstado = false;

    public void cambiarEstadoSimbolo() 
    {
        simboloEstado = !simboloEstado;
        if (simboloEstado)
        {
            simboloContexto.SetActive(true);
        }
        else 
        {
            simboloContexto.SetActive(false);
        }
    }
}
