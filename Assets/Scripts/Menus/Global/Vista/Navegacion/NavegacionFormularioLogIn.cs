using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavegacionFormularioLogIn : NavegacionGenerica
{

    [Header("Componentes graficos que permiten detectar una seleccion")]
    [SerializeField] private Selectable enterInputLogIn;

    [SerializeField] private Selectable enterInputRegistro;

    [SerializeField] private Selectable enterInputRecuperaPass;

    [SerializeField] private Selectable enterInputCierraJuego;

    [Header("Botones del Log In")]
    [SerializeField] private Button botonLogIn;

    [SerializeField] private Button botonRegistro;

    [SerializeField] private Button botonRecuperaPass;

    [SerializeField] private Button botonCierraJuego;

    public override void Update()
    {
        base.Update();
        if (EventoSistema.currentSelectedGameObject == enterInputLogIn
                        || EventoSistema.currentSelectedGameObject == enterInputRegistro
                        || EventoSistema.currentSelectedGameObject == enterInputRecuperaPass
                        || EventoSistema.currentSelectedGameObject == enterInputCierraJuego)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (EventoSistema.currentSelectedGameObject == enterInputLogIn)
                {
                    botonLogIn.onClick.Invoke();
                }
                else
                {
                    if (EventoSistema.currentSelectedGameObject == enterInputRegistro)
                    {
                        botonRegistro.onClick.Invoke();
                    }
                    else
                    {
                        if (EventoSistema.currentSelectedGameObject == enterInputRecuperaPass)
                        {
                            botonRecuperaPass.onClick.Invoke();
                        }
                        else
                        {
                            if (EventoSistema.currentSelectedGameObject == enterInputCierraJuego)
                            {
                                botonCierraJuego.onClick.Invoke();
                            }
                        }
                    }
                }
            }
        }
    }

}
