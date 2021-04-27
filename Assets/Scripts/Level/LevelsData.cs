using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelsData
{
    private static List<LevelProperties> readonlyList = new List<LevelProperties>()
    {
        new LevelProperties()
        {
            From = 0,
            To = 10,
            Opperation = LevelProperties.OpperationEnum.Plus
        },
        new LevelProperties()
        {
            From = 10,
            To = 20,
            Opperation = LevelProperties.OpperationEnum.Plus
        },
        new LevelProperties()
        {
            From = 20,
            To = 60,
            Opperation = LevelProperties.OpperationEnum.Plus
        },
        new LevelProperties()
        {
            From = 60,
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
            From = 10,
            To = 20,
            Opperation = LevelProperties.OpperationEnum.Minus
        },
        new LevelProperties()
        {
            From = 20,
            To = 60,
            Opperation = LevelProperties.OpperationEnum.Minus
        },
        new LevelProperties()
        {
            From = 60,
            To = 100,
            Opperation = LevelProperties.OpperationEnum.Minus
        },
    };

    public static string SetGameLevel(int numberLevel)
    {
        return JsonUtility.ToJson(readonlyList[numberLevel - 1]);
    }
}
