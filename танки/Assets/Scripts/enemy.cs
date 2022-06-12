using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] Transform turret;
    public static float speed = 3;
    private Tank player;
    float timer = 0;
    [SerializeField] float cooldown;
    [SerializeField] TankShell tankShell;
    [SerializeField] Transform spawnPoint;
    void Start()
    {
        transform.Rotate(Vector3.up, 180);
        player = FindObjectOfType<Tank>();
    }


    void Update()
    {
        if (player == null)
        {
            return;
        }
        Shot();
        TurretRot();
        transform.Translate(speed * Time.deltaTime, 0, 0);

    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("exit"))
        {
            Destroy(gameObject);
        }


    }

    private void OnTriggerEnter2D(Collider2D coll)
    {


        if (coll.CompareTag("shell"))
        {
            Destroy(gameObject);    
        }

    }
    void TurretRot()
    {        
        Vector3 direction = player.transform.position - transform.position;
        turret.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
    void Shot()
    {
        if (timer < cooldown)
        {
            timer += Time.deltaTime;
            return;
        }       
        
        timer = 0;
        TankShell shell = Instantiate(tankShell, spawnPoint.position, Quaternion.identity);
        shell.gameObject.transform.Rotate(spawnPoint.rotation.eulerAngles, Space.World);
        shell.Shot(spawnPoint.right);
        
    }
}
