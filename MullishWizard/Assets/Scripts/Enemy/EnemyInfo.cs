using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    private float health = 10;
    private bool isDead = false;

    public float Health { get { return health; } set { health = value; } }
    public bool IsDead { get { return isDead; } set { isDead = value; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            isDead = true;
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
