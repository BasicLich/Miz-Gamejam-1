using UnityEngine;

public static class DungeonPresets
{
    public static DungeonParams medium = new DungeonParams(
        new Vector2Int(9, 14),
        new Vector2Int(4, 7),
        new Vector2Int(64, 64),
        new Vector2Int(7, 10),
        10,
        3
    );

    public static DungeonParams difficultyToDungeonParams(float difficulty)
    {
        // if (difficulty < 1.0) return small;
        if (difficulty < 2.0) return medium;
        // if (difficulty < 3.0) return large;
        return medium;
    }

}