using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoEliminaUsuario : ComponenteGraficoFormulario, IGraficoCanvasMenuPrincipal
{
    [Header("Canvas que contiene el menu principal")]
    [SerializeField] private GameObject canvasMenuPrincipal;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField passwordFiled;

    public GameObject CanvasMenuPrincipal { get => canvasMenuPrincipal; set => canvasMenuPrincipal = value; }
    public InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
}
