using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trepaCielosCorrupto : enemigo
{

    private Rigidbody2D enemigoRigidBody;

    private Transform objetivoPerseguir;

    private Animator enemigoAnimator;

    [Header("Distancia de persecucion")]
    [SerializeField] private float radioPersecucion;

    [Header("Distancia de ataque")]
    [SerializeField] private float radioAtaque;

    public Rigidbody2D EnemigoRigidBody { get => enemigoRigidBody; set => enemigoRigidBody = value; }
    public Transform ObjetivoPerseguir { get => objetivoPerseguir; set => objetivoPerseguir = value; }
    public Animator EnemigoAnimator { get => enemigoAnimator; set => enemigoAnimator = value; }
    public float RadioPersecucion { get => radioPersecucion; set => radioPersecucion = value; }
    public float RadioAtaque { get => radioAtaque; set => radioAtaque = value; }

    public override void OnEnable()
    {
        base.OnEnable();
        EstadoEnemigo.Estado = estadoGenerico.durmiendo;
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        enemigoRigidBody = GetComponent<Rigidbody2D>();
        enemigoAnimator = GetComponent<Animator>();
        enemigoAnimator.SetBool("Despertar", true);
    }

    public override void Start()
    {
        base.Start();
        EstadoEnemigo.Estado = estadoGenerico.ninguno;
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        enemigoRigidBody = gameObject.GetComponent<Rigidbody2D>();
        enemigoAnimator = gameObject.GetComponent<Animator>();
        enemigoAnimator.SetBool("Despertar", true);
    }

    public virtual void FixedUpdate()
    {
        if (PuedoMoverme)
        {
            gestionDistancias();
            enemigoRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else 
        {
            enemigoRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public virtual void gestionDistancias()
    {
        if (objetivoPerseguir != null) 
        {
            if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) <= radioPersecucion
                && Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) >= radioAtaque)
            {
                if (EstadoEnemigo != null) 
                {
                    if (EstadoEnemigo.Estado == estadoGenerico.caminando
                        || EstadoEnemigo.Estado == estadoGenerico.durmiendo
                        || EstadoEnemigo.Estado == estadoGenerico.ninguno)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position,
                            objetivoPerseguir.position,
                            VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        Vector3 refAnimacion = objetivoPerseguir.position - vectorTemporal;
                        cambiaAnimaciones(refAnimacion);
                        if (enemigoRigidBody != null) 
                        {
                            enemigoRigidBody.MovePosition(transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }
                        EstadoEnemigo.Estado = estadoGenerico.caminando;
                        if (enemigoAnimator != null) 
                        {
                            enemigoAnimator.SetBool("Despertar", true);
                        }
                    }
                }
            }
            else
            {
                if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) > radioPersecucion)
                {
                    if (enemigoAnimator != null)
                    {
                        enemigoAnimator.SetBool("Despertar", false);
                    }
                    if (EstadoEnemigo != null)
                    {
                        EstadoEnemigo.Estado = estadoGenerico.durmiendo;
                    }
                }
            }
        }
    }

    private void enviaAnimacion(Vector2 vector) 
    {
        enemigoAnimator.SetFloat("MoviminetoX", vector.x);
        enemigoAnimator.SetFloat("MovimientoY", vector.y);
    }

    public void cambiaAnimaciones(Vector2 vectorMovimiento) 
    {
        if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
        {
            if (vectorMovimiento.x > 0)
            {
                enviaAnimacion(Vector2.right);
            }
            else 
            {
                if (vectorMovimiento.x < 0) 
                {
                    enviaAnimacion(Vector2.left);
                }
            }
        }
        else
        {
            if (Mathf.Abs(vectorMovimiento.x) < Mathf.Abs(vectorMovimiento.y))
            {
                if (vectorMovimiento.y > 0)
                {
                    enviaAnimacion(Vector2.up);
                }
                else
                {
                    if (vectorMovimiento.y < 0)
                    {
                        enviaAnimacion(Vector2.down);
                    }
                }
            }
        }
    }
}