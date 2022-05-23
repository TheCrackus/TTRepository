using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class componentesGraficosPrincipal : componentesGraficosFormulario
{

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

    public GameObject CanvasElimina { get => canvasElimina; set => canvasElimina = value; }
    public GameObject CanvasModifica { get => canvasModifica; set => canvasModifica = value; }

    public override void Update()
    {
        base.Update();
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
