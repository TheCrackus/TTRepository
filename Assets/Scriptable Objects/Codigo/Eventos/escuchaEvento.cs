using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class escuchaEvento : MonoBehaviour
{

    public evento evento;
    public UnityEvent eventoUnity;

    public void invocaEvento() 
    {
        eventoUnity.Invoke();
    }

    private void OnEnable()
    {
        evento.registraEvento(this);
    }

    private void OnDisable()
    {
        evento.eliminaEvento(this);
    }
}
