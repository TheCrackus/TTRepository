using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavegacionFormularioRegistrarUsuario : NavegacionGenerica
{

    [Header("Componentes graficos que permiten detectar una seleccion")]
    [SerializeField] private Selectable enterInputRegistrar;

    [SerializeField] private Selectable enterInputRegresar;

    [Header("Botones del Registro")]
    [SerializeField] private Button botonRegistrar;

    [SerializeField] private Button botonRegresar;

    public override void Update()
    {
        base.Update();
        if (EventoSistema.currentSelectedGameObject == enterInputRegistrar
             || EventoSistema.currentSelectedGameObject == enterInputRegresar)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (EventoSistema.currentSelectedGameObject == enterInputRegistrar)
                {
                    botonRegistrar.onClick.Invoke();
                }
                else
                {
                    if (EventoSistema.currentSelectedGameObject == enterInputRegresar)
                    {
                        botonRegresar.onClick.Invoke();
                    }
                }
            }
        }
    }

}
