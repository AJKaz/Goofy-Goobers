using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    void Start()
    {
        Grid grid = new Grid(40, 40, 1, new Vector3(-20, -20f));
    }
}
