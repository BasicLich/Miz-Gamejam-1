using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DungeonParams
{
    public Vector2Int roomSize;
    public Vector2Int corridorLength;
    public Vector2Int maxSize;
    public Vector2Int roomCount;
    public int tries;
    public int corridorWidth;
    public DungeonParams(Vector2Int roomSize, Vector2Int corridorLength, Vector2Int maxSize,
        Vector2Int roomCount, int tries, int corridorWidth)
    {
        this.roomSize = roomSize;
        this.corridorLength = corridorLength;
        this.maxSize = maxSize;
        this.roomCount = roomCount;
        this.tries = tries;
        this.corridorWidth = corridorWidth;
    }
}