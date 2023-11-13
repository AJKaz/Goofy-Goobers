using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVector : MonoBehaviour
{
    // If multiple enemies are spawned in a GroupManager group, enemy
    // n's spawn position will be offset by spawnVectorOffset * n
    [SerializeField]
    private Vector2 spawnVectorOffset = new Vector2(0,0);
    public Vector2 SpawnVectorOffset { get { return spawnVectorOffset; } }
}
