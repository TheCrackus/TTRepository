using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponenteGraficoFormularioLogIn : ComponenteGraficoFormulario, IGraficoCanvasRegistroUsuario, IGraficoCanvasFormularioEnlaceProfesor, IGraficoCanvasFormularioRecuperacionContrase�a
{

    [Header("Canvas que contiene el formulario de registro")]
    [SerializeField] private GameObject canvasFormularioRegistroUsuario;

    [Header("Canvas que contiene el formulario de enlace con el profesor")]
    [SerializeField] private GameObject canvasFormularioEnlaceProfesor;

    [Header("Canvas que contiene el formulario de recuperacion de contrase�a")]
    [SerializeField] private GameObject canvasFormularioRecuperacionContrase�a;

    [Header("Componentes graficos que contienen la informacion del formulario LogIn")]
    [SerializeField] private TMP_InputField emailField;

    [SerializeField] private TMP_InputField passwordFiled;

    public GameObject CanvasFormularioRegistroUsuario { get => canvasFormularioRegistroUsuario; set => canvasFormularioRegistroUsuario = value; }
    public GameObject CanvasFormularioEnlaceProfesor { get => canvasFormularioEnlaceProfesor; set => canvasFormularioEnlaceProfesor = value; }
    public GameObject CanvasFormularioRecuperacionContrase�a { get => canvasFormularioRecuperacionContrase�a; set => canvasFormularioRecuperacionContrase�a = value; }
    public TMP_InputField EmailField { get => emailField; set => emailField = value; }
    public TMP_InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
}
