using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class SimpleWalkAnimation : MonoBehaviour
{
    public bool rotateTowardsMovement = false;
    [SerializeField] private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemy currentEnemy = GetComponent<Enemy>();

        if (rotateTowardsMovement)
        {
            // Lerping is for smooth rotation
            //if (currentEnemy.Direction.x >= 0)
            //{
                sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(currentEnemy.Direction.y, currentEnemy.Direction.x) * Mathf.Rad2Deg), Time.deltaTime * 10);
            //}
            //else
            //{
            //    sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(currentEnemy.Direction.y, currentEnemy.Direction.x) * Mathf.Rad2Deg + 180), Time.deltaTime * 10);
            //}
        }
        else
        {
            sprite.flipX = currentEnemy.Direction.x < 0;
        }

        
    }
}
