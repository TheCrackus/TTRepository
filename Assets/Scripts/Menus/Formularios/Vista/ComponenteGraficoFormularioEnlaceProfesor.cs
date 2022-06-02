using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoFormularioEnlaceProfesor : ComponenteGraficoFormulario, IGraficoCanvasLogIn
{

    [Header("Canvas que contiene el formulario de Log In")]
    [SerializeField] private GameObject canvasLogIn;

    [Header("Componentes graficos que contienen la informacion del formulario")]

    [SerializeField] private TMP_InputField passwordGrupoFiled;

    public GameObject CanvasLogIn { get => canvasLogIn; set => canvasLogIn = value; }
    public TMP_InputField PasswordGrupoFiled { get => passwordGrupoFiled; set => passwordGrupoFiled = value; }
}
