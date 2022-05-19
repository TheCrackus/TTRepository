using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singletonEventosEscenas : MonoBehaviour
{

    public static singletonEventosEscenas instance;

    [Header("Eventos que se ejecutaran al cambiar de escena")]
    [SerializeField] private List<evento> eventos = new List<evento>();

    void Awake()
    {
        instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void agregaEvento(evento evento) 
    {
        eventos.Add(evento);
    }

    public void eliminaEventos() 
    {
        if (eventos != null
            && eventos.Count > 0)
        {
            eventos.Clear();
        }
    }

    public void ejecutaEventos() 
    {
        if (eventos != null
                && eventos.Count > 0)
        {
            foreach (evento eventoLoop in eventos)
            {
                eventoLoop.invocaFunciones();
            }
        }
    }

}
