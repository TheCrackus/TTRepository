using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorruptoTorreta : trepaCielosCorrupto
{
    private float tiempoDisparoSegundos;

    private bool puedoDisparar;

    [Header("El proyectil que arroja el enemigo")]
    [SerializeField] private GameObject proyectilPiedra;

    [Header("El tiempo entre cada disparo")]
    [SerializeField] private float tiempoDisparo;

    [Header("Manejador de audio del proyectil")]
    [SerializeField] private audioProyectil manejadorAudioProyectil;

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
        if (EnemigoRigidBody != null) 
        {
            EnemigoRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (PuedoMoverme)
        {
            gestionDistancias();
        }
    }

    public override void gestionDistancias()
    {
        if (ObjetivoPerseguir != null) 
        {
            if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) <= RadioPersecucion
            && Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) >= RadioAtaque)
            {
                if (EstadoEnemigo != null) 
                {
                    if (EstadoEnemigo.Estado == estadoGenerico.caminando
                        || EstadoEnemigo.Estado == estadoGenerico.durmiendo
                        || EstadoEnemigo.Estado == estadoGenerico.ninguno)
                    {
                        if (puedoDisparar)
                        {
                            manejadorAudioProyectil.reproduceAudioProyectil();
                            Vector3 vectorTemporal = ObjetivoPerseguir.transform.position - gameObject.transform.position;
                            GameObject proyectilActual = Instantiate(proyectilPiedra, gameObject.transform.position, Quaternion.identity);
                            proyectilActual.GetComponent<proyectilPiedra>().arroja(vectorTemporal.normalized);
                            puedoDisparar = false;
                        }
                        EstadoEnemigo.Estado = estadoGenerico.caminando;
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
                        EstadoEnemigo.Estado = estadoGenerico.durmiendo;
                    }
                }
            }
        }
    }

}
