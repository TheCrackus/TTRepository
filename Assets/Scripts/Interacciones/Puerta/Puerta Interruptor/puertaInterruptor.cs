using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaInterruptor : puerta
{

    [Header("Texto a mostrar")]
    [SerializeField] private string dialogo;

    void Update()
    {
        if (Input.GetButtonDown("Interactuar")
            && PlayerEnRango)
        {
            iniciarCanvas();
            if (ContenedorTextoDialogos != null
                && TextoDialogos != null)
            {
                if (ContenedorTextoDialogos.activeInHierarchy)
                {
                    ManejadorAudioDialogos.reproducirAudioCierraDialogo();
                    ContenedorTextoDialogos.SetActive(false);
                    Destroy(NCanvas);
                }
                else
                {
                    ManejadorAudioDialogos.reproducirAudioAbreDialogo();
                    ContenedorTextoDialogos.SetActive(true);
                    TextoDialogos.text = dialogo;
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
                    ManejadorAudioDialogos.reproducirAudioCierraDialogo();
                    ContenedorTextoDialogos.SetActive(false);
                    TextoDialogos.text = "";
                    Destroy(NCanvas);
                }
            }
        }
    }

}
