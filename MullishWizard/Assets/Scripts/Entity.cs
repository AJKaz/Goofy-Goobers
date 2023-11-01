using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth = 10;
    [SerializeField]
    Slider healthBar;

    protected bool isDead = false;
    protected float health;
    
    public float Health
    {
        get => health;
        set => health = value;
    }

    public float MaxHealth => maxHealth;

    public bool IsDead
    {
        get => isDead;
        set => isDead = value;
    }

    protected virtual void Start()
    {
        ResetHealth();
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (healthBar) healthBar.value = health / maxHealth;
        if (health <= 0) Die();
    }

    protected void ResetHealth()
    {
        health = MaxHealth;
        if (healthBar) healthBar.value = 1;
    }

    protected abstract void Die(); 
}
