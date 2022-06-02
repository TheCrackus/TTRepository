using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadoGenerico
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

public class EstadoObjeto : MonoBehaviour
{
    [SerializeField] private EstadoGenerico estado;

    public EstadoGenerico Estado { get => estado; set => estado = value; }

}
