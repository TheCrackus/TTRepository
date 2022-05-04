using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class manejadorEntradasPrincipal : MonoBehaviour
{

    EventSystem sistema;
    public Selectable primerInput;
    public Selectable enterInputNuevaPartida;
    public Selectable enterInputConfiguracion;
    public Selectable enterInputModificarUsuario;
    public Selectable enterInputEliminarUsuario;
    public Selectable enterInputCerrarSesion;
    public Button botonNuevaPartida;
    public Button botonConfiguracion;
    public Button botonModificarUsuario;
    public Button botonEliminarUsuario;
    public Button botonCerrarSesion;

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
                    if (sistema.currentSelectedGameObject == enterInputNuevaPartida
                        || sistema.currentSelectedGameObject == enterInputConfiguracion
                        || sistema.currentSelectedGameObject == enterInputModificarUsuario
                        || sistema.currentSelectedGameObject == enterInputEliminarUsuario
                        || sistema.currentSelectedGameObject == enterInputCerrarSesion)
                    {
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            if (sistema.currentSelectedGameObject == enterInputNuevaPartida)
                            {
                                botonNuevaPartida.onClick.Invoke();
                            }
                            else
                            {
                                if (sistema.currentSelectedGameObject == enterInputConfiguracion)
                                {
                                    botonConfiguracion.onClick.Invoke();
                                }
                                else
                                {
                                    if (sistema.currentSelectedGameObject == enterInputModificarUsuario)
                                    {
                                        botonModificarUsuario.onClick.Invoke();
                                    }
                                    else
                                    {
                                        if (sistema.currentSelectedGameObject == enterInputEliminarUsuario)
                                        {
                                            botonEliminarUsuario.onClick.Invoke();
                                        }
                                        else
                                        {
                                            if (sistema.currentSelectedGameObject == enterInputCerrarSesion)
                                            {
                                                botonCerrarSesion.onClick.Invoke();
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
    }
}
