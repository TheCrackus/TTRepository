using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoTorreta : TrepaCielosCorrupto
{
    private float tiempoDisparoSegundos;

    private bool puedoDisparar;

    [Header("El proyectil que arroja el enemigo")]
    [SerializeField] private GameObject proyectilPiedra;

    [Header("El tiempo entre cada disparo")]
    [SerializeField] private float tiempoDisparo;

    [Header("Manejador de audio del proyectil")]
    [SerializeField] private AudioProyectil manejadorAudioProyectil;

    public override void Start()
    {
        base.Start();
        puedoDisparar = false;
    }

    public override void Update()
    {
        base.Update();
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>().EstadoPlayer.Estado == EstadoGenerico.transicionando
            && GameObject.FindGameObjectWithTag("Player").GetComponent<MovimientoPlayer>().EstadoPlayer.Estado != EstadoGenerico.interactuando)
        {
            puedoDisparar = false;
            tiempoDisparoSegundos = tiempoDisparo;
        }
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
        if (EnemigoRigidBody != null) 
        {
            EnemigoRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (PuedoMoverme)
        {
            gestionarDistancias();
        }
    }

    public override void gestionarDistancias()
    {
        if (ObjetivoPerseguir != null) 
        {
            if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) <= RadioPersecucion
            && Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) >= RadioAtaque)
            {
                if (EstadoEnemigo != null) 
                {
                    if (EstadoEnemigo.Estado == EstadoGenerico.caminando
                        || EstadoEnemigo.Estado == EstadoGenerico.durmiendo
                        || EstadoEnemigo.Estado == EstadoGenerico.ninguno)
                    {
                        if (puedoDisparar)
                        {
                            manejadorAudioProyectil.reproducirAudioProyectil();
                            Vector3 vectorTemporal = ObjetivoPerseguir.transform.position - gameObject.transform.position;
                            GameObject proyectilActual = Instantiate(proyectilPiedra, gameObject.transform.position, Quaternion.identity);
                            proyectilActual.GetComponent<ProyectilPiedra>().arrojar(vectorTemporal.normalized);
                            puedoDisparar = false;
                        }
                        EstadoEnemigo.Estado = EstadoGenerico.caminando;
                        if (EnemigoAnimator != null) 
                        {
                            EnemigoAnimator.SetBool("Despertar", true);
                        }
                    }
                }
            }
            else
            {
                if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) > RadioPersecucion)
                {
                    if (EnemigoAnimator != null)
                    {
                        EnemigoAnimator.SetBool("Despertar", false);
                    }
                    if (EstadoEnemigo != null)
                    {
                        EstadoEnemigo.Estado = EstadoGenerico.durmiendo;
                    }
                }
            }
        }
    }

}
