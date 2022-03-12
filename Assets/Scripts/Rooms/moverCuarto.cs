using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverCuarto : MonoBehaviour
{

    public Vector2 cambioCamara;
    public Vector3 cambioPlayer;

    private movimientoCamara movCam;

    // Start is called before the first frame update
    void Start()
    {
        movCam = Camera.main.GetComponent<movimientoCamara>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.CompareTag("Player"))
        {
            movCam.posicionMinima += cambioCamara;
            movCam.posicionMaxima += cambioCamara;
            colisionDetectada.transform.position += cambioPlayer;
        }
    }
}