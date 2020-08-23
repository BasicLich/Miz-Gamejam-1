using UnityEngine;

public static class DungeonPresets
{
    public static DungeonParams medium = new DungeonParams(
        new Vector2Int(12, 16),
        new Vector2Int(2, 8),
        new Vector2Int(96, 96),
        new Vector2Int(5, 7),
        20,
        4
    );

    public static DungeonParams difficultyToDungeonParams(float difficulty)
    {
        // if (difficulty < 1.0) return small;
        if (difficulty < 2.0) return medium;
        // if (difficulty < 3.0) return large;
        return medium;
    }

}