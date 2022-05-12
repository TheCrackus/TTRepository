using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoTorreta : trepaCielosCorrupto
{
    [Header("Contador para lanzar un proyectil")]
    [SerializeField] private float tiempoDisparoSegundos;
    [Header("Puedo disparar el proyectil?")]
    [SerializeField] private bool puedoDisparar;
    [Header("El proyectil que arroja el enemigo")]
    [SerializeField] private GameObject proyectilPiedra;
    [Header("El tiempo entre cada disparo")]
    [SerializeField] private float tiempoDisparo;
    [Header("Contenedor de un audio a reporducir")]
    [SerializeField] private GameObject audioEmergente;
    [Header("Audio para arrojar el proyectil")]
    [SerializeField] private AudioSource audioArrojaProyectil;
    [Header("Velocidad de reproduccion del Audio y agudez")]
    [SerializeField] private float velocidadAudioArrojaProyectil;

    public override void Start()
    {
        base.Start();
        puedoDisparar = false;
    }

    public override void Update()
    {
        base.Update();
        if (!puedoDisparar) 
        {
            tiempoDisparoSegundos -= Time.deltaTime;
            if (tiempoDisparoSegundos <= 0)
            {
                tiempoDisparoSegundos = tiempoDisparo;
                puedoDisparar = true;
            }
        }
    }

    public override void FixedUpdate()
    {
        getEnemigoRigidBody().constraints = RigidbodyConstraints2D.FreezeAll;
        if (getPuedoMoverme())
        {
            gestionDistancias();
        }
    }

    public void reproduceAudio(AudioSource audio, float velocidad)
    {
        if (audio)
        {
            audioEmergente audioEmergenteTemp = Instantiate(audioEmergente, gameObject.transform.position, Quaternion.identity).GetComponent<audioEmergente>();
            audioEmergenteTemp.GetComponent<AudioSource>().clip = audio.clip;
            audioEmergenteTemp.GetComponent<AudioSource>().pitch = velocidad;
            audioEmergenteTemp.reproduceAudioClick();
        }
    }

    public override void gestionDistancias()
    {
        if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) <= radioPersecucion
            && Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) >= radioAtaque)
        {
            if (getEstadoEnemigo() == estadoGenerico.caminando
                || getEstadoEnemigo() == estadoGenerico.durmiendo
                || getEstadoEnemigo() == estadoGenerico.ninguno)
            {
                if (puedoDisparar) 
                {
                    reproduceAudio(audioArrojaProyectil, velocidadAudioArrojaProyectil);
                    Vector3 vectorTemporal = getObjetivoPerseguir().transform.position - gameObject.transform.position;
                    GameObject proyectilActual = Instantiate(proyectilPiedra, gameObject.transform.position, Quaternion.identity);
                    proyectilActual.GetComponent<proyectilPiedra>().arroja(vectorTemporal.normalized);
                    puedoDisparar = false;
                }
                setEstadoEnemigo(estadoGenerico.caminando);
                getEnemigoAnimator().SetBool("Despertar", true);
            }
        }
        else
        {
            if (Vector3.Distance(getObjetivoPerseguir().position, gameObject.transform.position) > radioPersecucion)
            {
                getEnemigoAnimator().SetBool("Despertar", false);
                setEstadoEnemigo(estadoGenerico.durmiendo);
            }
        }
    }

}
