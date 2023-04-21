using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combate : MonoBehaviour
{

    public Transform posicion;
    public float radioGolpe;
    public float dañoGolpe;



    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Golpe();
        }

    }
    private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(posicion.position, Mathf.Abs(dañoGolpe));

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<EnemigoM>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicion.position, radioGolpe);
    }


}
