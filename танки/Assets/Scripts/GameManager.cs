using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool overPl = true;  
    [SerializeField] enemy enemy;
    [SerializeField] GameObject GameOver;

    void Start()
    {
        InvokeRepeating("spawnEnemy", 1, 2);
        GameOver.SetActive(false);
    }
    void Update()
    {

    }
    void spawnEnemy()
    {      
        
        if (Random.Range(0, 2) == 0)
        {
            Instantiate(enemy, new Vector3(10, 3, 0), Quaternion.identity);
        }   
        else
        {
            Instantiate(enemy, new Vector3(10, -3, 0), Quaternion.identity);
        }
    }
    public void gameOver()
    {
        GameOver.SetActive(true);
        overPl = false;
        CancelInvoke();        
    }
    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
