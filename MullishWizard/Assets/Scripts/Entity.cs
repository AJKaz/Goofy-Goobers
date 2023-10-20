using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float health = 10;
    protected bool isDead = false;

    public float Health
    {
        get => health;
        set => health = value;
    }

    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected abstract void Die(); 
}
