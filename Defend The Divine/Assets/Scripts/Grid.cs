using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private Vector3 originPosition;
    [SerializeField]
    public int width;
    [SerializeField]
    public int height;
    [SerializeField]
    public float cellSize;

    private int[,] gridArray;

    private void Start()
    {
        gridArray = new int[width, height];
    }

    private void Update()
    {
        updateDebug();
    }

    /// <summary>
    /// Returns a the lower left corner of a tile based on world position
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0) * cellSize + originPosition;
    }

    /// <summary>
    /// Returns the center of a tile based on coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GetTileCenter(int x, int y)
    {
        return GetWorldPosition(x, y) + new Vector3(cellSize / 2f, cellSize / 2f, 0);
    }

    /// <summary>
    /// Returns the indecies of the tile containing 'worldPosition'
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    /// <summary>
    /// Sets the value of the grid's 'gridArray' at [x, y]
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="value"></param>
    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
        }
    }

    /// <summary>
    /// Sets the value of the grid's 'gridArray' at the [x, y] tile containing
    /// 'worldPosition'
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="value"></param>
    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        return gridArray[x, y];
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    public bool PositionInGrid(Vector3 worldPosition)
    {
        return worldPosition.x >= originPosition.x
               && worldPosition.y >= originPosition.y
               && worldPosition.x <= originPosition.x + width * cellSize
               && worldPosition.y <= originPosition.y + height * cellSize;
    }

    private Color cellColor = Color.red;
    /// <summary>
    /// Redraws the debug grid lines
    /// </summary>
    public void updateDebug()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (gridArray[x, y] == 1)
                {
                    cellColor = Color.green;
                }
                else
                {
                    cellColor = Color.red;
                }
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), cellColor, 2f, false);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), cellColor, 2f, false);
            }
        }

        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), cellColor, 2f, false);
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), cellColor, 2f, false);
    }
}