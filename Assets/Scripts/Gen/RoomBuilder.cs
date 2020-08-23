using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// TODO: Stuff like health, weapon, type etc
public static class RoomBuilder
{
    public static List<Room> BuildRooms(float difficulty, TileBase[,] wallTiles, List<Rect> roomRects, List<Rect> corridorRects)
    {
        Dictionary<string, TileBase> floorTiles = TileLoader.LoadFloorTiles("Color");
        List<Room> rooms = new List<Room>();

        // First room should be empty/no enemies
        rooms.Add(new EmptyRoom(roomRects[0], floorTiles["floor"]));

        int totalRooms = roomRects.Count;
        int enemyRooms = (int)Mathf.Floor(Random.Range(0.7f, 0.8f) * totalRooms);
        int treasureRooms = totalRooms - enemyRooms;

        int a = totalRooms;
        for (int i = 1; i < a; i++)
        {
            if (enemyRooms < totalRooms && Random.Range(0.0f, 1.0f) > 0.75f)
            {
                rooms.Add(new TreasureRoom(roomRects[i], floorTiles["floor"]));
                totalRooms--;
            }
            else
            {
                rooms.Add(new EnemyRoom(roomRects[i], floorTiles["floor"], difficulty));
                enemyRooms--;
                totalRooms--;
            }
        }
        return rooms;
    }
}
