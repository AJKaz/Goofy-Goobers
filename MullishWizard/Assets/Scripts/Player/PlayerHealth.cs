using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Entity
{ 
    protected override void Die()
    {
        GetComponent<Rigidbody2D>().MovePosition(Vector2.zero);
        ResetHealth();
    }
}
