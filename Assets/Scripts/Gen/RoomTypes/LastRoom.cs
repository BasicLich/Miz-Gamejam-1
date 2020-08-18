using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Last room a player enters. Contains stairs up/level exit
 * Maybe contains boss placement?
 * Maybe it's better to not have special types for first and last room
 */

public class LastRoom : Room
{
    Vector2 stairsUpPos;

    public LastRoom(Rect roomRect, TileBase floorTile)
    {
        createRoom(roomRect, floorTile);
    }
    public void createRoom(Rect roomRect, TileBase floorTile)
    {
        this.roomRect = roomRect;
        this.stairsUpPos = new Vector2Int((int)roomRect.center.x, (int)roomRect.center.y);

        this.floorTiles = new TileBase[(int)roomRect.width - 2, (int)roomRect.height - 2];

        for (int x = 0; x < this.floorTiles.GetLength(0); x++)
            for (int y = 0; y < this.floorTiles.GetLength(1); y++)
            {
                this.floorTiles[x,y] = floorTile;
            }

    }
}