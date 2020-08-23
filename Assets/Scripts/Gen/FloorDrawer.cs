using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorDrawer : MonoBehaviour
{
    public Tilemap wallTilemap;
    public Tilemap floorTilemap;
    public Tilemap exitTilemap;
    public GameObject enemyPrefab;
    public GameObject treasurePrefab;
    public List<GameObject> instantiatedEnemies = new List<GameObject>();
    Dictionary<string, TileBase> exitTiles;
    int currentFloor = 0;
    // Called when we transition to scene
    void Start()
    {
        exitTiles = TileLoader.LoadExitTiles("color");
        DrawFloor();
        currentFloor = 0;
    }
    void Update()
    {
        if (GameManager.Instance.dungeonController.activeFloor > currentFloor)
        {
            CleanUpCurrentFloor();
            DrawFloor();
            currentFloor++;
        }
    } 

    public void CleanUpCurrentFloor()
    {
        wallTilemap.ClearAllTiles();
        floorTilemap.ClearAllTiles();
        exitTilemap.ClearAllTiles();
        foreach (GameObject enemy in instantiatedEnemies)
        {
            Destroy(enemy);
        }
        instantiatedEnemies.Clear();
        foreach(GameObject coin in GameObject.FindGameObjectsWithTag("Coin"))
        {
            Destroy(coin);
        }
        foreach(GameObject treasure in GameObject.FindGameObjectsWithTag("Treasure"))
        {
            Destroy(treasure);
        }
    }

    public void DrawFloor()
    {
        DungeonFloor dungeonFloor = GameManager.Instance.dungeonController.GetActiveDungeonFloor();
        RenderTiles(wallTilemap, dungeonFloor.wallTiles, Vector2Int.zero, true);
        foreach (Room room in dungeonFloor.rooms)
        {
            DrawRoom(room);
        }
        if ( currentFloor + 2 == GameManager.Instance.dungeonController.dungeonFloors.Count)
        {
            PlaceFinalExit(dungeonFloor.rooms[dungeonFloor.rooms.Count - 1]);
        }
        else
        {
            PlaceExit(dungeonFloor.rooms[dungeonFloor.rooms.Count - 1]);
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
                GameObject newEnemy = Instantiate(
                    enemyPrefab,
                    new Vector3(botX + enemy.spawnLoc.x, botY + enemy.spawnLoc.y, 0),
                    Quaternion.identity);
                    instantiatedEnemies.Add(newEnemy);
            }
        }
        else if (room is TreasureRoom treasureRoom)
        {
            GameObject treasure = Instantiate(treasurePrefab,  room.roomRect.center + Vector2.left * 2, Quaternion.identity);
            treasure.GetComponent<TreasureController>().SetValue(treasureRoom.value);
        }

        RenderTiles(floorTilemap, room.floorTiles, new Vector2Int(botX + 1, botY + 1));
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

    void PlaceExit(Room room)
    {
        exitTilemap.SetTile(new Vector3Int((int)room.roomRect.center.x, (int)room.roomRect.center.y, 0), exitTiles["stairsUp"]);
    }
    void PlaceFinalExit(Room room)
    {
        exitTilemap.SetTile(new Vector3Int((int)room.roomRect.center.x, (int)room.roomRect.center.y, 0), exitTiles["ladder"]);
    }
}
