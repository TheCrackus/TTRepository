using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class componentesGraficosPrincipal : componentesGraficos, ventanaEmergente, ordenInputs
{

    private EventSystem sistema;

    [Header("El primer componente grafico del formulario")]
    [SerializeField] private Selectable primerInput;

    [Header("Componentes graficos que permiten detectar una seleccion")]
    [SerializeField] private Selectable enterInputContinuarPartida;

    [SerializeField] private Selectable enterInputNuevaPartida;

    [SerializeField] private Selectable enterInputNivel1;

    [SerializeField] private Selectable enterInputNivel2;

    [SerializeField] private Selectable enterInputNivel3;

    [SerializeField] private Selectable enterInputConfiguracion;

    [SerializeField] private Selectable enterInputModificarUsuario;

    [SerializeField] private Selectable enterInputEliminarUsuario;

    [SerializeField] private Selectable enterInputCerrarSesion;

    [SerializeField] private Selectable enterInputCierraJuego;

    [Header("Botones del menu principal")]
    [SerializeField] private Button botonContinuarPartida;

    [SerializeField] private Button botonNuevaPartida;

    [SerializeField] private Button botonNivel1;

    [SerializeField] private Button botonNivel2;

    [SerializeField] private Button botonNivel3;

    [SerializeField] private Button botonConfiguracion;

    [SerializeField] private Button botonModificarUsuario;

    [SerializeField] private Button botonEliminarUsuario;

    [SerializeField] private Button botonCerrarSesion;

    [SerializeField] private Button botonCierraJuego;

    [Header("Canvas que contiene el formulario de eliminacion")]
    [SerializeField] private GameObject canvasElimina;

    [Header("Canvas que contiene el formulario de modificacion")]
    [SerializeField] private GameObject canvasModifica;

    [Header("Canvas que contiene una ventana emergente")]
    [SerializeField] private GameObject canvasVentanaEmergente;

    public EventSystem Sistema { get => sistema; set => sistema = value; }
    public Selectable PrimerInput { get => primerInput; set => primerInput = value; }
    public GameObject CanvasElimina { get => canvasElimina; set => canvasElimina = value; }
    public GameObject CanvasModifica { get => canvasModifica; set => canvasModifica = value; }
    public GameObject CanvasVentanaEmergente { get => canvasVentanaEmergente; set => canvasVentanaEmergente = value; }

    public void Start()
    {
        sistema = EventSystem.current;
        primerInput.Select();
    }

    public void Update()
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
        if (Sistema.currentSelectedGameObject == enterInputNuevaPartida 
            || Sistema.currentSelectedGameObject == enterInputConfiguracion
            || Sistema.currentSelectedGameObject == enterInputModificarUsuario
            || Sistema.currentSelectedGameObject == enterInputEliminarUsuario
            || Sistema.currentSelectedGameObject == enterInputCerrarSesion)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Sistema.currentSelectedGameObject == enterInputNuevaPartida)
                {
                    botonNuevaPartida.onClick.Invoke();
                }
                else
                {
                    if (Sistema.currentSelectedGameObject == enterInputConfiguracion)
                    {
                        botonConfiguracion.onClick.Invoke();
                    }
                    else
                    {
                        if (Sistema.currentSelectedGameObject == enterInputModificarUsuario)
                        {
                            botonModificarUsuario.onClick.Invoke();
                        }
                        else
                        {
                            if (Sistema.currentSelectedGameObject == enterInputEliminarUsuario)
                            {
                                botonEliminarUsuario.onClick.Invoke();
                            }
                            else
                            {
                                if (Sistema.currentSelectedGameObject == enterInputCerrarSesion)
                                {
                                    botonCerrarSesion.onClick.Invoke();
                                }
                                else 
                                {
                                    if (Sistema.currentSelectedGameObject == enterInputCierraJuego)
                                    {
                                        botonCierraJuego.onClick.Invoke();
                                    }
                                    else 
                                    {
                                        if (Sistema.currentSelectedGameObject == enterInputContinuarPartida)
                                        {
                                            botonContinuarPartida.onClick.Invoke();
                                        }
                                        else 
                                        {
                                            if (Sistema.currentSelectedGameObject == enterInputNivel1)
                                            {
                                                botonNivel1.onClick.Invoke();
                                            }
                                            else 
                                            {
                                                if (Sistema.currentSelectedGameObject == enterInputNivel2)
                                                {
                                                    botonNivel2.onClick.Invoke();
                                                }
                                                else
                                                {
                                                    if (Sistema.currentSelectedGameObject == enterInputNivel3)
                                                    {
                                                        botonNivel3.onClick.Invoke();
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
    }
}
