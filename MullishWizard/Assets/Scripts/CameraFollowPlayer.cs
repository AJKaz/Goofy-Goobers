using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float smoothness = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        SetPosition(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        // This is so cursed. Eventually, refactor this to reduce camera stuttering
        // and to actually make sense in a 2d context

        //if ((player.position - transform.position).sqrMagnitude > maxDistFromPlayer * maxDistFromPlayer)
        //{
        //    Vector3 direction = (player.position - transform.position).normalized;

        //    SetPosition(player.position - direction * maxDistFromPlayer);
        //    DebugCanvas.AddDebugText("Cam direction", direction.ToString());
        //}

        Vector2 currentPosition = Vector2.Lerp(transform.position, player.position, smoothness * Time.deltaTime);
        SetPosition(currentPosition);
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
