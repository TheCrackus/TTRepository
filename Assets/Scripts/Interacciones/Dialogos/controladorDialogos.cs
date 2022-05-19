using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class controladorDialogos : interactuador
{

    [Header("Texto a mostrar")]
    [SerializeField] private string dialogo;

    [Header("Manejador de audio para dialogos")]
    [SerializeField] private audioDialogos manejadorAudioDialogos;

    public virtual void Update()
    {
        if (Input.GetButtonDown("Interactuar") && PlayerEnRango) 
        {
            iniciaCanvas();
            if (ContenedorTextoDialogos != null
                && TextoDialogos != null) 
            {
                if (ContenedorTextoDialogos.activeInHierarchy)
                {
                    ContenedorTextoDialogos.SetActive(false);
                    manejadorAudioDialogos.reproduceAudioCierraDialogo();
                    Destroy(NCanvas);
                }
                else
                {
                    ContenedorTextoDialogos.SetActive(true);
                    TextoDialogos.text = dialogo;
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
