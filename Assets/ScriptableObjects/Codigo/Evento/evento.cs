using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Evento : ScriptableObject
{

    public List<EscuchaEvento> eventos = new List<EscuchaEvento>();

    public void invocarFunciones() 
    {
        foreach (EscuchaEvento evento in eventos)
        {
            evento.invocarEvento();
        }
    }

    public void registrarEvento(EscuchaEvento evento) 
    {
        eventos.Add(evento);
    }

    public void eliminarEvento(EscuchaEvento evento) 
    {
        eventos.Remove(evento);
    }
}
