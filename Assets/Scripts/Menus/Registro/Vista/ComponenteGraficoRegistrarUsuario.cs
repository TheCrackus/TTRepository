using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ComponenteGraficoRegistrarUsuario : ComponenteGraficoFormulario, IGraficoCanvasLogIn
{

    [Header("Canvas que contiene el formulario de Log In")]
    [SerializeField] private GameObject canvasLogIn;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField emailFiled;

    [SerializeField] private InputField passwordFiled;

    [SerializeField] private InputField sobrenombreFiled;

    [SerializeField] private InputField diaFiled;

    [SerializeField] private InputField mesFiled;

    [SerializeField] private InputField añoFiled;

    public GameObject CanvasLogIn { get => canvasLogIn; set => canvasLogIn = value; }
    public InputField EmailFiled { get => emailFiled; set => emailFiled = value; }
    public InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
    public InputField SobrenombreFiled { get => sobrenombreFiled; set => sobrenombreFiled = value; }
    public InputField DiaFiled { get => diaFiled; set => diaFiled = value; }
    public InputField MesFiled { get => mesFiled; set => mesFiled = value; }
    public InputField AñoFiled { get => añoFiled; set => añoFiled = value; }

}
