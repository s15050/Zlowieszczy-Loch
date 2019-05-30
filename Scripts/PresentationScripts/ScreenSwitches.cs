using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSwitches {

    //Clamps: Najpierw MIN, potem MAX
    //===============================
    //Więzienie
    private static Vector2[] prison = { new Vector2(-2.008628f, -2.008628f), //Clamps
                                        new Vector2(-1.97f, -0.88f), //Pozycja kamery
                                        new Vector2(-0.03f, -2.96f), //Pozycja startowa
                                        new Vector2(-12.18f, -2.96f) }; //Wejście od lewej (Left)

    //Korytarz
    private static Vector2[] corridor = { new Vector2(-49.24f, -40.09f), //Clamps
                                          new Vector2(-45.02f, 0f), //Pozycja kamery
                                          new Vector2(-29.37f, -2.29f), //Wejście z więzienia (Prison)
                                          new Vector2(-59.82f, -2.29f), //Wejście z rytuału (Ritual)
                                          new Vector2(-33.17f, -2.29f), //Wejście ze schowka (Storage)
                                          new Vector2(-51.19f, -2.29f)}; //Wejście ze stróżówki (Watch)

    //Sala Rytuału
    private static Vector2[] ritual = { new Vector2(-100.71f, -91.94f), //Clamps
                                        new Vector2(-91.94f, -0.3f), //Pozycja kamery
                                        new Vector2(-81.33f, -2.63f)}; //Wejście z korytarza (Corridor)

    //Stróżówka
    private static Vector2[] watch = { new Vector2(-60.49f, -60.49f), //Clamps
                                       new Vector2(-60.49f, 15.23f), //Pozycja kamery
                                       new Vector2(-71.26f, 13.21f)}; //Wejście z korytarza (Corridor)

    //Schowek
    private static Vector2[] storage = { new Vector2(-24.37f, -24.37f), //Clamps
                                         new Vector2(-24.37f, 15.42f), //Pozycja kamery
                                         new Vector2(-34.82f, 13.25f)}; //Wejście z korytarza (Corridor)

    public static Vector2 getCamera(string needed, string room) //Zwraca "clamps" lub "position" (needed) kamery w pokoju (room)
    {
        int a = 0;
        if (string.Equals("clamps", needed))
            a = 0;
        else a = 1;

        switch (room)
        {
            case "prison": return prison[a];
            case "corridor": return corridor[a];
            case "ritual": return ritual[a];
            case "watch": return watch[a];
            case "storage": return storage[a];
            default: return prison[a];
        }
    }

    public static Vector2 getPlayerStarter() //zwraca pozycję początkową gracza
    {
        return prison[2];
    }

    public static Vector2 getPlayerIntoCorridor(string from) //Zwraca pozycję gracza w korytarzu, wychodząc z pokoju (from)
    {
        return getPlayerEntry(from, "corridor");
    }

    public static Vector2 getPlayerEntry(string from, string to) //Zwraca pozycję gracza idącego z pokoju (from) do pokoju (to)
    {
        switch (to)
        {
            case "prison": return prison[3];
            case "ritual": return ritual[2];
            case "watch": return watch[2];
            case "storage": return storage[2];
            case "corridor":
                {
                    switch(from)
                    {
                        case "prison": return corridor[2];
                        case "ritual": return corridor[3];
                        case "storage": return corridor[4];
                        case "watch": return corridor[5];
                        default: return prison[2];
                    }
                }
            default: return prison[2];
        }
    }

    public static Vector2 getPlayerRelive(string room)
    {
        switch (room)
        {
            case "corridor": return corridor[2];
            case "watch": return corridor[5];
            default: return prison[2];
        }
    }
}
