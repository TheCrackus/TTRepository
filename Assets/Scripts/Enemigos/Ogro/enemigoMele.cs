using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoMele : trepaCielosCorrupto
{
    private AnimationClip atacandoClip;

    [Header("Manejador de audio del Player arma mele")]
    [SerializeField] private audioMelee manejadorAudioMelee;
    public override void OnEnable()
    {
        if (PosicionMapa != null
            && ObjetivoPerseguir != null
            && EnemigoRigidBody != null
            && EnemigoAnimator != null
            && EstadoEnemigo != null)
        {
            PosicionMapa.transform.position = PosicionOriginal;
            ObjetivoPerseguir = GameObject.FindWithTag("Player").transform;
            EnemigoRigidBody = gameObject.GetComponent<Rigidbody2D>();
            EnemigoAnimator = gameObject.GetComponent<Animator>();
            EstadoEnemigo.Estado = estadoGenerico.ninguno;
        }
    }

    public override void Start()
    {
        if (ObjetivoPerseguir != null
            && EnemigoRigidBody != null
            && EnemigoAnimator != null
            && EstadoEnemigo != null) 
        {
            ObjetivoPerseguir = GameObject.FindWithTag("Player").transform;
            EnemigoRigidBody = gameObject.GetComponent<Rigidbody2D>();
            EnemigoAnimator = gameObject.GetComponent<Animator>();
            EstadoEnemigo.Estado = estadoGenerico.ninguno;
            ContadorEsperaMovimiento = TiempoEsperaMovimientoAtaque;
            PuedoMoverme = true;
            foreach (AnimationClip clip in EnemigoAnimator.runtimeAnimatorController.animationClips)
            {
                if (clip.name == "Atacando Arriba")
                {
                    atacandoClip = clip;
                }
            }
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
                        || EstadoEnemigo.Estado == estadoGenerico.ninguno)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position,
                            ObjetivoPerseguir.position,
                            VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        Vector3 refAnimacion = ObjetivoPerseguir.position - vectorTemporal;
                        cambiaAnimaciones(refAnimacion);
                        if (EnemigoRigidBody != null) 
                        {
                            EnemigoRigidBody.MovePosition(transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }
                        EstadoEnemigo.Estado = estadoGenerico.caminando;
                    }
                }
            }
            else
            {
                if (Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) <= RadioPersecucion
                        && Vector3.Distance(ObjetivoPerseguir.position, gameObject.transform.position) <= RadioAtaque)
                {
                    if (EstadoEnemigo != null)
                    {
                        if (EstadoEnemigo.Estado == estadoGenerico.caminando
                             || EstadoEnemigo.Estado == estadoGenerico.ninguno)
                        {
                            manejadorAudioMelee.reproduceAudioMelee();
                            EstadoEnemigo.Estado = estadoGenerico.atacando;
                            StartCoroutine(ataca());
                        }
                    }
                }
            }
        }
    }

    private IEnumerator ataca() 
    {
        if (EnemigoAnimator != null) 
        {
            EnemigoAnimator.SetBool("Atacar", true);
        }
        if (atacandoClip != null)
        {
            yield return new WaitForSeconds(atacandoClip.length);
        }
        if (EnemigoAnimator != null)
        {
            EnemigoAnimator.SetBool("Atacar", false);
        }
        if (EstadoEnemigo != null) 
        {
            if (EstadoEnemigo.Estado == estadoGenerico.atacando)
            {
                EstadoEnemigo.Estado = estadoGenerico.ninguno;
            }
        }
    }
}
