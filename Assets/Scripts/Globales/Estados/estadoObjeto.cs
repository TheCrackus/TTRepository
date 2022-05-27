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

    public estadoGenerico Estado { get => estado; set => estado = value; }

}
