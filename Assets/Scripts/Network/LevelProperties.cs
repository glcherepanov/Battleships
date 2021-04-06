using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties
{
    public enum OpperationEnum
    { 
        Plus,
        Minus
    };

    public int From { get; set; }
    public int To { get; set; }
    public OpperationEnum Opperation { get; set; }
}
