using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using UnityEditor;

public class GenComponent : MonoBehaviour
{
    // public int townWidth, townHeight, houseWidth, houseHeight, rand, margin, randPoints, numRoads;
    public Tilemap room;
    public RoomDigger digger;
    public Vector2Int roomSize, corridorLength, maxSize, roomCount;
    public int tries, corridorWidth;
    // DungeonParams params;
    public void GenAndRender() {
        digger = new RoomDigger();
        DungeonParams dungeonParams = new DungeonParams(roomSize, corridorLength, maxSize, roomCount, tries, corridorWidth);
        TileBase[,] hey = digger.MakeDungeonFloor(dungeonParams);
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

    void OnDrawGizmosSelected()
    {
        // Orange
        Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
        if (digger != null)
        { 
            foreach (Rect rect in digger.rooms)
            {
                DrawRect(rect);
            }
            foreach (Rect rect in digger.corridors)
            {
                DrawRect(rect);
            }
        }
    }
     
    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }



}

//[CustomEditor(typeof(GenComponent))]
//public class ScriptEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        GenComponent genComp = (GenComponent)target;
//        if (GUILayout.Button("Generate Room"))
//        {
//            genComp.GenAndRender();
//        }
//    }
//}