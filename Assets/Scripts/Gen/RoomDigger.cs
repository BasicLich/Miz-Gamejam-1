using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Size
{
    SMALL,
    MEDIUM,
    LARGE
}

public enum Dir
{
    LEFT,
    RIGHT,
    UP,
    DOWN,
}


public class RoomDigger
{
    public Dictionary<Dir, Vector2Int> directions = new Dictionary<Dir, Vector2Int>(){
        { Dir.LEFT, Vector2Int.left },
        { Dir.RIGHT, Vector2Int.right },
        { Dir.UP, Vector2Int.up },
        { Dir.DOWN, Vector2Int.down },
    };

    public List<Rect> rooms;
    public List<Rect> corridors;

    Vector2Int corridorSize = new Vector2Int(4,15);
    Vector2Int roomSize = new Vector2Int(6,10);
    Vector2Int maxSize = new Vector2Int(64, 64);
    TileBase placeholder = TileLoader.LoadTile("Tiles/Color/", "placeholder");
    System.Random random = new System.Random();
    // Array dirs = Enum.GetValues(typeof(Dir));

    public TileBase[,] TryMakeRoom(Vector2Int roomSize, Vector2Int corridorSize, Vector2Int maxSize)
    {
        this.roomSize = roomSize;
        this.corridorSize = corridorSize;
        this.maxSize = maxSize;

        rooms = new List<Rect>();
        corridors = new List<Rect>();

        TileBase[,] slate = new TileBase[maxSize.x, maxSize.y];
        return GenBigRoom(slate, new Vector2Int(roomSize.y, roomSize.y), Dir.RIGHT);
    }


    private bool checkRoomCollision(Rect newRoom)
    {
        foreach (Rect room in rooms)
        {
            if (newRoom.Overlaps(room)) return true;
        }
        rooms.Add(newRoom);
        return false;
    }


    // FIXME: Coordinates may end up negative here. Get some handling
    // Returns room aligned so that position overlaps with center of entry wall
    public Rect findRoomRect(Vector2Int pos, int width, int height, Dir dir)
    {
        Rect roomRect;
        switch (dir)
        {
            case Dir.LEFT:
            {
                roomRect = new Rect(pos.x - width, pos.y - height/2 + 1, width, height);
                break;
            }

            case Dir.RIGHT:
            {
                roomRect = new Rect(pos.x, pos.y - height/2 + 1, width, height);
                break;
            }

            case Dir.UP:
            {
                roomRect = new Rect(pos.x - width/2 + 1, pos.y, width, height);
                break;
            }

            case Dir.DOWN:
            {
                roomRect = new Rect(pos.x - width/2 + 1, pos.y - height, width, height);
                break;
            }
            default: // Can literally never happen, just to appease C#
                roomRect = Rect.zero;
                break;
        }
        return roomRect;
    }

    public bool RectContains(Rect a, Rect b)
    {
        return (a.Contains(new Vector2(b.xMin, b.yMin)) && a.Contains(new Vector2(b.xMax, b.yMax)));
    }

    public TileBase[,] GenBigRoom(TileBase[,] roomTiles, Vector2Int pos, Dir dir) // Pass in room params as obj?
    {
        // ROOM GEN
        // XXX: Check if collides with corridors?
        int subRoomWidth = random.Next(roomSize.x, roomSize.y);
        int subRoomHeight = random.Next(roomSize.x, roomSize.y);
        Rect subRoomRect = findRoomRect(pos, subRoomWidth, subRoomHeight, dir);

        // Room overlaps with existing room
        if (checkRoomCollision(subRoomRect))
            return new TileBase[0,0];

        Rect bigRoomRect = new Rect(0, 0, maxSize.x, maxSize.y);
        // Room is out of bounds
        if (!(RectContains(bigRoomRect, subRoomRect)))
        {
            return new TileBase[0, 0];
        }

        // Add new room
        rooms.Add(subRoomRect);
        TileBase[,] subRoomTiles = BuildRoom(subRoomWidth, subRoomHeight);
        ArrayUtils.Merge2DArrays(roomTiles, subRoomTiles,
            new Vector2Int((int)subRoomRect.xMin, (int)subRoomRect.yMin));



        // CORRIDOR GEN

        // Select new dir (can't be opposite of previous)
        Dir[] dirs;
        switch (dir)
        {
            case Dir.LEFT: dirs = new Dir[3]{Dir.LEFT, Dir.UP, Dir.DOWN}; break;
            case Dir.RIGHT: dirs = new Dir[3]{Dir.UP, Dir.DOWN, Dir.RIGHT}; break;
            case Dir.UP: dirs = new Dir[3]{Dir.LEFT, Dir.RIGHT, Dir.UP}; break;
            case Dir.DOWN: dirs = new Dir[3]{Dir.LEFT, Dir.RIGHT, Dir.DOWN}; break;
            default: dirs = new Dir[3]; break;
        }
        Stack<Dir> shuffledDirs = new Stack<Dir>(dirs.OrderBy(x => random.Next()).ToArray());

        // Try dirs until something works. If nothing works, ¯\_(ツ)_/¯
        TileBase[,] recursedTiles = new TileBase[0, 0];
        while (shuffledDirs.Count > 0)
        { 
            Dir newDir = shuffledDirs.Pop();

            // Find corridorStart (center of exit wall)
            Vector2Int corridorStart;
            switch (newDir)
            {
                case Dir.LEFT: corridorStart =
                    new Vector2Int((int)subRoomRect.xMin + 1, (int)(subRoomRect.yMin + subRoomHeight/2) - 1);
                    break;

                case Dir.RIGHT: corridorStart =
                    new Vector2Int((int)subRoomRect.xMax - 1, (int)(subRoomRect.yMin + subRoomHeight/2) - 1);
                    break;

                case Dir.UP: corridorStart =
                    new Vector2Int((int)(subRoomRect.xMin + subRoomWidth/2) - 1, (int)(subRoomRect.yMax - 1));
                    break;

                case Dir.DOWN: corridorStart =
                    new Vector2Int((int)(subRoomRect.xMin + subRoomWidth/2) - 1, (int)(subRoomRect.yMin + 1));
                    break;

                default: corridorStart = Vector2Int.zero; break;
            }

            // Find corridorEnd
            int corridorLength = random.Next(corridorSize.x, corridorSize.y);
            Vector2Int corridorEnd = corridorStart + directions[newDir] * corridorLength;

            // Recurse (attempt to place room at corridor end)
            recursedTiles = GenBigRoom((TileBase[,])roomTiles.Clone(), corridorEnd - directions[newDir], newDir);

            // Recursion was unable to place room: try another direction
            if (recursedTiles.GetLength(0) == 0) continue;

            // Recursion succeeded, stop trying directions
            // Merge in recursed rooms & corridors
            ArrayUtils.Merge2DArrays(roomTiles, recursedTiles, Vector2Int.zero);

            // Merge in corridor
            TileBase[,] corridorTiles = BuildCorridor(corridorStart, corridorEnd, newDir);

            switch (newDir)
            {
                case Dir.LEFT: case Dir.DOWN:
                    ArrayUtils.Merge2DArrays(roomTiles, corridorTiles, corridorEnd, true); break;
                case Dir.RIGHT: case Dir.UP:
                    ArrayUtils.Merge2DArrays(roomTiles, corridorTiles, corridorStart, true); break;
            }

            break;
        } 

        return roomTiles;
    }


