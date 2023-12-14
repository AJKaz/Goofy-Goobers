using UnityEngine;

public class DemonEssence : MonoBehaviour
{
    //[SerializeField] Vector3 destination;
    [SerializeField] Transform destination;
    [SerializeField] float initialSpeed = 4.0f;
    [SerializeField] float acceleration = 0.1f;

    private float currentSpeed;

    private void Start() {
        currentSpeed = initialSpeed;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination.position, currentSpeed * Time.deltaTime);

        if ((transform.position - destination.position).sqrMagnitude < 0.1f) {
            Destroy(gameObject);
        }

        currentSpeed += acceleration * Time.deltaTime;
    }
}
