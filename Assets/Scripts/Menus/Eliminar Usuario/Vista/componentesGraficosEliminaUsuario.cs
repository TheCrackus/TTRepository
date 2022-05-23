using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class componentesGraficosEliminaUsuario : componentesGraficosFormulario
{

    [Header("Componentes graficos que permiten detectar una seleccion")]
    [SerializeField] private Selectable enterInputEliminar;

    [SerializeField] private Selectable enterInputRegresar;

    [Header("Botones de la eliminacion de usuario")]
    [SerializeField] private Button botonEliminar;

    [SerializeField] private Button botonRegresar;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField passwordFiled;

    public InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }

    public override void Update()
    {
        base.Update();
        if (Sistema.currentSelectedGameObject == enterInputEliminar
                        || Sistema.currentSelectedGameObject == enterInputRegresar)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Sistema.currentSelectedGameObject == enterInputEliminar)
                {
                    botonEliminar.onClick.Invoke();
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
