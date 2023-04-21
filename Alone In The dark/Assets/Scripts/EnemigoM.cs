using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoM : MonoBehaviour
{
    public float Vida;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TomarDaño(float daño)
    {
        Vida -= daño;
        if (Vida<=1)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        animator.SetTrigger("Muerte");
    }

}
