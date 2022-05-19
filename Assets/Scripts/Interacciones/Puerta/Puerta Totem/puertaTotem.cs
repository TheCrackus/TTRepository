using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puertaTotem : puerta
{

    [Header("Texto a mostrar")]
    [SerializeField] private string dialogo;

    void Update()
    {
        if (Input.GetButtonDown("Interactuar")
            && PlayerEnRango)
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

    public override void abrir()
    {
        if (PuertaColliders != null && PuertaSpriteRenderer != null)
        {
            if (EstaAbierta != null)
            {
                if (EstaAbierta.valorBooleanoEjecucion)
                {
                    ManejadorAudioPuerta.reproduceAudioRomperPuerta();
                }
            }
            PuertaSpriteRenderer.enabled = false;
            if (EstaAbierta != null)
            {
                EstaAbierta.valorBooleanoEjecucion = true;
            }
            foreach (BoxCollider2D colision in PuertaColliders)
            {
                colision.enabled = false;
            }
        }
    }

    public override void cerrar()
    {
        if (PuertaColliders != null && PuertaSpriteRenderer != null)
        {
            if (EstaAbierta != null) 
            {
                if (EstaAbierta.valorBooleanoEjecucion) 
                {
                    ManejadorAudioPuerta.reproduceAudioRomperPuerta();
                }
            }
            PuertaSpriteRenderer.enabled = true;
            if (EstaAbierta != null)
            {
                EstaAbierta.valorBooleanoEjecucion = false;
            }
            foreach (BoxCollider2D colision in PuertaColliders)
            {
                colision.enabled = true;
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
