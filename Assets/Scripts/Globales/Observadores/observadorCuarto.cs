using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservadorCuarto : MonoBehaviour
{

    [Header("Enemigos que se encuentran en esta sala")]
    [SerializeField] private Enemigo[] enemigos;

    [Header("Objetos rompibles que se encuentran en esta sala")]
    [SerializeField] private Jarro[] rompibles;

    [Header("Camara de esta sala")]
    [SerializeField] private GameObject camaraVirtual;

    [Header("Mini mapa de esta sala")]
    [SerializeField] private GameObject miniMapa;

    public Enemigo[] Enemigos { get => enemigos; set => enemigos = value; }
    public Jarro[] Rompibles { get => rompibles; set => rompibles = value; }
    public GameObject CamaraVirtual { get => camaraVirtual; set => camaraVirtual = value; }
    public GameObject MiniMapa { get => miniMapa; set => miniMapa = value; }

    private void Start()
    {
        foreach (Enemigo enemigo in enemigos)
        {
            cambiarActivacion(enemigo, false);
        }
        foreach (Jarro rompible in rompibles)
        {
            cambiarActivacion(rompible, false);
        }
        camaraVirtual.SetActive(false);
        miniMapa.SetActive(false);
    }

    public void cambiarActivacion(Component componente, bool estadoActivacion) 
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
            foreach (Enemigo enemigo in enemigos) 
            {
                cambiarActivacion(enemigo, true);
            }
            foreach (Jarro rompible in rompibles)
            {
                cambiarActivacion(rompible, true);
            }
            camaraVirtual.SetActive(true);
            miniMapa.SetActive(true);
        }
    }

    public virtual void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player") && !colisionDetectada.isTrigger)
        {
            foreach (Enemigo enemigo in enemigos)
            {
                cambiarActivacion(enemigo, false);
            }
            foreach (Jarro rompible in rompibles)
            {
                cambiarActivacion(rompible, false);
            }
            camaraVirtual.SetActive(false);
            miniMapa.SetActive(false);
        }
    }
}
