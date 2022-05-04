using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class manejadorEntradasLogIn : MonoBehaviour
{
    EventSystem sistema;
    public Selectable primerInput;
    public Selectable enterInputLogIn;
    public Selectable enterInputRegistro;
    public Selectable enterInputRecuperaPass;
    public Button botonLogIn;
    public Button botonRegistro;
    public Button botonRecuperaPass;

    void Start()
    {
        sistema = EventSystem.current;
        primerInput.Select();
    }

    void Update()
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
                else
                {
                    if (sistema.currentSelectedGameObject == enterInputLogIn
                        || sistema.currentSelectedGameObject == enterInputRegistro
                        || sistema.currentSelectedGameObject == enterInputRecuperaPass)
                    {
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            if (sistema.currentSelectedGameObject == enterInputLogIn)
                            {
                                botonLogIn.onClick.Invoke();
                            }
                            else
                            {
                                if (sistema.currentSelectedGameObject == enterInputRegistro)
                                {
                                    botonRegistro.onClick.Invoke();
                                }
                                else
                                {
                                    if (sistema.currentSelectedGameObject == enterInputRecuperaPass)
                                    {
                                        botonRecuperaPass.onClick.Invoke();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
