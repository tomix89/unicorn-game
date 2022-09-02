using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class GameInfoHolder
{

    public static bool wasLastLevelSucess { get; private set; } = false;
    public static string lastLevelName { get; private set; }

    public static int lastLevelId { get; private set; } = -1;


    public static void initLevel(string levelName) {
        lastLevelName = levelName;
        lastLevelId = int.Parse(levelName.Replace("Level_", "").Replace("_Scene", ""));
    }

    public static void setLevelResult(string levelName, bool wasSucess) {
        wasLastLevelSucess = wasSucess;
        lastLevelName = levelName;
        lastLevelId = int.Parse(levelName.Replace("Level_", "").Replace("_Scene", ""));
    }


}