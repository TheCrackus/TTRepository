using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoFormularioEnlaceProfesor : ComponenteGraficoFormulario, IGraficoCanvasLogIn
{

    [Header("Canvas que contiene el formulario de Log In")]
    [SerializeField] private GameObject canvasLogIn;

    [Header("Componentes graficos que contienen la informacion del formulario")]

    [SerializeField] private InputField passwordGrupoFiled;

    public GameObject CanvasLogIn { get => canvasLogIn; set => canvasLogIn = value; }
    public InputField PasswordGrupoFiled { get => passwordGrupoFiled; set => passwordGrupoFiled = value; }
}
