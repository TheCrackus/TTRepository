using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class componentesGraficosLogIn : componentesGraficosFormulario
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

    [Header("Canvas que contiene el formulario de registro")]
    [SerializeField] private GameObject canvasRegistro;

    [Header("Componentes graficos que contienen la informacion del formulario LogIn")]
    [SerializeField] private InputField emailField;

    [SerializeField] private InputField passwordFiled;

    public GameObject CanvasRegistro { get => canvasRegistro; set => canvasRegistro = value; }
    public InputField EmailField { get => emailField; set => emailField = value; }
    public InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }

    public override void Update()
    {
        base.Update();
        if (Sistema.currentSelectedGameObject == enterInputLogIn
                        || Sistema.currentSelectedGameObject == enterInputRegistro
                        || Sistema.currentSelectedGameObject == enterInputRecuperaPass
                        || Sistema.currentSelectedGameObject == enterInputCierraJuego)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Sistema.currentSelectedGameObject == enterInputLogIn)
                {
                    botonLogIn.onClick.Invoke();
                }
                else
                {
                    if (Sistema.currentSelectedGameObject == enterInputRegistro)
                    {
                        botonRegistro.onClick.Invoke();
                    }
                    else
                    {
                        if (Sistema.currentSelectedGameObject == enterInputRecuperaPass)
                        {
                            botonRecuperaPass.onClick.Invoke();
                        }
                        else
                        {
                            if (Sistema.currentSelectedGameObject == enterInputCierraJuego)
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
