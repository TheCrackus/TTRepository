using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectilPiedra : proyectil
{

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Player")
            && colisionDetectada.isTrigger)
        {
            Destroy(gameObject);
        }
    }

}
