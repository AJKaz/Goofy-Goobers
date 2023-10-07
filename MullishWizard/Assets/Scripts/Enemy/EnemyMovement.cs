using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    GameObject trackingTarget;

    private Vector3 direction;
    private Vector3 velocity;
    [SerializeField]
    private float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(trackingTarget);
    }

    void Move(GameObject target)
    {
        // Get direction of the target
        direction = target.transform.position - transform.position;

        // Get the angle the target is facing and rotate the to face the target
        float angle = Mathf.Atan(direction.y/direction.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = transform.forward * angle;

        // Move the enemy towards the target
        velocity = direction.normalized * moveSpeed * Time.deltaTime;
        transform.position += velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // Can be used if we end up having an enemy that "rams" towers for more damage on itial hit
        if (collision.gameObject.CompareTag("Tower")) {
            Debug.Log("enemy tower enter");
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Tower")) {
            //Debug.Log("Tower health: " + collision.gameObject.GetComponent<Tower>().Health);
            collision.gameObject.GetComponent<Tower>().Health -= 1 * Time.deltaTime;
            //collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            // Have tower take damage here, called every frame that enemy is in contact w/ tower,
            // so use deltaTime either here prob in tower damage method
        }
        //collision.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
