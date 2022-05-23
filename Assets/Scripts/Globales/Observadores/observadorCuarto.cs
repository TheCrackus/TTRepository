using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class observadorCuarto : MonoBehaviour
{

    [Header("Enemigos que se encuentran en esta sala")]
    [SerializeField] private enemigo[] enemigos;

    [Header("Objetos rompibles que se encuentran en esta sala")]
    [SerializeField] private jarro[] rompibles;

    [Header("Camara de esta sala")]
    [SerializeField] private GameObject camaraVirtual;

    public enemigo[] Enemigos { get => enemigos; set => enemigos = value; }
    public jarro[] Rompibles { get => rompibles; set => rompibles = value; }
    public GameObject CamaraVirtual { get => camaraVirtual; set => camaraVirtual = value; }

    public void cambiaActivacion(Component componente, bool estadoActivacion) 
    {
        if (componente.gameObject.activeInHierarchy && !estadoActivacion)
        {
            componente.gameObject.SetActive(false);
        }
        else 
        {
            if (!componente.gameObject.activeInHierarchy && estadoActivacion) 
            {
                componente.gameObject.SetActive(true);
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada) 
    {
        if (colisionDetectada.CompareTag("Player") && !colisionDetectada.isTrigger) 
        {
            foreach (enemigo enemigo in enemigos) 
            {
                cambiaActivacion(enemigo, true);
            }
            foreach (jarro rompible in rompibles)
            {
                cambiaActivacion(rompible, true);
            }
            camaraVirtual.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player") && !colisionDetectada.isTrigger)
        {
            foreach (enemigo enemigo in enemigos)
            {
                cambiaActivacion(enemigo, false);
            }
            foreach (jarro rompible in rompibles)
            {
                cambiaActivacion(rompible, false);
            }
            camaraVirtual.SetActive(false);
        }
    }
}
