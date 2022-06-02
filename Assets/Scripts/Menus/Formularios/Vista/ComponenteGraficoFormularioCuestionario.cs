using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComponenteGraficoFormularioCuestionario : ComponenteGraficoFormulario, IGraficoCanvasMenuPrincipal
{

    [Header("Canvas que contiene el menu principal")]
    [SerializeField] private GameObject canvasMenuPrincipal;

    [Header("Componentes graficos que contienen la informacixon del formulario")]
    [SerializeField] private TMP_InputField fieldRespuesta1;

    [SerializeField] private TMP_InputField fieldRespuesta2;

    [SerializeField] private TMP_InputField fieldRespuesta3;

    [SerializeField] private TMP_InputField fieldRespuesta4;

    [SerializeField] private TMP_InputField fieldRespuesta5;

    [SerializeField] private TMP_InputField fieldRespuesta6;

    public GameObject CanvasMenuPrincipal { get => canvasMenuPrincipal; set => canvasMenuPrincipal = value; }
    public TMP_InputField FieldRespuesta1 { get => fieldRespuesta1; set => fieldRespuesta1 = value; }
    public TMP_InputField FieldRespuesta2 { get => fieldRespuesta2; set => fieldRespuesta2 = value; }
    public TMP_InputField FieldRespuesta3 { get => fieldRespuesta3; set => fieldRespuesta3 = value; }
    public TMP_InputField FieldRespuesta4 { get => fieldRespuesta4; set => fieldRespuesta4 = value; }
    public TMP_InputField FieldRespuesta5 { get => fieldRespuesta5; set => fieldRespuesta5 = value; }
    public TMP_InputField FieldRespuesta6 { get => fieldRespuesta6; set => fieldRespuesta6 = value; }
}
