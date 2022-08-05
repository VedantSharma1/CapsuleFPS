using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTarget : MonoBehaviour
{   //speed
    private float speed = 10;

    //health
    public int health = 100;
    public Vector3 spawnPos1 = new Vector3(0, 1, 40);
    public Vector3 spawnPos2 = new Vector3(0, 1, 40);

    private CapsuleCollider enemyCol;
    private GameManager gameManager;

    //damage slider
    public Slider damageSlider;
    public int totalDamage;
    
    void Start()
    {
        enemyCol = GetComponent<CapsuleCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = RandomSpawnPos();

        damageSlider.maxValue = health;
        damageSlider.value = health;
        damageSlider.fillRect.gameObject.SetActive(true);
        
    }

    public void DamageTaken(int _damage)
    {
        totalDamage += _damage;
        int healthLeft = health - totalDamage;
        
        //damageSlider.fillRect.gameObject.SetActive(true);
        
        damageSlider.value = healthLeft;

        if(healthLeft <= 0)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(1);
        }

        /*health -= -_damage;
        Debug.Log(health);
        if(health == 0)
        {
            Destroy(gameObject);
            gameManager.UpdateScore(1);
        }*/
    }

    /*IEnumerator Die()
    {

    }*/
    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomSpawnPos()
    {
        if(Random.value > 0.5f)
        {
            return spawnPos1;
        }
        else
        {
            return spawnPos1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //trigger collider is being called twice
            gameManager.UpdateLives(-0.5f);
        }   
         
    }
}
