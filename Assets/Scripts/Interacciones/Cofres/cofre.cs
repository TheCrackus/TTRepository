using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cofre : interactuador
{

    private Animator cofreAnimator;

    [Header("Esta abierto este cofre?")]
    [SerializeField] private bool cofreAbierto = false;

    [Header("Esta vacio este cofre?")]
    [SerializeField] private bool cofreVacio = false;

    [Header("Evento para mostrar un objeto")]
    [SerializeField] private evento muestraObjeto;

    [Header("Fue Abrieto este cofre?")]
    [SerializeField] private valorBooleano estadoCofre;

    [Header("El inventario general del Player")]
    [SerializeField] private listaInventario inventariopPlayerItems;

    [Header("El item a agregar al inventario")]
    [SerializeField] private inventarioItem itemAgrgarInventario;

    [Header("Manejador de audio dialogos")]
    [SerializeField] private audioDialogos manejadorAudioDialogos;

    [Header("Manejador de audio secretos")]
    [SerializeField] private audioSecretos manejadorAudioSecretos;

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
            && PlayerEnRango)
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
        iniciaCanvas();
        manejadorAudioSecretos.reproduceAudioSecreto();
        manejadorAudioDialogos.reproduceAudioAbreDialogo();
        itemAgrgarInventario.mostrarItem = false;
        if (TextoDialogos != null
            && ContenedorTextoDialogos != null) 
        {
            ContenedorTextoDialogos.SetActive(true);
            TextoDialogos.text = itemAgrgarInventario.descripcionItem;
        }
        
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
        SimboloActivoDesactivo.invocaFunciones();
        cofreAbierto = true;
        estadoCofre.valorBooleanoEjecucion = true;
        cofreAnimator.SetBool("Abrir", true);
    }

    public void cofreYaAbierto() {
        if (!cofreVacio)
        {
            if (ContenedorTextoDialogos != null
            && TextoDialogos != null)
            {
                ContenedorTextoDialogos.SetActive(false);
                TextoDialogos.text = "";
                manejadorAudioDialogos.reproduceAudioCierraDialogo();
                Destroy(NCanvas);
            }
            muestraObjeto.invocaFunciones();
            SimboloActivoDesactivo.invocaFunciones();
            cofreVacio = true;
        }
        else 
        {
            if (ContenedorTextoDialogos != null
            && TextoDialogos != null)
            {
                if (ContenedorTextoDialogos.activeInHierarchy)
                {
                    ContenedorTextoDialogos.SetActive(false);
                    TextoDialogos.text = "";
                    manejadorAudioDialogos.reproduceAudioCierraDialogo();
                    Destroy(NCanvas);
                }
                else
                {
                    ContenedorTextoDialogos.SetActive(true);
                    TextoDialogos.text = "Un cofre vacío...";
                    manejadorAudioDialogos.reproduceAudioAbreDialogo();
                }
            }
        }
    }


    public override void OnTriggerExit2D(Collider2D colisionDetectada)
    {
        base.OnTriggerExit2D(colisionDetectada);
        if (colisionDetectada.CompareTag("Player")
            && !colisionDetectada.isTrigger)
        {
            if (ContenedorTextoDialogos != null
            && TextoDialogos != null)
            {
                ContenedorTextoDialogos.SetActive(false);
                TextoDialogos.text = "";
                Destroy(NCanvas);
            }
        }
    }
}