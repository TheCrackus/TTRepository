using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flecha : Proyectil
{
    [Header("Cantidad de magia que decrementa al jugador")]
    public float costoMagia;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if (colisionDetectada.gameObject.CompareTag("Enemigo")
            && colisionDetectada.isTrigger)
        {
            Destroy(gameObject);
        }
    }

}
