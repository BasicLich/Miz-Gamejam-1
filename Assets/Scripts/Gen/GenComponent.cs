using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class GenComponent : MonoBehaviour
{
    // public int townWidth, townHeight, houseWidth, houseHeight, rand, margin, randPoints, numRoads;
    public Tilemap room;
    public RoomDigger digger;
    public Vector2Int roomSize, corridorLength, maxSize;
    public void GenAndRender() {
        digger = new RoomDigger();
        TileBase[,] hey = digger.TryMakeRoom(roomSize, corridorLength, maxSize);
        RenderTiles(hey);
    }

    public void RenderTiles(TileBase[,] tiles)
    {
        room.ClearAllTiles();

        int width = tiles.GetUpperBound(0);
        int height = tiles.GetUpperBound(1);

        for (int x = 0; x <= width; x++)
        {
            for (int y = 0; y <= height; y++)
            {
                room.SetTile(new Vector3Int(x, y, 0), tiles[x, y]);
            }
        }
    }
}

[CustomEditor(typeof(GenComponent))]
public class ScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GenComponent genComp = (GenComponent)target;
        if (GUILayout.Button("Generate Room"))
        {
            genComp.GenAndRender();
        }
    }
}