using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponenteGraficoLogIn : ComponenteGraficoFormulario, IGraficoCanvasRegistroUsuario, IGraficoCanvasFormularioEnlaceProfesor
{

    [Header("Canvas que contiene el formulario de registro")]
    [SerializeField] private GameObject canvasFormularioRegistroUsuario;

    [Header("Canvas que contiene el formulario de enlace con el profesor")]
    [SerializeField] private GameObject canvasFormularioEnlaceProfesor;

    [Header("Componentes graficos que contienen la informacion del formulario LogIn")]
    [SerializeField] private InputField emailField;

    [SerializeField] private InputField passwordFiled;

    public GameObject CanvasFormularioRegistroUsuario { get => canvasFormularioRegistroUsuario; set => canvasFormularioRegistroUsuario = value; }
    public GameObject CanvasFormularioEnlaceProfesor { get => canvasFormularioEnlaceProfesor; set => canvasFormularioEnlaceProfesor = value; }

    public InputField EmailField { get => emailField; set => emailField = value; }
    public InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }

}
