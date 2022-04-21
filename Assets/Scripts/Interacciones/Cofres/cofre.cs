using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cofre : interactuador
{

    private Animator cofreAnimator;
    private bool cofreAbierto;
    private bool cofreVacio;
    [Header("Objeto que contiene el cofre")]
    public objeto objetoContenido;
    [Header("Evento para mostrar un objeto")]
    public evento muestraObjeto;
    [Header("Objeto que contiene el texto a mostrar")]
    public GameObject objetoContenedorTextoDialogos;
    [Header("Objeto texto a mostrar")]
    public Text textoDialogos;
    [Header("Inventario del player")]
    public inventario inventarioPlayer;
    

    void Start()
    {
        cofreAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerEnRango)
        {
            if (!cofreAbierto && !cofreVacio)
            {
                abreCofre();
            }
            else
            {
                cofreYaAbierto();
            }
        }
    }

    public void abreCofre() 
    {
        textoDialogos.text = objetoContenido.descripcionObjeto;
        objetoContenedorTextoDialogos.SetActive(true);
        inventarioPlayer.agregaObjeto(objetoContenido);
        inventarioPlayer.objetoActual = objetoContenido;
        muestraObjeto.invocaFunciones();
        simboloActivoDesactivo.invocaFunciones();
        cofreAbierto = true;
        cofreAnimator.SetBool("Abrir", true);
    }

    public void cofreYaAbierto() {
        if (!cofreVacio)
        {
            textoDialogos.text = "";
            objetoContenedorTextoDialogos.SetActive(false);
            muestraObjeto.invocaFunciones();
            simboloActivoDesactivo.invocaFunciones();
            cofreVacio = true;
        }
        else 
        {
            if (objetoContenedorTextoDialogos.activeInHierarchy)
            {
                objetoContenedorTextoDialogos.SetActive(false);
            }
            else 
            {
                textoDialogos.text = "Un cofre vacío...";
                objetoContenedorTextoDialogos.SetActive(true);
            }
        }
    }


    public override void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            simboloActivoDesactivo.invocaFunciones();
            playerEnRango = false;
            textoDialogos.text = "";
            objetoContenedorTextoDialogos.SetActive(false);
        }
    }
}