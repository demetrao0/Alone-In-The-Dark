using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movimiento : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    //public bool colPies = false;

    private Rigidbody2D rigidBody;
   // private bool Derecha = true;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        procesarMovimiento();
        ProcesarSalto();
        

    }
  
    void ProcesarSalto()
    {
        //colPies = CheckGround.colPies;

        if (Input.GetButtonDown("Jump") /*&& colPies*/)
        {
            rigidBody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }

    }
 

    void procesarMovimiento()
    {

        float inputMovimiento = Input.GetAxis("Horizontal");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(inputMovimiento * velocidad, rigidbody.velocity.y);
       // GestionarOrientacion(inputMovimiento);
    }

   /* void GestionarOrientacion(float inputMovimiento)
    {
        if ((Derecha = true && inputMovimiento < 1)|| (Derecha == false && inputMovimiento > -1))
        {
            Derecha = !Derecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }*/
    }




