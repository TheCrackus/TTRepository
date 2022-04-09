using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simbolosContexto : MonoBehaviour
{

    public GameObject simboloContexto;
    private  bool simboloEstado = false;

    public void cambiaEstadoSimbolo() 
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
