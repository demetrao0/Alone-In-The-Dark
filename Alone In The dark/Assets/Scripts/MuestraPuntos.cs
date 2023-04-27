using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuestraPuntos : MonoBehaviour
{

    [SerializeField] private Transform origen;
    [SerializeField] private Transform destino;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(origen.position, 0.1f);
        Gizmos.DrawSphere(destino.position, 0.1f);
    }

}
