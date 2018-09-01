using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{

    public Guard guardLegs;
    public Guard guardArms;
    public Guard guardChest;

    public List<Guard> guards = new List<Guard>();

    PlayerHealth playerHealth;


    void Start()
    {
        Guard[] tempGuard = gameObject.GetComponentsInChildren<Guard>();
        Debug.Log(guardArms);
        for(int i=0;i<tempGuard.Length;i++){
            if(tempGuard[0].name=="GuardArms"){
                guardArms=tempGuard[0];
            }
        }
        for(int i=0;i<tempGuard.Length;i++){
            if(tempGuard[i].name=="GuardLegs"){
                guardLegs=tempGuard[i];
            }
        }
        for(int i=0;i<tempGuard.Length;i++){
            if(tempGuard[i].name=="GuardChest"){
                guardChest=tempGuard[i];
            }
        }
        Debug.Log(guardArms);

        /*guards.Add(guardLegs);
        guards.Add(guardArms);
        guards.Add(guardChest);*/

        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        if (temp[0].name == "Player1")
        {
            playerHealth = temp[0].GetComponent<PlayerHealth>();
        }
        else
        {
            playerHealth = temp[1].GetComponent<PlayerHealth>();
        }

    }

    public void setGuardPunch(){
        guardArms.gameObject.SetActive(true);
        guardArms.Play();
        guardArms.Invoke("Stop", 0.5f);
    }
    public void setGuardKick(){
        guardLegs.gameObject.SetActive(true);
        guardLegs.Play();
        guardLegs.Invoke("Stop", 0.5f);
    }
    public void setGuardLaser(){
        guardChest.gameObject.SetActive(true);
        guardChest.Play();
        guardChest.Invoke("Stop", 0.5f);
    }

    private void Update()
    {   
        /*if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnGuard(0, FightCharTab.GuardPunch);
        }*/

        /*float a = playerHealth.CurrentGlobalHealth;
        float b = playerHealth.StartingGlobalHealth;

        if (a / b < 0.5f)
        {
            spark.SetActive(true);
        }*/
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
