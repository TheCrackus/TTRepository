using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class componentesGraficosFormulario : componentesGraficos, ventanaEmergente, ordenInputs
{

    private EventSystem sistema;

    [Header("El primer componente grafico del formulario")]
    [SerializeField] private Selectable primerInput;

    [Header("Canvas que contiene una ventana emergente")]
    [SerializeField] private GameObject canvasVentanaEmergente;

    public EventSystem Sistema { get => sistema; set => sistema = value; }
    public Selectable PrimerInput { get => primerInput; set => primerInput = value; }
    public GameObject CanvasVentanaEmergente { get => canvasVentanaEmergente; set => canvasVentanaEmergente = value; }

    public virtual void Start()
    {
        sistema = EventSystem.current;
        primerInput.Select();
    }


    public virtual void Update()
    {
        if (primerInput != null && sistema.currentSelectedGameObject != null)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
            {
                Selectable anterior = sistema.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
                if (anterior != null)
                {
                    anterior.Select();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    Selectable siguiente = sistema.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
                    if (siguiente != null)
                    {
                        siguiente.Select();
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
            {
                primerInput.Select();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    primerInput.Select();
                }
            }
        }
    }

}
