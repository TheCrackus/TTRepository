using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaLlave : puerta
{
    [Header("Item que representa la llave corta")]
    [SerializeField] private inventarioItem llaveCorta;

    [Header("Texto a mostrar")]
    [SerializeField] private string dialogo;

    void Update()
    {
        if (Input.GetButtonDown("Interactuar"))
        {
            if (PlayerEnRango)
            {
                if (InventarioPlayerItems && llaveCorta)
                {
                    if (InventarioPlayerItems.verififcaItem(llaveCorta))
                    {
                        llaveCorta.invocaEventoUsaItem();
                        abrir();
                    }
                    else 
                    {
                        iniciaCanvas();
                        if (ContenedorTextoDialogos != null
                            && TextoDialogos != null)
                        {
                            if (ContenedorTextoDialogos.activeInHierarchy)
                            {
                                ManejadorAudioDialogos.reproduceAudioCierraDialogo();
                                ContenedorTextoDialogos.SetActive(false);
                                Destroy(NCanvas);
                            }
                            else
                            {
                                ManejadorAudioDialogos.reproduceAudioAbreDialogo();
                                ContenedorTextoDialogos.SetActive(true);
                                TextoDialogos.text = dialogo;
                            }
                        }
                    }
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
