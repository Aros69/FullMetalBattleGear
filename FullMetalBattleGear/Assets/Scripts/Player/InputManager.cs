using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputKey
{
    a,
    b,
    x,
    y,
    lb,
    rb,
    lt,
    rt,
    select,
    start
}

public class InputManager : MonoBehaviour
{
    
    public static string Input(InputKey input, int playerId)
    {
        string output = (playerId == 0) ? "Player1" : "Player2";

        switch (input)
        {
            case InputKey.a:
                output += "A";
                break;
            case InputKey.b:
                output += "B";
                break;
            case InputKey.x:
                output += "X";
                break;
            case InputKey.y:
                output += "Y";
                break;
            case InputKey.lb:
                output += "LB";
                break;
            case InputKey.rb:
                output += "RB";
                break;
            case InputKey.lt:
                output += "LT";
                break;
            case InputKey.rt:
                output += "RT";
                break;
            case InputKey.select:
                output += "Back";
                break;
            case InputKey.start:
                output += "Start";
                break;
        }

        return output;
    }
}