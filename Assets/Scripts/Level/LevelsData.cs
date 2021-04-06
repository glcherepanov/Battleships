using System.Collections.Generic;

public class LevelsData
{
    private IReadOnlyList<LevelDataObject> readonlyList = new List<LevelDataObject>()
    {
        new LevelDataObject()
        {
            name = "+ to 10",
            resultFrom = 0,
            resultTo = 10,
            opperation = "+"
        },
        new LevelDataObject()
        {
            name = "+ to 20",
            resultFrom = 0,
            resultTo = 20,
            opperation = "+"
        },
        new LevelDataObject()
        {
            name = "+ to 60",
            resultFrom = 0,
            resultTo = 60,
            opperation = "+"
        },
        new LevelDataObject()
        {
            name = "+ to 100",
            resultFrom = 0,
            resultTo = 100,
            opperation = "+"
        },
        new LevelDataObject()
        {
            name = "- to 10",
            resultFrom = 0,
            resultTo = 10,
            opperation = "-"
        },
        new LevelDataObject()
        {
            name = "- to 30",
            resultFrom = 0,
            resultTo = 10,
            opperation = "-"
        },
        new LevelDataObject()
        {
            name = "- to 60",
            resultFrom = 0,
            resultTo = 10,
            opperation = "-"
        },
        new LevelDataObject()
        {
            name = "- to 100",
            resultFrom = 0,
            resultTo = 10,
            opperation = "-"
        },
    };

    public void SetGameLevel()
    {
        
    }
}
