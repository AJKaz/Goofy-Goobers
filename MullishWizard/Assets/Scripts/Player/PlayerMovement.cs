using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float accelerationK = 50f;


    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private SpriteRenderer playerSprite;

    private Vector2 direction = Vector2.zero;
    private Vector2 inputDirection = Vector2.zero;

    private Vector2 position = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private Vector2 acceleration = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        // Apply acceleration
        acceleration = inputDirection.normalized;
        if (acceleration.sqrMagnitude > 0.01f)
        {
            // Make it easier to change directions
            if (Mathf.Sign(acceleration.y) != Mathf.Sign(velocity.y)) velocity.y = 0;
            if (Mathf.Sign(acceleration.x) != Mathf.Sign(velocity.x)) velocity.x = 0;
            velocity += acceleration * accelerationK * Time.deltaTime;
        }
        else
        {
            // This is to prevent jittery oscillation when velocity is close to 0
            Vector2 deceleration = direction * 2 * accelerationK * Time.deltaTime;
            Vector2 newVelocity = velocity - deceleration;
            velocity = velocity.sqrMagnitude > newVelocity.sqrMagnitude? newVelocity : Vector2.zero;
        }

        // Constrain velocity
        if (velocity.sqrMagnitude > speed * speed)
            velocity = velocity.normalized * speed;

        // Set direction
        if (velocity.sqrMagnitude > 0.01f)
            direction = velocity.normalized;

        // Update position
        position += velocity * Time.deltaTime;
        rigidbody.MovePosition(position);


        // Update sprite
        playerSprite.flipX = direction.x < -0.1f;
    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        inputDirection = callback.ReadValue<Vector2>();
    }
}
