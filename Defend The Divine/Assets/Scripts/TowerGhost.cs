using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGhost : MonoBehaviour
{
    public bool CollidingWithPath {  get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CollidingWithPath = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CollidingWithPath = true;
    }

    // Needed to fix a bug where the tower ghost wouldn't count as "entering"
    // a trigger when already in a box, allowing tower placement in the path.
    private void OnTriggerStay2D(Collider2D collision)
    {
        CollidingWithPath = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CollidingWithPath = false;
    }
}
