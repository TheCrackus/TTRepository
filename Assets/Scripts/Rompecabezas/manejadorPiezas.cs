using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ManejadorPiezas : MonoBehaviour
{

    private bool estoyPosicionCorrecta;

    private bool estoySeleccionada;

    private Vector3 posicionContenedorPiezas;

    [Header("Evento que verifica la posicion de las piezas")]
    [SerializeField] private Evento verificaPiezas;

    [Header("Manejador de audio de las piezas")]
    [SerializeField] private AudioPieza manejadorAudioPieza;

    public bool EstoyPosicionCorrecta { get => estoyPosicionCorrecta; set => estoyPosicionCorrecta = value; }
    public bool EstoySeleccionada { get => estoySeleccionada; set => estoySeleccionada = value; }

    private void Start()
    {
        EstoyPosicionCorrecta = false;
        posicionContenedorPiezas = gameObject.transform.position;
        gameObject.transform.position = new Vector3(Random.Range(2f, 10f), Random.Range(-5f, 5f));
    }

    private void Update()
    {
        if (!EstoySeleccionada) 
        {
            if (Vector3.Distance(gameObject.transform.position, posicionContenedorPiezas) < 0.5f)
            {
                if (!EstoyPosicionCorrecta) 
                {
                    manejadorAudioPieza.reproduceAciertaPieza();
                    gameObject.transform.position = posicionContenedorPiezas;
                    EstoyPosicionCorrecta = true;
                    gameObject.GetComponent<SortingGroup>().sortingOrder = 0;
                    verificaPiezas.invocarFunciones();
                }
            }
        }
    }
}
