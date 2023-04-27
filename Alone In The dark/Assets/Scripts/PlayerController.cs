using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [Header("VALORES CONFIGURABLES")]
    [SerializeField] private float velocidad;
    [SerializeField] private float Salto;
    [SerializeField] private bool saltoMejorado;
    [SerializeField] private float saltoLargo = 1.5f;
    [SerializeField] private float saltoCorto = 1f;
    [SerializeField] private Transform checkGround;
    [SerializeField] private float checkGroundRadio;
    [SerializeField] private LayerMask capaSuelo;
    [SerializeField] private float fuersaToque;
    [SerializeField] private int vida = 1;

    [Header("VARIABLES INFORMATIVAS")]

    private Rigidbody2D rPlayer;
    private Animator aPlayer;
    private SpriteRenderer sPlayer;
    private float h;
    private CapsuleCollider2D ccPlayer;
    private Camera camara;

    private bool tocaSuelo = false;
    private bool miraDerecha = true;
    private bool saltando = false;
    private bool puedoSaltar = false;
    private Vector2 nuevaVelocidad;
    private Vector3 posIni;

    private bool tocando = false;
    private Color colorOriginal;
    private bool muerto = false;
    private float posPlayer, altoCam, altoPlayer;



    // Start is called before the first frame update
    void Start()
    {
        posIni = transform.position;
        rPlayer = GetComponent<Rigidbody2D>();
        aPlayer = GetComponent<Animator>();
        sPlayer = GetComponent<SpriteRenderer>();
        ccPlayer = GetComponent<CapsuleCollider2D>();
        colorOriginal = sPlayer.color;
        camara = Camera.main;
        altoCam = camara.orthographicSize * 2;
        altoPlayer = GetComponent<Renderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Jugador viendo en direccion de movimiento
        if (GameController.gameOn)
        {


            recibePulsaciones();
            variablesAnimador();
        }
        if (muerto)
        {
            posPlayer = camara.transform.InverseTransformDirection(transform.position - camara.transform.position).y;
            if (posPlayer < ((altoCam / 2) * -1) - (altoPlayer / 2)) {
                Invoke("llamaRecarga", 1);
            }
        }
      
    }
    private void llamaRecarga()
    {
        GameController.playerMuerto=true;
    }


    void FixedUpdate()
    {
        if (GameController.gameOn)
        {
            checkTocaSuelo();
            movimientoPlayer();
            movimientoPlayer();
        }
    }

    private void movimientoPlayer()
    {
        if (tocaSuelo && !saltando)
        {
            nuevaVelocidad.Set(velocidad * h, 0.0f);
            rPlayer.velocity = nuevaVelocidad;
        } else
        { if (!tocaSuelo)
            {
                nuevaVelocidad.Set(velocidad * h, rPlayer.velocity.y);
                rPlayer.velocity = nuevaVelocidad;
            }
        }
    }

    

    private void vueltaJugador()
    {
        miraDerecha = !miraDerecha;
        Vector3 escalaGiro = transform.localScale;
        escalaGiro.x = escalaGiro.x * -1;
        transform.localScale = escalaGiro;

    }
    private void recibePulsaciones()
    {
        //if (Input.GetButton("Fire2")) reaparece();
        h = Input.GetAxisRaw("Horizontal");
        if ((h > 0 && !miraDerecha) || (h < 0 && miraDerecha)) vueltaJugador();
        if (Input.GetButtonDown("Jump") && puedoSaltar) Saltar();
        if (saltoMejorado) saltoMejorados();
        
    }


    private void Saltar()
    {
        saltando = true;
        puedoSaltar = false;
        rPlayer.velocity = new Vector2(rPlayer.velocity.x, 0f);
        rPlayer.AddForce(new Vector2(0, Salto), ForceMode2D.Impulse);

    }
    private void saltoMejorados()
    {

        if (rPlayer.velocity.y < 0)
        {
            rPlayer.velocity += Vector2.up * Physics2D.gravity.y * saltoLargo * Time.deltaTime;
        }
        else if (rPlayer.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rPlayer.velocity += Vector2.up * Physics2D.gravity.y * saltoCorto * Time.deltaTime;
        }

    }

    private void checkTocaSuelo()
    {
        tocaSuelo = Physics2D.OverlapCircle(checkGround.position, checkGroundRadio, capaSuelo);
        if (rPlayer.velocity.y <= 0f)
        {
            saltando = false;
            if (tocando)
            {
                rPlayer.velocity = Vector2.zero;
                tocando = false;
                sPlayer.color = colorOriginal;
            }
        }
        if (tocaSuelo && !saltando)
        {
            puedoSaltar = true;
            
        }
    } 


    private void variablesAnimador()
    {
        aPlayer.SetFloat("VelocidadX", Mathf.Abs(rPlayer.velocity.x));
        aPlayer.SetFloat("VelocidadY", rPlayer.velocity.y);
        aPlayer.SetBool("Saltando", saltando);
        
    }

   

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(checkGround.position, checkGroundRadio);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Respawn") // Caida al vacio
        {
            Debug.Log("Muerte por caida al vacio");
            Invoke("llamaRecarga", 1);

        }
        if (collision.gameObject.tag == "Enemigo")
        {
            
            Debug.Log("enemigo golpeo");
            //reaparece();
            tocado(collision.transform.position.x);
        }
        if (collision.gameObject.tag == "CabezaEnemigo")
        {
            rPlayer.velocity = Vector2.zero;
            rPlayer.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
            collision.gameObject.SendMessage("Muere");
        }

    }

    private void tocado (float posX)
    {
        if (!tocando)
        {
            if (vida > 1){
                Color nuevoColor = new Color(255f / 255f, 100 / 255, 100f / 255f);
                sPlayer.color = nuevoColor;
                tocando = true;
                float lado = Mathf.Sign(posX - transform.position.x);
                rPlayer.velocity = Vector2.zero;
                rPlayer.AddForce(new Vector2(fuersaToque * -lado, fuersaToque), ForceMode2D.Impulse);
            }else
            {
                muertePlayer();
            }
        }
    }
    private void muertePlayer()
    {
        GameController.gameOn = false;
        //aPlayer.Play("Muerto");
        rPlayer.velocity = Vector2.zero;
        rPlayer.AddForce(new Vector2(0.0f, Salto), ForceMode2D.Impulse);
        ccPlayer.enabled = false;
        muerto = true;
    }

   
   

}

