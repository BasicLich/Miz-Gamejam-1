using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorDrawer : MonoBehaviour
{
    public Tilemap wallTiles;
    public Tilemap floorTiles;
    public GameObject enemyPrefab;
    // Called when we transition to scene
    void Start()
    {
        DrawFloor();
    }
    void Update() { } 

    public void DrawFloor()
    {
        DungeonFloor dungeonFloor = GameManager.Instance.dungeonController.GetActiveDungeonFloor();
        RenderTiles(wallTiles, dungeonFloor.wallTiles, Vector2Int.zero, true);
        foreach (Room room in dungeonFloor.rooms)
        {
            DrawRoom(room);
        }
    }

    public void DrawRoom(Room room)
    {
        int botX = (int)room.roomRect.xMin;
        int botY = (int)room.roomRect.yMin;
        if (room is EnemyRoom enemyRoom)
        {
            foreach (EnemySpawnProperties enemy in enemyRoom.enemies)
            {
                Instantiate(enemyPrefab, new Vector3(botX + enemy.spawnLoc.x, botY + enemy.spawnLoc.y, 0), Quaternion.identity);
            }
        }
        RenderTiles(floorTiles, room.floorTiles, new Vector2Int(botX + 1, botY + 1));
    }

    public void RenderTiles(Tilemap tilemap, TileBase[,] tiles, Vector2Int bottomLeft, bool clear = false)
    {
        if (clear) tilemap.ClearAllTiles();

        int width = tiles.GetUpperBound(0);
        int height = tiles.GetUpperBound(1);

        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                tilemap.SetTile(new Vector3Int(bottomLeft.x + x, bottomLeft.y + y, 0), tiles[x, y]);
            }
        }
    }
}
