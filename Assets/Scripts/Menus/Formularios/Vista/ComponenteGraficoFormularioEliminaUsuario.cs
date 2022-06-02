using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoFormularioEliminaUsuario : ComponenteGraficoFormulario, IGraficoCanvasMenuPrincipal
{
    [Header("Canvas que contiene el menu principal")]
    [SerializeField] private GameObject canvasMenuPrincipal;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private TMP_InputField passwordFiled;

    public GameObject CanvasMenuPrincipal { get => canvasMenuPrincipal; set => canvasMenuPrincipal = value; }
    public TMP_InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
}
