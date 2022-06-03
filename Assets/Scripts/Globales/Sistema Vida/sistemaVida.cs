using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaVida : MonoBehaviour
{
    [Header("La vida que posee este objeto")]
    [SerializeField] private ValorFlotante vidaObjeto;

    [Header("La vida actual del objeto")]
    [SerializeField] private float vidaActualObjeto;

    [Header("Objeto contenedor de efecto")]
    [SerializeField] private GameObject efectoMuerte;

    [Header("Clip de muerte del enemigo")]
    [SerializeField] private AnimationClip muerteClip;

    [Header("Objetos que dejara al morir")]
    [SerializeField] private TablaLoot miLoot;

    [Header("Objeto que posee esta vida")]
    [SerializeField] private GameObject objeto;

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
    [SerializeField] private SpriteRenderer spriteObjeto;

    [Header("Manejador de audio al recibir daño")]
    [SerializeField] private AudioReciveGolpe manejadorAudioRecibeGolpe;

    [Header("Manejador de audio al morir")]
    [SerializeField] private AudioEfectoMuerte manejadorAudioEfectoMuerte;

    public ValorFlotante VidaObjeto { get => vidaObjeto; set => vidaObjeto = value; }
    public float VidaActualObjeto { get => vidaActualObjeto; set => vidaActualObjeto = value; }
    public GameObject Objeto { get => objeto; set => objeto = value; }
    public AudioReciveGolpe ManejadorAudioRecibeGolpe { get => manejadorAudioRecibeGolpe; set => manejadorAudioRecibeGolpe = value; }
    public AudioEfectoMuerte ManejadorAudioEfectoMuerte { get => manejadorAudioEfectoMuerte; set => manejadorAudioEfectoMuerte = value; }

    public virtual void OnEnable()
    {
        if (vidaObjeto != null
            && spriteObjeto != null
            && colisionTrigger != null) 
        {
            vidaActualObjeto = vidaObjeto.valorFlotanteEjecucion;
            spriteObjeto.color = colorNormal;
            colisionTrigger.enabled = true;
        }
    }

    public virtual void agregarVida(float vidaExtra)
    {
        if (vidaObjeto != null)
        {
            vidaActualObjeto += vidaExtra;
            if (vidaActualObjeto > vidaObjeto.valorFlotanteEjecucion)
            {
                vidaActualObjeto = vidaObjeto.valorFlotanteEjecucion;
            }
        }
    }

    public virtual void llenarVida()
    {
        if (vidaObjeto != null)
        {
            vidaActualObjeto = vidaObjeto.valorFlotanteEjecucion;
        }
    }

    public virtual void quitarVida(float vidaMenos) 
    {
        if (manejadorAudioRecibeGolpe != null) 
        {
            manejadorAudioRecibeGolpe.reproduceAudioRecibeGolpe();
        }
        StartCoroutine(realizarFlash());
        vidaActualObjeto -= vidaMenos;
        if (vidaActualObjeto <= 0)
        {
            if (manejadorAudioEfectoMuerte != null) 
            {
                manejadorAudioEfectoMuerte.reproduceAudioMuerte();
            }
            vidaActualObjeto = 0;
            if (objeto != null) 
            {
                objeto.SetActive(false);
            }
            animarMuerte();
            procesarLoot();
        }
    }

    public virtual void quitarVidaCompleta() 
    {
        vidaActualObjeto = 0;
    }

    public void animarMuerte()
    {
        if (efectoMuerte != null)
        {
            GameObject efecto = Instantiate(efectoMuerte, gameObject.transform.position, Quaternion.identity);
            Destroy(efecto, muerteClip.length);
        }
    }

    public void procesarLoot()
    {
        if (miLoot != null)
        {
            IncrementoEstadisticas incrementoActual = miLoot.generarLootIncrementoEstadisticas();
            if (incrementoActual != null)
            {
                Instantiate(incrementoActual.gameObject, gameObject.transform.position, Quaternion.identity);
            }
        }
    }

    public IEnumerator realizarFlash()
    {
        int numeroFlashTemporal = 0;
        colisionTrigger.enabled = false;
        while (numeroFlashTemporal < numeroFlash)
        {
            spriteObjeto.color = colorFlash;
            yield return new WaitForSeconds(tiempoFlash);
            spriteObjeto.color = colorNormal;
            yield return new WaitForSeconds(tiempoFlash);
            numeroFlashTemporal++;
        }
        colisionTrigger.enabled = true;
    }
}
