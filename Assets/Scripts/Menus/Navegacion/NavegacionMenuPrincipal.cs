using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavegacionMenuPrincipal : NavegacionGenerica
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

    public override void Update()
    {
        base.Update();
        if (EventoSistema.currentSelectedGameObject == enterInputNuevaPartida
            || EventoSistema.currentSelectedGameObject == enterInputConfiguracion
            || EventoSistema.currentSelectedGameObject == enterInputModificarUsuario
            || EventoSistema.currentSelectedGameObject == enterInputEliminarUsuario
            || EventoSistema.currentSelectedGameObject == enterInputCerrarSesion)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (EventoSistema.currentSelectedGameObject == enterInputNuevaPartida)
                {
                    botonNuevaPartida.onClick.Invoke();
                }
                else
                {
                    if (EventoSistema.currentSelectedGameObject == enterInputConfiguracion)
                    {
                        botonConfiguracion.onClick.Invoke();
                    }
                    else
                    {
                        if (EventoSistema.currentSelectedGameObject == enterInputModificarUsuario)
                        {
                            botonModificarUsuario.onClick.Invoke();
                        }
                        else
                        {
                            if (EventoSistema.currentSelectedGameObject == enterInputEliminarUsuario)
                            {
                                botonEliminarUsuario.onClick.Invoke();
                            }
                            else
                            {
                                if (EventoSistema.currentSelectedGameObject == enterInputCerrarSesion)
                                {
                                    botonCerrarSesion.onClick.Invoke();
                                }
                                else
                                {
                                    if (EventoSistema.currentSelectedGameObject == enterInputCierraJuego)
                                    {
                                        botonCierraJuego.onClick.Invoke();
                                    }
                                    else
                                    {
                                        if (EventoSistema.currentSelectedGameObject == enterInputContinuarPartida)
                                        {
                                            botonContinuarPartida.onClick.Invoke();
                                        }
                                        else
                                        {
                                            if (EventoSistema.currentSelectedGameObject == enterInputNivel1)
                                            {
                                                botonNivel1.onClick.Invoke();
                                            }
                                            else
                                            {
                                                if (EventoSistema.currentSelectedGameObject == enterInputNivel2)
                                                {
                                                    botonNivel2.onClick.Invoke();
                                                }
                                                else
                                                {
                                                    if (EventoSistema.currentSelectedGameObject == enterInputNivel3)
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
