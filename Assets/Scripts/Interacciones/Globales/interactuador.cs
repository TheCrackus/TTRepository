using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactuador : MonoBehaviour
{

    private GameObject nCanvas;

    private GameObject contenedorTextoDialogos;

    private TextMeshProUGUI textoDialogos;

    [Header("Evento que activa un simbolo")]
    [SerializeField] private evento simboloActivoDesactivo;

    [Header("Interactua?")]
    [SerializeField] private bool playerEnRango;

    [Header("Panel para mostrar un dialogo")]
    [SerializeField] private GameObject fadeInFadeOutCanvas;

    public GameObject ContenedorTextoDialogos { get => contenedorTextoDialogos; set => contenedorTextoDialogos = value; }
    public TextMeshProUGUI TextoDialogos { get => textoDialogos; set => textoDialogos = value; }
    public evento SimboloActivoDesactivo { get => simboloActivoDesactivo; set => simboloActivoDesactivo = value; }
    public bool PlayerEnRango { get => playerEnRango; set => playerEnRango = value; }
    public GameObject NCanvas { get => nCanvas; set => nCanvas = value; }

    public void iniciarCanvas()
    {
        if (fadeInFadeOutCanvas != null)
        {
            if (!GameObject.FindGameObjectWithTag("CanvasEscenas"))
            {
                nCanvas = Instantiate(fadeInFadeOutCanvas, Vector3.zero, Quaternion.identity);
            }
            else 
            {
                nCanvas = GameObject.FindGameObjectWithTag("CanvasEscenas");
            }
            contenedorTextoDialogos = nCanvas.transform.Find("ContenedorTextoDialogos").gameObject;
            textoDialogos = contenedorTextoDialogos.transform.Find("TextoDialogos").gameObject.GetComponent<TextMeshProUGUI>();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaFunciones();
            playerEnRango = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaFunciones();
            playerEnRango = false;
        }
    }
}
