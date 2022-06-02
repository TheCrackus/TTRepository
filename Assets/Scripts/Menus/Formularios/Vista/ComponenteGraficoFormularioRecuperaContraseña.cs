using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoFormularioRecuperaContraseña : ComponenteGraficoFormulario, IGraficoCanvasLogIn
{

    [Header("Componentes graficos que contienen la informacion del formulario LogIn")]
    [SerializeField] private TMP_InputField emailField;

    [Header("Canvas que contiene el formulario de Log In")]
    [SerializeField] private GameObject canvasLogIn;

    public TMP_InputField EmailField { get => emailField; set => emailField = value; }
    public GameObject CanvasLogIn { get => canvasLogIn; set => canvasLogIn = value; }
    
}
