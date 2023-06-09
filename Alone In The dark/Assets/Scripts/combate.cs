using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combate : MonoBehaviour
{

    public Transform posicion;
    public float radioGolpe;
    public float da�oGolpe;



    private void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Golpe();
        }

    }
    private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(posicion.position, Mathf.Abs(da�oGolpe));

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<EnemigoM>().TomarDa�o(da�oGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(posicion.position, radioGolpe);
    }


}
