using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavegacionFormularioEliminarUsuario : NavegacionGenerica
{

    [Header("Componentes graficos que permiten detectar una seleccion")]
    [SerializeField] private Selectable enterInputEliminar;

    [SerializeField] private Selectable enterInputRegresar;

    [Header("Botones de la eliminacion de usuario")]
    [SerializeField] private Button botonEliminar;

    [SerializeField] private Button botonRegresar;

    public override void Update()
    {
        base.Update();
        if (EventoSistema.currentSelectedGameObject == enterInputEliminar
                        || EventoSistema.currentSelectedGameObject == enterInputRegresar)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (EventoSistema.currentSelectedGameObject == enterInputEliminar)
                {
                    botonEliminar.onClick.Invoke();
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
