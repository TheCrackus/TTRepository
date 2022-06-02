using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrepaCielosCorrupto : Enemigo
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
        EstadoEnemigo.Estado = EstadoGenerico.durmiendo;
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        enemigoRigidBody = GetComponent<Rigidbody2D>();
        enemigoAnimator = GetComponent<Animator>();
        enemigoAnimator.SetBool("Despertar", true);
    }

    public override void Start()
    {
        base.Start();
        EstadoEnemigo.Estado = EstadoGenerico.ninguno;
        objetivoPerseguir = GameObject.FindWithTag("Player").transform;
        enemigoRigidBody = gameObject.GetComponent<Rigidbody2D>();
        enemigoAnimator = gameObject.GetComponent<Animator>();
        enemigoAnimator.SetBool("Despertar", true);
    }

    public virtual void FixedUpdate()
    {
        if (PuedoMoverme)
        {
            gestionarDistancias();
            enemigoRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else 
        {
            enemigoRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public virtual void gestionarDistancias()
    {
        if (objetivoPerseguir != null) 
        {
            if (Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) <= radioPersecucion
                && Vector3.Distance(objetivoPerseguir.position, gameObject.transform.position) >= radioAtaque)
            {
                if (EstadoEnemigo != null) 
                {
                    if (EstadoEnemigo.Estado == EstadoGenerico.caminando
                        || EstadoEnemigo.Estado == EstadoGenerico.durmiendo
                        || EstadoEnemigo.Estado == EstadoGenerico.ninguno)
                    {
                        Vector3 vectorTemporal = Vector3.MoveTowards(gameObject.transform.position,
                            objetivoPerseguir.position,
                            VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        Vector3 refAnimacion = objetivoPerseguir.position - vectorTemporal;
                        cambiarAnimaciones(refAnimacion);
                        if (enemigoRigidBody != null) 
                        {
                            enemigoRigidBody.MovePosition(transform.position + refAnimacion.normalized * VelocidadMovimientoEnemigo * Time.fixedDeltaTime);
                        }
                        EstadoEnemigo.Estado = EstadoGenerico.caminando;
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
                        EstadoEnemigo.Estado = EstadoGenerico.durmiendo;
                    }
                }
            }
        }
    }

    private void verificarAnimacion(Vector2 vector) 
    {
        enemigoAnimator.SetFloat("MoviminetoX", vector.x);
        enemigoAnimator.SetFloat("MovimientoY", vector.y);
    }

    public void cambiarAnimaciones(Vector2 vectorMovimiento) 
    {
        if (Mathf.Abs(vectorMovimiento.x) > Mathf.Abs(vectorMovimiento.y))
        {
            if (vectorMovimiento.x > 0)
            {
                verificarAnimacion(Vector2.right);
            }
            else 
            {
                if (vectorMovimiento.x < 0) 
                {
                    verificarAnimacion(Vector2.left);
                }
            }
        }
        else
        {
            if (Mathf.Abs(vectorMovimiento.x) < Mathf.Abs(vectorMovimiento.y))
            {
                if (vectorMovimiento.y > 0)
                {
                    verificarAnimacion(Vector2.up);
                }
                else
                {
                    if (vectorMovimiento.y < 0)
                    {
                        verificarAnimacion(Vector2.down);
                    }
                }
            }
        }
    }
}