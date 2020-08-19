using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* The first room of a dungeon, where the player spawns. Contains stairs down and player spawn pos */
public class FirstRoom : Room
{
    Vector2Int spawnPos;
    Vector2Int stairsDownPos;
    public FirstRoom(Rect roomRect, TileBase floorTile)
    {
        createRoom(roomRect, floorTile);
    }
    public void createRoom(Rect roomRect, TileBase floorTile)
    {
        this.roomRect = roomRect;
        this.stairsDownPos = new Vector2Int((int)roomRect.center.x, (int)roomRect.center.y);
        this.spawnPos = stairsDownPos + new Vector2Int(1,1);

        this.floorTiles = new TileBase[(int)roomRect.width - 2, (int)roomRect.height - 2];

        for (int x = 0; x < this.floorTiles.GetLength(0); x++)
            for (int y = 0; y < this.floorTiles.GetLength(1); y++)
            {
                this.floorTiles[x,y] = floorTile;
            }

    }
}