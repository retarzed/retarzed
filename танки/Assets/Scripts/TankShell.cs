using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShell : MonoBehaviour
{
    [SerializeField] int speed = 2;
    Rigidbody2D rb;
    GameManager GM;
    void Start()
    {
         
    }
    public void Shot (Vector3 direction)
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.velocity = direction.normalized * speed;
    }
    private void OnTriggerExit2D(Collider2D coll)
    {

        if (coll.CompareTag("enemy"))
        {            
                Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {


        if (coll.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}
