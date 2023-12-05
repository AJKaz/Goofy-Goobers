using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entity : MonoBehaviour {
    [SerializeField]
    protected float maxHealth = 10;

    [SerializeField]
    Slider healthBar;

    protected float health;

    public float Health {
        get => health;
        set => health = value;
    }

    public float MaxHealth => maxHealth;

    protected virtual void Start() {
        health = maxHealth;
    }

    /// <summary>
    /// Handles an entity taking damage
    /// </summary>
    /// <param name="damage">Amount of damage to take</param>
    /// <returns>Returns true if entity died, false otherwise</returns>
    public virtual bool TakeDamage(float damage) {
        health -= damage;
        if (healthBar) healthBar.value = health / maxHealth;
        if (health <= 0) {
            Die();
            return true;
        }
        return false;
    }

    protected abstract void Die();
}
