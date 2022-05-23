using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class componentesGraficosModificaUsuario : componentesGraficosFormulario
{

    [Header("Componentes graficos que permiten detectar una seleccion")]
    [SerializeField] private Selectable enterInputModificar;

    [SerializeField] private Selectable enterInputRegresar;

    [Header("Botones de la modificacion de usuario")]
    [SerializeField] private Button botonModificar;

    [SerializeField] private Button botonRegresar;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField emailFiled;

    [SerializeField] private InputField passwordFiled;

    [SerializeField] private InputField passwordFieldConf;

    [SerializeField] private InputField sobrenombreFiled;

    [SerializeField] private InputField diaFiled;

    [SerializeField] private InputField mesFiled;

    [SerializeField] private InputField añoFiled;

    public InputField EmailFiled { get => emailFiled; set => emailFiled = value; }
    public InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
    public InputField PasswordFieldConf { get => passwordFieldConf; set => passwordFieldConf = value; }
    public InputField SobrenombreFiled { get => sobrenombreFiled; set => sobrenombreFiled = value; }
    public InputField DiaFiled { get => diaFiled; set => diaFiled = value; }
    public InputField MesFiled { get => mesFiled; set => mesFiled = value; }
    public InputField AñoFiled { get => añoFiled; set => añoFiled = value; }

    public override void Update()
    {
        base.Update();
        if (Sistema.currentSelectedGameObject == enterInputModificar
                        || Sistema.currentSelectedGameObject == enterInputRegresar)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Sistema.currentSelectedGameObject == enterInputModificar)
                {
                    botonModificar.onClick.Invoke();
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
