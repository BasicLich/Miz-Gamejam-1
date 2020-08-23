using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public struct EnemySpawnProperties
{
    public Vector2Int spawnLoc;
    public EnemySpawnProperties(Vector2Int spawnLoc)
    {
        this.spawnLoc = spawnLoc;
    }
}

public class EnemyRoom : Room
{
    public EnemySpawnProperties[] enemies;
    TileBase floorTile;
    float difficulty;

    public EnemyRoom(Rect roomRect, TileBase floorTile, float difficulty)
    {
        this.roomRect = roomRect;
        this.floorTiles = new TileBase[(int)roomRect.width - 2, (int)roomRect.height - 2];
        this.floorTile = floorTile;
        this.difficulty = difficulty;

        createRoom();
    }
    public void createRoom()
    {
        for (int x = 0; x < this.floorTiles.GetLength(0); x++)
            for (int y = 0; y < this.floorTiles.GetLength(1); y++)
            {
                this.floorTiles[x,y] = this.floorTile;
                PlaceEnemies();
            }
    }
    void PlaceEnemies()
    {
        int enemyCount = (int)Random.Range(difficulty/3, difficulty/1.5f);
        Vector2Int[] enemyCoords = genUniqueRandomCoords(new Vector2Int[enemyCount], enemyCount);
        this.enemies = new EnemySpawnProperties[enemyCount];
        for (int i = 0; i < enemyCount; i++)
        {
            enemies[i] = new EnemySpawnProperties(enemyCoords[i]);
        }
    }

    Vector2Int[] genUniqueRandomCoords(Vector2Int[] coords, int iterations, int n = 0, int tries = 0)
    {
        if (n == iterations) return coords;
        Vector2Int newCoord = new Vector2Int(
            Random.Range(2, (int)roomRect.width - 2),
            Random.Range(2, (int)roomRect.height - 2));
        for (int i = 0; i < n; i++)
        {
            if (Vector2Int.Distance(coords[i], newCoord) < 3 && tries < 10)
                return genUniqueRandomCoords(coords, iterations, n, tries + 1);
        }
        coords[n] = newCoord;

        return genUniqueRandomCoords(coords, iterations, n + 1);
    }
}