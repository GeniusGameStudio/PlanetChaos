using System.Collections;
using System.Collections.Generic;
using TurnBaseUtil;
using UnityEngine;

public static class Global
{
    public static Team teamA = new Team("A");
    public static Team teamB = new Team("B");

    public static Color teamA_Color = new Color(118f / 255f, 187f / 255f, 255f / 255f);
    public static Color teamB_Color = new Color(255f / 255f, 129f / 255f, 117f / 255f);

    public enum Weapon
    {
        BAZOOKA = 0,
    }
}
