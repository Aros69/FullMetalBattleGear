﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{

    public Guard guardLegs1;
    public Guard guardArms1;
    public Guard guardLazer1;

    public Guard guardLegs2;
    public Guard guardArms2;
    public Guard guardLazer2;

    public List<Guard> guards = new List<Guard>();

    void Start()
    {
        guards.Add(guardLegs1);
        guards.Add(guardArms1);
        guards.Add(guardLazer1);

        guards.Add(guardLegs2);
        guards.Add(guardArms2);
        guards.Add(guardLazer2);

        guardLegs1.gameObject.SetActive(false);
        guardArms1.gameObject.SetActive(false);
        guardLazer1.gameObject.SetActive(false);

        guardLegs2.gameObject.SetActive(false);
        guardArms2.gameObject.SetActive(false);
        guardLazer2.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnGuard(0, FightCharTab.GuardPunch);
        }
    }

    public void SpawnGuard(int playerId, char attack)
    {
        int a = (playerId == 0) ? 0 : 3;

        switch (attack)
        {
            case FightCharTab.GuardPunch:
                a += 1;
                break;
            case FightCharTab.GuardKick:
                a += 1;
                break;
            case FightCharTab.GuardLaser:
                a += 1;
                break;
            default:
                a = -1;
                break;
        }

        guards[a].gameObject.SetActive(true);
        guards[a].Play();
        guards[a].Invoke("Stop", 0.5f);
    }
}
