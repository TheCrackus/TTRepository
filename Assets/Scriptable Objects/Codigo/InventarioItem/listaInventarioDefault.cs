using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Inventario default", menuName = "Inventario/listaInventarioDefault")]
public class listaInventarioDefault : ScriptableObject
{
    [Header("La lista de objetos tipo item")]
    public List<inventarioItem> inventario = new List<inventarioItem>();
}
