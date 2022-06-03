using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaEvento : puerta
{

    [Header("Texto a mostrar")]
    [SerializeField] private string dialogoPositivo;

    [Header("Texto a mostrar")]
    [SerializeField] private string dialogoNegativo;

    [Header("Items que representan las piezas del puzzle")]
    [SerializeField] private InventarioItem[] piezas;

    [Header("Objeto para la transicion al evento")]
    [SerializeField] private MoverEscena manejadorMoverEscena;

    void Update()
    {
        if (Input.GetButtonDown("Interactuar"))
        {
            if (PlayerEnRango)
            {
                if (InventarioPlayerItems && piezas != null && piezas.Length > 0)
                {
                    iniciarCanvas();
                    foreach (InventarioItem itemLoop in piezas)
                    {
                        if (InventarioPlayerItems.verififcarItem(itemLoop))
                        {
                            continue;
                        }
                        else
                        {
                            abreEscenaEvento(dialogoNegativo, false);
                            return;
                        }
                    }
                    abreEscenaEvento(dialogoPositivo, true);
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
                ManejadorAudioDialogos.reproducirAudioCierraDialogo();
                ContenedorTextoDialogos.SetActive(false);
                manejadorMoverEscena.iniciarTransicionOut();
            }
            else
            {
                ManejadorAudioDialogos.reproducirAudioAbreDialogo();
                ContenedorTextoDialogos.SetActive(true);
                TextoDialogos.text = dialogo;
            }
        }
        else 
        {
            if (ContenedorTextoDialogos.activeInHierarchy)
            {
                ManejadorAudioDialogos.reproducirAudioCierraDialogo();
                ContenedorTextoDialogos.SetActive(false);
            }
            else
            {
                ManejadorAudioDialogos.reproducirAudioAbreDialogo();
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
                    ManejadorAudioDialogos.reproducirAudioCierraDialogo();
                    ContenedorTextoDialogos.SetActive(false);
                    TextoDialogos.text = "";
                    Destroy(NCanvas);
                }
            }
        }
    }

}
