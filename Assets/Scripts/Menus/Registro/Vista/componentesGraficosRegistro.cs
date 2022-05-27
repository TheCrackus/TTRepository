using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class componentesGraficosRegistro : componentesGraficosFormulario
{

    [Header("Componentes graficos que permiten detectar una seleccion")]
    [SerializeField] private Selectable enterInputRegistrar;

    [SerializeField] private Selectable enterInputRegresar;

    [Header("Botones del Registro")]
    [SerializeField] private Button botonRegistrar;

    [SerializeField] private Button botonRegresar;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField emailFiled;

    [SerializeField] private InputField passwordFiled;

    [SerializeField] private InputField sobrenombreFiled;

    [SerializeField] private InputField diaFiled;

    [SerializeField] private InputField mesFiled;

    [SerializeField] private InputField añoFiled;

    [Header("Canvas que contiene el formulario de Log In")]
    [SerializeField] private GameObject canvasLogIn;

    public InputField EmailFiled { get => emailFiled; set => emailFiled = value; }
    public InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
    public InputField SobrenombreFiled { get => sobrenombreFiled; set => sobrenombreFiled = value; }
    public InputField DiaFiled { get => diaFiled; set => diaFiled = value; }
    public InputField MesFiled { get => mesFiled; set => mesFiled = value; }
    public InputField AñoFiled { get => añoFiled; set => añoFiled = value; }
    public GameObject CanvasLogIn { get => canvasLogIn; set => canvasLogIn = value; }

    public override void Update()
    {
        base.Update();
        if (Sistema.currentSelectedGameObject == enterInputRegistrar
                        || Sistema.currentSelectedGameObject == enterInputRegresar)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Sistema.currentSelectedGameObject == enterInputRegistrar)
                {
                    botonRegistrar.onClick.Invoke();
                }
                else
                {
                    if (Sistema.currentSelectedGameObject == enterInputRegresar)
                    {
                        botonRegresar.onClick.Invoke();
                    }
                }
            }
        }
    }

}
