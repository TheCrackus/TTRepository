using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum estadoGenerico
{
    caminando,
    atacando,
    interactuando,
    transicionando,
    ninguno,
    estuneado,
    inactivo,
    habilidad,
    durmiendo
}

public class estadoObjeto : MonoBehaviour
{
    [SerializeField] private estadoGenerico estado;

    public void setEstadoActualObjeto(estadoGenerico nuevoEstado)
    {
        if (estado != nuevoEstado)
        {
            estado = nuevoEstado;
        }
    }

    public estadoGenerico getEstadoActualObjeto()
    {
        return estado;
    }

}
