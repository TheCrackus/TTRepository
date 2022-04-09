using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class inventario : ScriptableObject
{

    public objeto objetoActual;
    public List<objeto> objetos = new List<objeto>();
    public int numeroLlaves;

    public void agregaObjeto(objeto objetoAgrega) 
    {
        if (objetoAgrega.esLlave)
        {
            numeroLlaves++;
        }
        else 
        {
            if (!objetos.Contains(objetoAgrega)) 
            {
                objetos.Add(objetoAgrega);
            }
        }
    }

}
