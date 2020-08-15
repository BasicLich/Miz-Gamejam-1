using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class ArrayUtils
{
    public static TileBase[,] Merge2DArraysFunctional(TileBase[,] a, TileBase[,] b, Vector2Int botLeft) {
        int width = b.GetLength(0);
        int height = b.GetLength(1);
        TileBase[,] result = (TileBase[,]) a.Clone();

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
               if (b[x,y] != null) result[botLeft.x + x, botLeft.y + y] = b[x,y];

        return result;
    }

    public static void Merge2DArrays(TileBase[,] a, TileBase[,] b, Vector2Int botLeft, bool always = false) {
        int width = b.GetLength(0);
        int height = b.GetLength(1);
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            { 
                if (always) a[botLeft.x + x, botLeft.y + y] = b[x,y];
                else if (b[x,y] != null) a[botLeft.x + x, botLeft.y + y] = b[x,y];
            }
    }
}
