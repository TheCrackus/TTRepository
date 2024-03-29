using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cofre : Interactuador
{

    private Animator cofreAnimator;

    [Header("Esta abierto este cofre?")]
    [SerializeField] private bool cofreAbierto = false;

    [Header("Esta vacio este cofre?")]
    [SerializeField] private bool cofreVacio = false;

    [Header("Evento para mostrar un objeto")]
    [SerializeField] private Evento muestraObjeto;

    [Header("Fue Abrieto este cofre?")]
    [SerializeField] private ValorBooleano estadoCofre;

    [Header("El inventario general del Player")]
    [SerializeField] private ListaInventario inventariopPlayerItems;

    [Header("El item a agregar al inventario")]
    [SerializeField] private InventarioItem itemAgrgarInventario;

    [Header("Manejador de audio dialogos")]
    [SerializeField] private AudioDialogos manejadorAudioDialogos;

    [Header("Manejador de audio secretos")]
    [SerializeField] private AudioSecretos manejadorAudioSecretos;

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
            && GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>().EstadoPlayer.Estado != EstadoGenerico.estuneado
            && GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>().EstadoPlayer.Estado != EstadoGenerico.atacando
            && GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>().EstadoPlayer.Estado != EstadoGenerico.transicionando
            && GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>().EstadoPlayer.Estado != EstadoGenerico.inactivo
            && PlayerEnRango)
        {
            if (!cofreAbierto && !cofreVacio)
            {
                abrirCofre();
            }
            else
            {
                verificarCofreAbierto();
            }
        }
    }

    public void abrirCofre() 
    {
        iniciarCanvas();
        manejadorAudioSecretos.reproducirAudioSecreto();
        manejadorAudioDialogos.reproducirAudioAbreDialogo();
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
        muestraObjeto.invocarFunciones();
        SimboloActivoDesactivo.invocarFunciones();
        cofreAbierto = true;
        estadoCofre.valorBooleanoEjecucion = true;
        cofreAnimator.SetBool("Abrir", true);
    }

    public void verificarCofreAbierto() {
        if (!cofreVacio)
        {
            if (ContenedorTextoDialogos != null
            && TextoDialogos != null)
            {
                ContenedorTextoDialogos.SetActive(false);
                TextoDialogos.text = "";
                manejadorAudioDialogos.reproducirAudioCierraDialogo();
                Destroy(NCanvas);
            }
            muestraObjeto.invocarFunciones();
            SimboloActivoDesactivo.invocarFunciones();
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
                    manejadorAudioDialogos.reproducirAudioCierraDialogo();
                    Destroy(NCanvas);
                }
                else
                {
                    ContenedorTextoDialogos.SetActive(true);
                    TextoDialogos.text = "Un cofre vac�o...";
                    manejadorAudioDialogos.reproducirAudioAbreDialogo();
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