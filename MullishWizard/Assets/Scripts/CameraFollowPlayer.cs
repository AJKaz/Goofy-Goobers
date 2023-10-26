using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{

    [SerializeField] private float smoothness = 20.0f;
    private Transform player;
    private MapBounds mapBounds;

    private Vector2 screenExtents;


    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        mapBounds = GameObject.Find("MapBounds").GetComponent<MapBounds>();
        screenExtents = new Vector2(Camera.main.orthographicSize * Screen.width / (float)Screen.height, Camera.main.orthographicSize);
    }

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
        // Lock the camera to the map bounds
        if (mapBounds)
        {
            if (position.x - screenExtents.x < mapBounds.Min.x)
                position.x = mapBounds.Min.x + screenExtents.x;
            if (position.x + screenExtents.x > mapBounds.Max.x)
                position.x = mapBounds.Max.x - screenExtents.x;
            if (position.y - screenExtents.y < mapBounds.Min.y)
                position.y = mapBounds.Min.y + screenExtents.y;
            if (position.y + screenExtents.y > mapBounds.Max.y)
                position.y = mapBounds.Max.y - screenExtents.y;
        }
        transform.position = new Vector3(position.x, position.y, -10);
    }
}
