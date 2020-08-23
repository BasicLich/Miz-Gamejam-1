using UnityEngine;

public static class DungeonPresets
{
    public static DungeonParams small = new DungeonParams(
        new Vector2Int(10, 14),
        new Vector2Int(2, 6),
        new Vector2Int(96, 96),
        new Vector2Int(4, 6),
        20,
        3
    );
    public static DungeonParams medium = new DungeonParams(
        new Vector2Int(12, 16),
        new Vector2Int(2, 8),
        new Vector2Int(96, 96),
        new Vector2Int(5, 7),
        20,
        4
    );

    public static DungeonParams large = new DungeonParams(
        new Vector2Int(16, 20),
        new Vector2Int(2, 12),
        new Vector2Int(128, 128),
        new Vector2Int(6, 8),
        20,
        5
    );

    public static DungeonParams difficultyToDungeonParams(float difficulty)
    {
        if (difficulty < 4.0) return small;
        if (difficulty < 8.0) return medium;
        return large;
    }

}