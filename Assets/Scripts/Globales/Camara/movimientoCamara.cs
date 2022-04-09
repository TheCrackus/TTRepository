using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimientoCamara : MonoBehaviour
{

    public Transform objetivo;
    public float suavizado;
    public Vector2 posicionMaxima;
    public Vector2 posicionMinima;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void FixedUpdate() 
    {
        if (transform.position != objetivo.position) 
        {
            Vector3 posicionObjetivo = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);
            posicionObjetivo.x = Mathf.Clamp(posicionObjetivo.x, posicionMinima.x, posicionMaxima.x);
            posicionObjetivo.y = Mathf.Clamp(posicionObjetivo.y, posicionMinima.y, posicionMaxima.y);
            transform.position = Vector3.Lerp(transform.position, posicionObjetivo, suavizado);
        }
    }
}
