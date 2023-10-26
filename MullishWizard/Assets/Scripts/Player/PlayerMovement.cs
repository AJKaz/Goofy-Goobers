using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float accelerationK = 50f;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField]
    private MapBounds mapBounds;

    private Rigidbody2D rigidbody;
    private Animator animator;

    private Vector2 direction = Vector2.zero;
    private Vector2 inputDirection = Vector2.zero;

    private Vector2 position = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private Vector2 acceleration = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        position = transform.position;

        // Apply acceleration
        acceleration = inputDirection.normalized;

        if (acceleration.sqrMagnitude > 0.01f)
        {
            // Make it easier to change directions
            if (Mathf.Sign(acceleration.y) != Mathf.Sign(velocity.y) && acceleration.y != 0) velocity.y = 0;
            if (Mathf.Sign(acceleration.x) != Mathf.Sign(velocity.x) && acceleration.x != 0) velocity.x = 0;
            velocity += acceleration * accelerationK * Time.deltaTime;
        }


        // Split deceleration into different axes because an annoying interaction with edges
        if (Mathf.Abs(acceleration.x) < 0.01f)
        {
            // This is to prevent jittery oscillation when velocity is close to 0
            float deceleration = direction.x * 2f * accelerationK * Time.deltaTime;
            //Vector2 deceleration = direction * 0.1f * accelerationK * Time.deltaTime;
            float newVelocity = velocity.x - deceleration;
            velocity.x = Mathf.Sign(direction.x) == Mathf.Sign(newVelocity) ? newVelocity : 0;
        }
        if (Mathf.Abs(acceleration.y) < 0.01f)
        {
            // This is to prevent jittery oscillation when velocity is close to 0
            float deceleration = direction.y * 2f * accelerationK * Time.deltaTime;
            //Vector2 deceleration = direction * 0.1f * accelerationK * Time.deltaTime;
            float newVelocity = velocity.y - deceleration;
            velocity.y = Mathf.Sign(direction.y) == Mathf.Sign(newVelocity) ? newVelocity : 0;
        }

        // Map bound collisions
        // Honestly, it might just be better to delete this and use colliders for map bounds
        if (mapBounds)
        {
            if (position.x + velocity.x * Time.deltaTime <= mapBounds.Min.x + sprite.bounds.extents.x)
            {
                velocity.x = 0;
                position.x = mapBounds.Min.x + sprite.bounds.extents.x;
                transform.position = position;
            }

            if (position.x + velocity.x * Time.deltaTime >= mapBounds.Max.x - sprite.bounds.extents.x)
            {
                velocity.x = 0;
                position.x = mapBounds.Max.x - sprite.bounds.extents.x;
                transform.position = position;
            }

            if (position.y + velocity.y * Time.deltaTime <= mapBounds.Min.y + sprite.bounds.extents.y)
            {
                velocity.y = 0;
                position.y = mapBounds.Min.y + sprite.bounds.extents.y;
                transform.position = position;
            }

            if (position.y + velocity.y * Time.deltaTime >= mapBounds.Max.y - sprite.bounds.extents.y)
            {
                velocity.y = 0;
                position.y = mapBounds.Max.y - sprite.bounds.extents.y;
                transform.position = position;
            }
        }

        // Constrain velocity
        if (velocity.sqrMagnitude > speed * speed)
            velocity = velocity.normalized * speed;

        // Set direction
        if (velocity.sqrMagnitude > 0.01f)
            direction = velocity.normalized;

        // Update position
        //rigidbody.MovePosition(position);

        rigidbody.velocity = velocity;


        // Update sprite
        sprite.flipX = direction.x < -0.01 ? true : direction.x > 0.01 ? false : sprite.flipX;
        animator.SetBool("isWalking", rigidbody.velocity.sqrMagnitude > 0.01f);
    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        inputDirection = callback.ReadValue<Vector2>();
    }


}