    public TileBase[,] BuildCorridor(Vector2Int corridorStart, Vector2Int corridorEnd, Dir dir)
    {
        Dictionary<string, TileBase> tileDict = TileLoader.LoadCorridor("Color", 1);
        TileBase[,] corridor;
        bool hor = false;
        switch (dir)
        {
            case Dir.LEFT:
            case Dir.RIGHT:
                corridor = new TileBase[Math.Abs(corridorEnd.x - corridorStart.x), 3];
                hor = true;
                break;

            case Dir.UP:
            case Dir.DOWN:
                corridor = new TileBase[3, Math.Abs(corridorEnd.y - corridorStart.y)];
                hor = false;
                break;
            default:
               corridor = new TileBase[0, 0];
               break;
        }

        int width = corridor.GetLength(0);
        int height = corridor.GetLength(1);
        Debug.Log(width+ ", " + height);

        if (hor)
            for (int x = 0; x < width; x++)
                for (int y = 0; y < 3; y++)
                    if (x == 0)
                    {
                        if (y == 0) corridor[x,y] = tileDict["horBotLeft"];
                        else if (y == 2) corridor[x,y] = tileDict["horTopLeft"];
                    }
                    else if (x == width - 1)
                    {
                        if (y == 0) corridor[x,y] = tileDict["horBotRight"];
                        else if (y == 2) corridor[x,y] = tileDict["horTopRight"];
                    }
                    else
                    {
                        if (y == 0) corridor[x,y] = tileDict["top"];
                        else if (y == 2) corridor[x,y] = tileDict["bot"];
                    }
        else
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < height; y++)
                    if (y == 0)
                    {
                        if (x == 0) corridor[x,y] = tileDict["horBotRight"];
                        else if (x == 2) corridor[x,y] = tileDict["horBotLeft"];
                    }
                    else if (y == height - 1)
                    {
                        if (x == 0) corridor[x,y] = tileDict["horTopRight"];
                        else if (x == 2) corridor[x,y] = tileDict["horTopLeft"];
                    }
                    else
                    {
                        if (x == 0) corridor[x,y] = tileDict["right"];
                        else if (x == 2) corridor[x,y] = tileDict["left"];
                    }

                
        return corridor;
    }

    public TileBase[,] BuildRoom(int width, int height)
    {
        TileBase[,] room = new TileBase[width, height];
        Dictionary<string, TileBase> tileDict = TileLoader.LoadRoom("Color", 1);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if ((x == 0) && (y == 0))
                {
                    room[x, y] = tileDict["botLeft"];
                }
                else if ((x == (width - 1)) && (y == 0))
                {
                    room[x, y] = tileDict["botRight"];
                }
                else if ((x == 0) && (y == height - 1))
                {
                    room[x, y] = tileDict["topLeft"];
                }
                else if ((x == width - 1) && (y == height - 1))
                {
                    room[x, y] = tileDict["topRight"];
                }
                else if (y == 0)
                {
                    room[x, y] = tileDict["bot"];
                }
                else if (y == height - 1)
                {
                    room[x, y] = tileDict["top"];
                }
                else if (x == 0)
                {
                    room[x, y] = tileDict["left"];
                }
                else if (x == width - 1)
                {
                    room[x, y] = tileDict["right"];
                }
                // else
                // {
                //     room[x, y] = tileDict["floor"];
                // }
            }
        return room;
    }
}
