using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ArrastraSueltaPiezas : MonoBehaviour
{

    private GameObject piezaSeleccionada;

    private int piezaSeleccionadaOrderLayer;

    [Header("Manejador de la transicion")]
    [SerializeField] private MoverEscena manejadorMoverEscena;

    [Header("Las piezas del puzzle")]
    [SerializeField] private ManejadorPiezas[] piezas;

    [Header("Eventos que se ejecutaran en otra escena")]
    [SerializeField] private List<Evento> eventosPuzzle = new List<Evento>();

    [Header("Manejador de audio del puzle")]
    [SerializeField] private audioPuzzle manejadorAudioPuzzle;

    private void Start()
    {
        piezaSeleccionadaOrderLayer = 1;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Atacar")) 
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) 
            {
                if (hit.transform.CompareTag("Puzzle"))
                {
                    if (!hit.transform.GetComponent<ManejadorPiezas>().EstoyPosicionCorrecta) 
                    {
                        manejadorAudioPuzzle.reproduceTomaPieza();
                        piezaSeleccionada = hit.transform.gameObject;
                        piezaSeleccionada.GetComponent<ManejadorPiezas>().EstoyPosicionCorrecta = true;
                        piezaSeleccionada.GetComponent<SortingGroup>().sortingOrder = piezaSeleccionadaOrderLayer;
                        piezaSeleccionadaOrderLayer++;
                    }
                }
            }
        }
        if (Input.GetButtonUp("Atacar")) 
        {
            if (piezaSeleccionada) 
            {
                manejadorAudioPuzzle.reproduceSueltaPieza();
                piezaSeleccionada.GetComponent<ManejadorPiezas>().EstoyPosicionCorrecta = false;
            }
            piezaSeleccionada = null;
        }
        if (piezaSeleccionada)
        {
            Vector3 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            piezaSeleccionada.transform.position = new Vector3(posicionMouse.x, posicionMouse.y, 0);
        }
        
    }

    public void verificarPiezas() 
    {
        if (piezas != null && piezas.Length > 0)
        {
            foreach (ManejadorPiezas pieza in piezas)
            {
                if (pieza.EstoyPosicionCorrecta)
                {
                    continue;
                }
                else 
                {
                    return;
                }
            }
            if (GameObject.FindGameObjectWithTag("SetUp")) 
            { 
                foreach (Evento eventoLoop in eventosPuzzle) 
                {
                    SingletonEventosEscenas.instance.agregarEvento(eventoLoop);
                }
                manejadorMoverEscena.iniciarTransicionOut();
            }
        }
    }
}
