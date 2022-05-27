using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectilPiedra : proyectil
{

    [Header("Manejador de audio al impactar")]
    [SerializeField] private audioProyectilRomper manejadorAudioProyectilRomper;

    private void OnTriggerEnter2D(Collider2D colisionDetectada)
    {
        if ((colisionDetectada.gameObject.CompareTag("Player") || colisionDetectada.gameObject.CompareTag("ArmaObjetoPlayer"))
            && colisionDetectada.isTrigger)
        {
            manejadorAudioProyectilRomper.reproduceAudioProyectilRomper();
            Destroy(gameObject);
        }
    }

}
