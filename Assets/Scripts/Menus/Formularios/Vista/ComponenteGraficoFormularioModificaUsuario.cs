using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComponenteGraficoFormularioModificaUsuario : ComponenteGraficoFormulario, IGraficoCanvasMenuPrincipal
{

    [Header("Canvas que contiene el menu principal")]
    [SerializeField] private GameObject canvasMenuPrincipal;

    [Header("Componentes graficos que contienen la informacion del formulario")]
    [SerializeField] private TMP_InputField emailFiled;

    [SerializeField] private TMP_InputField passwordFiled;

    [SerializeField] private TMP_InputField passwordFieldConf;

    [SerializeField] private TMP_InputField sobrenombreFiled;

    [SerializeField] private TMP_InputField diaFiled;

    [SerializeField] private TMP_InputField mesFiled;

    [SerializeField] private TMP_InputField añoFiled;

    public GameObject CanvasMenuPrincipal { get => canvasMenuPrincipal; set => canvasMenuPrincipal = value; }
    public TMP_InputField EmailFiled { get => emailFiled; set => emailFiled = value; }
    public TMP_InputField PasswordFiled { get => passwordFiled; set => passwordFiled = value; }
    public TMP_InputField PasswordFieldConf { get => passwordFieldConf; set => passwordFieldConf = value; }
    public TMP_InputField SobrenombreFiled { get => sobrenombreFiled; set => sobrenombreFiled = value; }
    public TMP_InputField DiaFiled { get => diaFiled; set => diaFiled = value; }
    public TMP_InputField MesFiled { get => mesFiled; set => mesFiled = value; }
    public TMP_InputField AñoFiled { get => añoFiled; set => añoFiled = value; }

}
