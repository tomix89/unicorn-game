using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameInfoHolder
{

    public static bool wasLastLevelSucess { get; private set; } = false;
    public static string lastLevelName { get; private set; }


    public static void setLevelResult(string levelName, bool wasSucess) {
        wasLastLevelSucess = wasSucess;
        lastLevelName = levelName;
    }


}