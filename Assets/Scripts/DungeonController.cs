using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public struct DungeonFloor
{
    public TileBase[,] wallTiles;
    // TileBase[,] corridorFloorTiles;
    public List<Rect> roomRects;
    public List<Room> rooms;
    public List<Rect> corridorRects;
    public DungeonFloor(TileBase[,] wallTiles,
                        // TileBase[,] corridorFloorTiles,
                        List<Rect> roomRects,
                        List<Rect> corridorRects,
                        List<Room> rooms)
    {
        this.wallTiles = wallTiles;
        // this.corridorFloorTiles = corridorFloorTiles;
        this.roomRects = roomRects;
        this.corridorRects = corridorRects;
        this.rooms = rooms;
    }
}

public class DungeonController
{

    RoomDigger digger = new RoomDigger();
    public List<DungeonFloor> dungeonFloors = new List<DungeonFloor>();
    public int activeFloor;
    public int activeRoom, activeCorridor;

    public bool MoveToNextFloor()
    {
        if (dungeonFloors.Count > activeFloor + 1)
        { 
            activeRoom = 0;
            activeCorridor = 0;
            activeFloor++;
            return false;
        }
        else
        {
            GameManager.Instance.transitionToCampScene();
            return true;
        }
    }

    public void generateDungeon(float difficulty, int floors)
    {
        dungeonFloors.Clear();
        activeFloor = 0;
        float floorDifficulty = (difficulty / floors) * 2;

        for (int i = 0; i < floors; i++)
        {
            TileBase[,] floorWallTiles = digger.MakeDungeonFloor(
                DungeonPresets.difficultyToDungeonParams(floorDifficulty));
            List<Rect> floorRoomRects = digger.rooms.ToList();
            List<Rect> floorCorridorRects = digger.corridors.ToList();
            List<Room> rooms = RoomBuilder.BuildRooms(
                floorDifficulty,
                floorWallTiles,
                floorRoomRects,
                floorCorridorRects);

            DungeonFloor dungeonFloor = new DungeonFloor(
                floorWallTiles,
                floorRoomRects,
                floorCorridorRects,
                rooms);
            dungeonFloors.Add(dungeonFloor);
        }
    }

    public DungeonFloor GetActiveDungeonFloor()
    {
        return dungeonFloors[activeFloor];
    }

    public Vector2 GetFloorStart()
    {
        return GetActiveDungeonFloor().rooms[0].roomRect.center;
    }
}
