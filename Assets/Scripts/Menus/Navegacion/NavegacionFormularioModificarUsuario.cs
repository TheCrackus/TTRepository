using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavegacionFormularioModificarUsuario : NavegacionGenerica
{

    [Header("Componentes graficos que permiten detectar una seleccion")]
    [SerializeField] private Selectable enterInputModificar;

    [SerializeField] private Selectable enterInputRegresar;

    [Header("Botones de la modificacion de usuario")]
    [SerializeField] private Button botonModificar;

    [SerializeField] private Button botonRegresar;

    public override void Update()
    {
        base.Update();
        if (EventoSistema.currentSelectedGameObject == enterInputModificar
                        || EventoSistema.currentSelectedGameObject == enterInputRegresar)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (EventoSistema.currentSelectedGameObject == enterInputModificar)
                {
                    botonModificar.onClick.Invoke();
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
