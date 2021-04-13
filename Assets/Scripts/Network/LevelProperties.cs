using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelProperties
{
    public enum OpperationEnum
    { 
        Plus,
        Minus
    };

    public int From;
    public int To;
    public OpperationEnum Opperation;
}