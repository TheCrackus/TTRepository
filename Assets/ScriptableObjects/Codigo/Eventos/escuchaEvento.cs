using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EscuchaEvento : MonoBehaviour
{

    public Evento evento;
    public UnityEvent eventoUnity;

    public void invocarEvento() 
    {
        eventoUnity.Invoke();
    }

    private void OnEnable()
    {
        evento.registrarEvento(this);
    }

    private void OnDisable()
    {
        evento.eliminarEvento(this);
    }
}
