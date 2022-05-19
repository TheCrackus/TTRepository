using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaEvento : puerta
{

    private bool cumploEvento;

    [Header("Texto a mostrar")]
    [SerializeField] private string dialogoPositivo;

    [Header("Texto a mostrar")]
    [SerializeField] private string dialogoNegativo;

    [Header("Items que representan las piezas del puzzle")]
    [SerializeField] private inventarioItem[] piezas;

    [Header("Objeto para la transicion al evento")]
    [SerializeField] private moverEscena manejadorMoverEscena;

    void Update()
    {
        if (Input.GetButtonDown("Interactuar"))
        {
            if (PlayerEnRango)
            {
                if (InventarioPlayerItems && piezas != null && piezas.Length > 0)
                {
                    foreach (inventarioItem itemLoop in piezas)
                    {
                        if (InventarioPlayerItems.verififcaItem(itemLoop))
                        {
                            cumploEvento = true;
                            continue;
                        }
                        else
                        {
                            cumploEvento = false;
                        }
                    }
                    iniciaCanvas();
                    if (cumploEvento)
                    {
                        abreEscenaEvento(dialogoPositivo, cumploEvento);
                    }
                    else
                    {
                        abreEscenaEvento(dialogoNegativo, cumploEvento);
                    }
                }
            }
        }
    }

    public void abreEscenaEvento(string dialogo, bool cumploEvento)
    {
        if (cumploEvento)
        {
            if (ContenedorTextoDialogos.activeInHierarchy)
            {
                ManejadorAudioDialogos.reproduceAudioCierraDialogo();
                ContenedorTextoDialogos.SetActive(false);
                manejadorMoverEscena.iniciaTransicionOut();
            }
            else
            {
                ManejadorAudioDialogos.reproduceAudioAbreDialogo();
                ContenedorTextoDialogos.SetActive(true);
                TextoDialogos.text = dialogo;
            }
        }
        else 
        {
            if (ContenedorTextoDialogos.activeInHierarchy)
            {
                ManejadorAudioDialogos.reproduceAudioCierraDialogo();
                ContenedorTextoDialogos.SetActive(false);
            }
            else
            {
                ManejadorAudioDialogos.reproduceAudioAbreDialogo();
                ContenedorTextoDialogos.SetActive(true);
                TextoDialogos.text = dialogo;
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
                if (ContenedorTextoDialogos.activeInHierarchy)
                {
                    ManejadorAudioDialogos.reproduceAudioCierraDialogo();
                    ContenedorTextoDialogos.SetActive(false);
                    TextoDialogos.text = "";
                    Destroy(NCanvas);
                }
            }
        }
    }

}
