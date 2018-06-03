using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int StartingGlobalHealth = 150;
    public int CurrentGlobalHealth;
    public int StartingHeadHealth = 40;
    public int CurrentHeadHealth;
    public int StartingLegsHealth = 50;
    public int CurrentLegsHealth;
    public int StartingArmsHealth = 30;
    public int CurrentArmsHealth;

    // Use this for initialization
    void Start()
    {
        CurrentGlobalHealth = StartingGlobalHealth;
        CurrentHeadHealth = StartingHeadHealth;
        CurrentLegsHealth = StartingLegsHealth;
        CurrentArmsHealth = StartingArmsHealth;
    }

    public void getHit(char zoneHit, int DamageTaken)
    {
        switch (zoneHit)
        {
            case 'a': // arms hit
                CurrentArmsHealth -= DamageTaken;
                CurrentGlobalHealth -= DamageTaken;
                break;
            case 'l': // legs hit
                CurrentLegsHealth -= DamageTaken;
                CurrentGlobalHealth -= DamageTaken;
                break;
            case 'h': // head hit
                CurrentHeadHealth -= DamageTaken;
                CurrentGlobalHealth -= DamageTaken;
                break;
            case 'c': // chest hit
                CurrentHeadHealth -= DamageTaken;
                CurrentArmsHealth -= DamageTaken;
                CurrentLegsHealth -= DamageTaken;
                CurrentGlobalHealth -= DamageTaken * 3;
                break;
            default:
                // Nothing happen
                break;
        }
    }
}