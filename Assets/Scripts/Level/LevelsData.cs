using System.Collections.Generic;

public class LevelsData
{
    private static IReadOnlyList<LevelProperties> readonlyList = new List<LevelProperties>()
    {
        new LevelProperties()
        {
            From = 0,
            To = 10,
            Opperation = LevelProperties.OpperationEnum.Plus
        },
        new LevelProperties()
        {
            From = 0,
            To = 20,
            Opperation = LevelProperties.OpperationEnum.Plus
        },
        new LevelProperties()
        {
            From = 0,
            To = 60,
            Opperation = LevelProperties.OpperationEnum.Plus
        },
        new LevelProperties()
        {
            From = 0,
            To = 100,
            Opperation = LevelProperties.OpperationEnum.Plus
        },
        new LevelProperties()
        {
            From = 0,
            To = 10,
            Opperation = LevelProperties.OpperationEnum.Minus
        },
        new LevelProperties()
        {
            From = 0,
            To = 10,
            Opperation = LevelProperties.OpperationEnum.Minus
        },
        new LevelProperties()
        {
            From = 0,
            To = 10,
            Opperation = LevelProperties.OpperationEnum.Minus
        },
        new LevelProperties()
        {
            From = 0,
            To = 10,
            Opperation = LevelProperties.OpperationEnum.Minus
        },
    };

    public static LevelProperties SetGameLevel(int numberLevel)
    {
        return readonlyList[numberLevel - 1];
    }
}
