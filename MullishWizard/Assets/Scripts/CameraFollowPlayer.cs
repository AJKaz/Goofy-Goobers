using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float maxDistFromPlayer = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        setPosition(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        // This is so cursed. Eventually, refactor this to reduce camera stuttering
        // and to actually make sense in a 2d context

        if ((player.position - transform.position).sqrMagnitude > maxDistFromPlayer * maxDistFromPlayer)
        {
            Vector3 direction = (player.position - transform.position).normalized;

            setPosition(player.position - direction * maxDistFromPlayer);
            DebugCanvas.AddDebugText("Cam direction", direction.ToString());
        }
    }

    private void setPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
