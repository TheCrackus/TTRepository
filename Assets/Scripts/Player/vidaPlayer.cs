using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vidaPlayer : sistemaVida
{
    [Header("Evento que actualiza la vida en pantalla")]
    [SerializeField] private evento eventoVidaPlayer;

    [Header("Color cuando golpean este objeto")]
    [SerializeField] private Color colorFlash;

    [Header("Color normal de este objeto")]
    [SerializeField] private Color colorNormal;

    [Header("Tiempo que dura el efecto de golpe")]
    [SerializeField] private float tiempoFlash;

    [Header("Numero de veces que cambia de color este objeto")]
    [SerializeField] private int numeroFlash;

    [Header("La colision que  activa el efecto de golpe")]
    [SerializeField] private Collider2D colisionTrigger;

    [Header("El manejador de Sprites de este objeto")]
    [SerializeField] private SpriteRenderer spritePlayer;

    [Header("Manejador audio al recibir golpes")]
    [SerializeField] private audioReciveGolpe manejadorAudioRecibeGolpe;

    [Header("Manejador para las escenas")]
    [SerializeField] private moverEscenaPuntoControl manejadorEscenaPuntoControl;

    public override void quitaVida(float vidaMenos)
    {
        manejadorAudioRecibeGolpe.reproduceAudioRecibeGolpe();
        base.quitaVida(vidaMenos);
        getVidaMaxima().valorFlotanteEjecucion = getVidaActual();
        eventoVidaPlayer.invocaFunciones();
        if (getVidaActual() <= 0)
        {
            manejadorEscenaPuntoControl.iniciaTransicionOut();
        }
    }

    public IEnumerator flash()
    {
        int numeroFlashTemporal = 0;
        colisionTrigger.enabled = false;
        while (numeroFlashTemporal < numeroFlash)
        {
            spritePlayer.color = colorFlash;
            yield return new WaitForSeconds(tiempoFlash);
            spritePlayer.color = colorNormal;
            yield return new WaitForSeconds(tiempoFlash);
            numeroFlashTemporal++;
        }
        colisionTrigger.enabled = true;
    }

    public void actualizaVida()
    {
        setVidaActual(getVidaMaxima().valorFlotanteEjecucion);
    }

}
