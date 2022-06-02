using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponenteGraficoFormularioLogIn : ComponenteGraficoFormulario, IGraficoCanvasRegistroUsuario, IGraficoCanvasFormularioEnlaceProfesor, IGraficoCanvasFormularioRecuperacionContraseña
{

    [Header("Canvas que contiene el formulario de registro")]
    [SerializeField] private GameObject canvasFormularioRegistroUsuario;

    [Header("Canvas que contiene el formulario de enlace con el profesor")]
    [SerializeField] private GameObject canvasFormularioEnlaceProfesor;

    [Header("Canvas que contiene el formulario de recuperacion de contraseña")]
    [SerializeField] private GameObject canvasFormularioRecuperacionContraseña;

    [Header("Componentes graficos que contienen la informacion del formulario LogIn")]
    [SerializeField] private TMP_InputField emailField;

    [SerializeField] private TMP_InputField passwordFiled;

    public GameObject CanvasFormularioRegistroUsuario { get => canvasFormularioRegistroUsuario; set => canvasFormularioRegistroUsuario = value; }
    public GameObject CanvasFormularioEnlaceProfesor { get => canvasFormularioEnlaceProfesor; set => canvasFormularioEnlaceProfesor = value; }
    public GameObject CanvasFormularioRecuperacionContraseña { get => canvasFormularioRecuperacionContraseña; set => canvasFormularioRecuperacionContraseña = value; }
    public TMP_InputField EmailField { get => emailField; set => emailField = value; }
    public TMP_InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
}
