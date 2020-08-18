using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EmptyRoom : Room
{
    public EmptyRoom(Rect roomRect, TileBase floorTile)
    {
        createRoom(roomRect, floorTile);
    }
    public void createRoom(Rect roomRect, TileBase floorTile)
    {
        this.roomRect = roomRect;
        this.floorTiles = new TileBase[(int)roomRect.width - 2, (int)roomRect.height - 2];

        for (int x = 0; x < this.floorTiles.GetLength(0); x++)
            for (int y = 0; y < this.floorTiles.GetLength(1); y++)
            {
                this.floorTiles[x,y] = floorTile;
            }
    }
}