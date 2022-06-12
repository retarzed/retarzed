using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tank : MonoBehaviour
{
    [Header("---Move---")]    
    [SerializeField] bool wasd;
    [SerializeField] int speed = 1;
    [SerializeField] int speedRot = 1;
    Rigidbody2D rb;
    [Header("---Turret---")]
    [SerializeField] Transform turret;
    [SerializeField] Transform spawnPoint;
    [SerializeField] TankShell tankShell;    
    [SerializeField] float cooldown;
    float timer=0;
    private int hp = 3;
    Slider hpSlider;
    GameManager GM;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        hpSlider = FindObjectOfType<Slider>();
        rb = GetComponent<Rigidbody2D>();
    }    
    void Update()
    {
        if(transform==null)
        {
            return;
        }
        if (GM.overPl == false)
        {
            return;
        }
        Shot();
        TurretRot();
        Move(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("shell"))
        {
            hp--;
            hpSlider.value = hp;
        }
        if(hp<= 0)
        {
            print("lost");
            GM.gameOver();
            Destroy(gameObject);
        }          
    }
    void Shot()
    {
        if(timer<cooldown)
        {
            timer += Time.deltaTime;
            return;
        }
        if(Input.GetMouseButtonDown(0))
        {
            timer = 0;
            TankShell shell = Instantiate(tankShell, spawnPoint.position, Quaternion.identity);
            shell.gameObject.transform.Rotate(spawnPoint.rotation.eulerAngles, Space.World);
            shell.Shot(spawnPoint.right);
        }
    }
    void Move(KeyCode left, KeyCode right, KeyCode up, KeyCode down)
    {
        float v = 0;
        if (Input.GetKey(up)|| Input.GetKey(down))
        {
            if (Input.GetKey(up))
                v = 1;
            else
                v = -1;
            rb.AddForce(transform.right * speed * v, ForceMode2D.Impulse);
        }
         v = 0;
        if (Input.GetKey(right) || Input.GetKey(left))
        {
            if (Input.GetKey(left))
                v = 1;
            else
                v = -1;
            rb.AddTorque(speedRot * v, ForceMode2D.Impulse);
        }        
    }
    void TurretRot()
    {
        Vector2 pos = Input.mousePosition;
        pos = Camera.main.ScreenToWorldPoint(pos) - transform.position;
        turret.rotation = Quaternion.LookRotation(Vector3.forward, pos);
    }     
}
