using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int baseAttackCount = 5;
    public int realAttackCount = 0;
    public bool isSelectingAttack = true;
    public char[] attackList;

    private GameObject[] secondPlayer;
    private Animator player2Animator;
    private Animator anim;
    private PlayerHealth playerHealth;

    private int playerId;

    // Use this for initialization
    void Start()
    {
        if (name == "Player1")
        {
            playerId = 0;
        }
        else if (name == "Player2")
        {
            playerId = 1;
        }
        else
        {
            playerId = 2;
        }

        playerHealth = GetComponent<PlayerHealth>();
        attackList = new char[baseAttackCount];
        anim = GetComponent<Animator>();
    }

    bool canDoThatAttack(char zone)
    {
        switch (zone)
        {
            case 'a':
                if (playerHealth.CurrentArmsHealth > 0)
                {
                    return true;
                }

                return false;
            case 'l':
                if (playerHealth.CurrentLegsHealth > 0)
                {
                    return true;
                }

                return false;
            case 'h':
                if (playerHealth.CurrentHeadHealth > 0)
                {
                    return true;
                }

                return false;
            case 'c':
                return true;
        }

        return false;
    }

    void AttackSelection()
    {
        if (Input.GetAxis(InputManager.Input(InputKey.lt, playerId)) > 0 ||
            Input.GetAxis(InputManager.Input(InputKey.rt, playerId)) > 0)
        {
            if (Input.GetButtonUp(InputManager.Input(InputKey.y, playerId)) && playerHealth.CurrentHeadHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.GuardHead;
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.x, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.GuardPunch;
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.a, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.GuardKick;
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.b, playerId)))
            {
                attackList[realAttackCount] = FightCharTab.GuardLaser;
                realAttackCount++;
            }
        }
        else
        {
            if (Input.GetButtonUp(InputManager.Input(InputKey.x, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.LittlePunch;
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.rb, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.BigPunch;
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.a, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.LittleKick;
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.lb, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.BigKick;
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.y, playerId)) && playerHealth.CurrentHeadHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.Head;
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.b, playerId)))
            {
                attackList[realAttackCount] = FightCharTab.Laser;
                realAttackCount++;
            }
        }

        if (realAttackCount == baseAttackCount)
        {
            isSelectingAttack = false;
        }
    }

    void AttackAuto()
    {
        //float actionRandom.Range(0f, 10f);
    }

    void ComboAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle")) /*&&
            player2Animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))*/
        {
            realAttackCount--;
            if (realAttackCount == -1)
            {
                realAttackCount = 0;
                isSelectingAttack = true;
                attackList = new char[baseAttackCount];
            }
            else
            {
                char zone = ' ';
                int temp = 0;
                FightManager.getInfoAttack(ref zone, ref temp, attackList[realAttackCount]);
                if (playerId == 0)
                {
                    if (canDoThatAttack(zone))
                    {
                        FightManager.setAttackPlayer1(attackList[realAttackCount]);
                    }
                    else
                    {
                        attackList[realAttackCount] = 'n';
                        FightManager.setAttackPlayer1(attackList[realAttackCount]);
                    }
                }
                else
                {
                    if (canDoThatAttack(zone))
                    {
                        FightManager.setAttackPlayer2(attackList[realAttackCount]);
                    }
                    else
                    {
                        attackList[realAttackCount] = 'n';
                        FightManager.setAttackPlayer2(attackList[realAttackCount]);
                    }
                }

                switch (attackList[realAttackCount])
                {
                    case FightCharTab.LittlePunch:
                        anim.SetTrigger("LittlePunch");
                        break;
                    case FightCharTab.BigPunch:
                        anim.SetTrigger("BigPunch");
                        break;
                    case FightCharTab.LittleKick:
                        anim.SetTrigger("LittleKick");
                        break;
                    case FightCharTab.BigKick:
                        anim.SetTrigger("BigKick");
                        break;
                    case FightCharTab.Head:
                        anim.SetTrigger("Head");
                        break;
                    case FightCharTab.Laser:
                        anim.SetTrigger("Laser");
                        break;
                    case FightCharTab.GuardPunch:
                        anim.SetTrigger("GuardPunch");
                        break;
                    case FightCharTab.GuardKick:
                        anim.SetTrigger("GuardKick");
                        break;
                    case FightCharTab.GuardHead:
                        anim.SetTrigger("GuardHead");
                        break;
                    case FightCharTab.GuardLaser:
                        anim.SetTrigger("GuardLaser");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectingAttack)
        {
            if (playerId < 2)
            {
                AttackSelection();
            }
            else
            {
                AttackAuto();
            }
        }
        else
        {
            ComboAttack();
        }
    }
}

/*if ((SystemInfo.operatingSystem.contains("Windows") &&
             (Input.GetAxis(InputManager.Input(InputKey.lt, playerId)) > 0 ||
              Input.GetAxis(InputManager.Input(InputKey.rt, playerId)) > 0)) ||
            (SystemInfo.operatingSystem.contains("Linux") && 
                (Input.GetButton(InputManager.Input(InputKey.lt, playerId)) ||
                Input.GetButton(InputManager.Input(InputKey.rt, playerId)))))*/