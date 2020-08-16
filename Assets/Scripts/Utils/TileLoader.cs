using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TileLoader
{
    public static TileBase LoadTile(string prefix, string name)
    {
        return Resources.Load<TileBase>(prefix + name);
    }

    public static Dictionary<string, TileBase> LoadRoom(string tileset, int room)
    {
        Dictionary<string, TileBase> tileDict = new Dictionary<string, TileBase>();
        string prefix = "Tiles/" + tileset + "/Room" + room + "/";

        tileDict.Add("floor", LoadTile(prefix, "floor"));

        tileDict.Add("topRight", LoadTile(prefix, "topRight"));
        tileDict.Add("topLeft", LoadTile(prefix, "topLeft"));
        tileDict.Add("botRight", LoadTile(prefix, "botRight"));
        tileDict.Add("botLeft", LoadTile(prefix, "botLeft"));

        tileDict.Add("left", LoadTile(prefix, "left"));
        tileDict.Add("right", LoadTile(prefix, "right"));
        tileDict.Add("bot", LoadTile(prefix, "bot"));
        tileDict.Add("top", LoadTile(prefix, "top"));

        return tileDict;
    }
    public static Dictionary<string, TileBase> LoadCorridor(string tileset, int room)
    {
        Dictionary<string, TileBase> tileDict = new Dictionary<string, TileBase>();
        string prefix = "Tiles/" + tileset + "/Corridor" + room + "/";

        tileDict.Add("floor", LoadTile(prefix, "floor"));

        tileDict.Add("horTopRight", LoadTile(prefix, "horTopRight"));
        tileDict.Add("horTopLeft", LoadTile(prefix, "horTopLeft"));
        tileDict.Add("horBotRight", LoadTile(prefix, "horBotRight"));
        tileDict.Add("horBotLeft", LoadTile(prefix, "horBotLeft"));

        tileDict.Add("vertTopRight", LoadTile(prefix, "vertTopRight"));
        tileDict.Add("vertTopLeft", LoadTile(prefix, "vertTopLeft"));
        tileDict.Add("vertBotRight", LoadTile(prefix, "vertBotRight"));
        tileDict.Add("vertBotLeft", LoadTile(prefix, "vertBotLeft"));

        tileDict.Add("left", LoadTile(prefix, "left"));
        tileDict.Add("right", LoadTile(prefix, "right"));
        tileDict.Add("bot", LoadTile(prefix, "bot"));
        tileDict.Add("top", LoadTile(prefix, "top"));

        return tileDict;
    }
}