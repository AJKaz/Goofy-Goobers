using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Entity
{
    const float MAX_HEALTH = 100;
    private void Start()
    {
        health = MAX_HEALTH;
    }
    protected override void Die()
    {
        GetComponent<Rigidbody2D>().MovePosition(Vector2.zero);
        health = MAX_HEALTH;
    }
}
