using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cofre : interactuador
{

    [Header("Animador de este objeto")]
    [SerializeField] private Animator cofreAnimator;
    [Header("Esta abierto este cofre?")]
    [SerializeField] private bool cofreAbierto = false;
    [Header("Esta vacio este cofre?")]
    [SerializeField] private bool cofreVacio = false;
    [Header("Evento para mostrar un objeto")]
    [SerializeField] private evento muestraObjeto;
    [Header("Objeto que contiene el texto a mostrar")]
    [SerializeField] private GameObject objetoContenedorTextoDialogos;
    [Header("Objeto texto a mostrar")]
    [SerializeField] private TextMeshProUGUI textoDialogos;
    [Header("Fue Abrieto este cofre?")]
    [SerializeField] private valorBooleano estadoCofre;
    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;
    [Header("El item a agregar al inventario")]
    [SerializeField] private inventarioItem itemAgrgarInventario;


    void Start()
    {
        cofreAnimator = gameObject.GetComponent<Animator>();
        cofreAbierto = estadoCofre.valorBooleanoEjecucion;
        cofreVacio = estadoCofre.valorBooleanoEjecucion;
        if (cofreAbierto && cofreVacio) 
        {
            cofreAnimator.SetBool("Abrir", true);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interactuar") 
            && playerEnRango 
            && estadoCofre != null
            && itemAgrgarInventario != null
            && objetoContenedorTextoDialogos != null 
            && textoDialogos != null)
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
        itemAgrgarInventario.mostrarItem = false;
        textoDialogos.text = itemAgrgarInventario.descripcionItem;
        objetoContenedorTextoDialogos.SetActive(true);
        if (inventariopPlayerItems && itemAgrgarInventario)
        {
            if (inventariopPlayerItems.inventario.Contains(itemAgrgarInventario) && !itemAgrgarInventario.esUnico)
            {
                itemAgrgarInventario.cantidadItem += 1;
            }
            else
            {
                inventariopPlayerItems.inventario.Add(itemAgrgarInventario);
                itemAgrgarInventario.cantidadItem += 1;
            }
        }
        itemAgrgarInventario.mostrarItem = true;
        muestraObjeto.invocaFunciones();
        simboloActivoDesactivo.invocaFunciones();
        cofreAbierto = true;
        estadoCofre.valorBooleanoEjecucion = true;
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