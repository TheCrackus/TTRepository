using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoModificarUsuario : ComponenteGraficoFormulario, IGraficoCanvasMenuPrincipal
{

    [Header("Canvas que contiene el menu principal")]
    [SerializeField] private GameObject canvasMenuPrincipal;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private InputField emailFiled;

    [SerializeField] private InputField passwordFiled;

    [SerializeField] private InputField passwordFieldConf;

    [SerializeField] private InputField sobrenombreFiled;

    [SerializeField] private InputField diaFiled;

    [SerializeField] private InputField mesFiled;

    [SerializeField] private InputField añoFiled;

    public GameObject CanvasMenuPrincipal { get => canvasMenuPrincipal; set => canvasMenuPrincipal = value; }
    public InputField EmailFiled { get => emailFiled; set => emailFiled = value; }
    public InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
    public InputField PasswordFieldConf { get => passwordFieldConf; set => passwordFieldConf = value; }
    public InputField SobrenombreFiled { get => sobrenombreFiled; set => sobrenombreFiled = value; }
    public InputField DiaFiled { get => diaFiled; set => diaFiled = value; }
    public InputField MesFiled { get => mesFiled; set => mesFiled = value; }
    public InputField AñoFiled { get => añoFiled; set => añoFiled = value; }

}
