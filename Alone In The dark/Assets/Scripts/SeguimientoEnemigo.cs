using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguimientoEnemigo : MonoBehaviour
{

    Vector2 Enemypos;
    public GameObject PlayerM;
    bool persegirP;
    public int vel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (persegirP)
        {
            transform.position = Vector2.MoveTowards(transform.position, Enemypos, vel * Time.deltaTime);
        }

        if (Vector2.Distance (transform.position, Enemypos) > 12F){

            persegirP = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Enemypos = PlayerM.transform.position;
            persegirP = true;
        }
    }

}
