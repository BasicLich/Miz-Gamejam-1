using UnityEngine;
using UnityEngine.Tilemaps;


public class TreasureRoom : Room
{
    TileBase floorTile;
    public int value;
    public int[] possibleValues = {20, 20, 50, 50, 100, 100, 200, 500};

    public TreasureRoom(Rect roomRect, TileBase floorTile)
    {
        this.roomRect = roomRect;
        this.floorTiles = new TileBase[(int)roomRect.width - 2, (int)roomRect.height - 2];
        this.floorTile = floorTile;
        this.value = possibleValues[Random.Range(0, possibleValues.Length)];

        createRoom();
    }
    public void createRoom()
    {
        for (int x = 0; x < this.floorTiles.GetLength(0); x++)
            for (int y = 0; y < this.floorTiles.GetLength(1); y++)
            {
                this.floorTiles[x,y] = this.floorTile;
            }
    }
}