using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid
{
    public int width;
    public int height;
    private Vector3 originPosition;
    private int[,] gridArray;

    public float cellSize;

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];

        // updateDebug();
    }

    /// <summary>
    /// Returns a world position based on a tile coordinate
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y, 0) * cellSize + originPosition;
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
        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x, y] = value;
        }

        // updateDebug();
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

    //private Color cellColor = Color.red;
    ///// <summary>
    ///// Redraws the debug grid lines
    ///// </summary>
    //private void updateDebug()
    //{
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            if (gridArray[x, y] == 1) {
    //                cellColor = Color.green;
    //            } else
    //            {
    //                cellColor = Color.red;
    //            }
    //            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), cellColor, 2f);
    //            Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), cellColor, 2f);
    //        }
    //    }

    //    Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), cellColor, 2f);
    //    Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), cellColor, 2f);
    //}
}
