using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Tower
{
    private void Start()
    {
        health = 25; //2.5x tower's hp
    }
    protected override void Update(){/*Do nothing!*/}
    protected override void Shoot(){/*Do nothing!*/}
    protected override Enemy GetTarget(){ return null; }
    // Currently, Wall is just a tower that doesnt shoot
    // so all code is same as Tower
    // This can change in future (if we make multiple wall types, there
    // should probably be a base wall class)
}
