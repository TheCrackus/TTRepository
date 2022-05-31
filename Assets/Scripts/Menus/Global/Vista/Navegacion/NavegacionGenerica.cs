using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NavegacionGenerica : MonoBehaviour
{

    private EventSystem sistema;

    [Header("El primer componente grafico de la vista")]
    [SerializeField] private Selectable primerComponenteGrafico;

    public EventSystem EventoSistema { get => sistema; set => sistema = value; }
    public Selectable PrimerComponenteGrafico { get => primerComponenteGrafico; set => primerComponenteGrafico = value; }

    public virtual void Start()
    {
        sistema = EventSystem.current;
        primerComponenteGrafico.Select();
    }

    public virtual void Update()
    {
        if (primerComponenteGrafico != null && sistema.currentSelectedGameObject != null)
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
                primerComponenteGrafico.Select();
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    primerComponenteGrafico.Select();
                }
            }
        }
    }

}
