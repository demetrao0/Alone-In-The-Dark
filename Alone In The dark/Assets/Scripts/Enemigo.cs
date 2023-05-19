//Script común a todos nuestros enemigos

using UnityEngine;

public class Enemigo : MonoBehaviour
{
    int capaJugador;                                                                    //Variable para guardar el ID de la capa jugador

    public bool muerto = false;                                                         //Variable para saber si el enemigo está muerto
    int muertoID;                                                                       //ID del bool muerto en el animador
    Animator anim;                                                                      //Referencia al Animador
    public Rigidbody2D rb;

    private void Start()
    {
        capaJugador = LayerMask.NameToLayer("Jugador");                                 //Obtenemos el ID de la capa

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();								                //Asignamos el Animator del enemigo a la variable anim
        muertoID = Animator.StringToHash("muerto");					                    //Guardamos el ID de muerto en el animador
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == capaJugador)                                  //Chocamos contra el jugador
        {
            if (transform.position.y < collision.transform.position.y)                  //Si el jugador está por encima, el enemigo muere
            {
                muerto = true;                                                          //Cambiamos la variable muerto
                rb.gravityScale = 1;
                anim.SetBool(muertoID, muerto);                                         //Decimos al animador que el jugador está muerto
                Destroy(gameObject, 0.32f);                                             //Destruimos el objeto enemigo tras la animación
            }
        }
    }
}
