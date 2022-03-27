using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class evento : ScriptableObject
{

    public List<escuchaEvento> eventos = new List<escuchaEvento>();

    public void altaEvento() 
    {
        foreach (escuchaEvento evento in eventos)
        {

            evento.alzaEvento();
        }
    }

    public void registraEvento(escuchaEvento evento) 
    {
        eventos.Add(evento);
    }

    public void eliminaEvento(escuchaEvento evento) 
    {
        eventos.Remove(evento);
    }
}
