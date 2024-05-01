using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float health = 100f;
    private EnemyController enemyController;
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        if(healthSlider != null)
        {
            healthSlider.value = health/100;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (gameObject.CompareTag("Player"))
            {
                GameManager.instance.LoseGame();
            }
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if(healthSlider != null)
        {
            healthSlider.value = health/100;
        }
        if (enemyController != null)
        {
            if (health<= 0)
            {
                enemyController.Die();
            }
            else
            {
                enemyController.ReceiveDamage();
            }
        }
    }
    
}
