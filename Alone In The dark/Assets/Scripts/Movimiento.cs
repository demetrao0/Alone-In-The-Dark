using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour

    public float velocidad;
    public float velocidadmax;

    private RigidBody2D rPlayer;
    private float a;

{
    void Start()
    {
        rPlayer = GetComponent<RigidBody2D>();
    }

    void Update()
    {
       
    }

    public void FixedUpdate();
    {
        a = Imput.GetAxisRaw("Horizontal");
        rPlayer.AddForce(Vector2.right * velocidad * a);
    }
}
