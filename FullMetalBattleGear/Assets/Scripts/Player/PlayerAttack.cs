using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    public int baseAttackCount = 6;
    public int realAttackCount = 0;
    public bool isSelectingAttack = true;
    public char[] attackList;
    public int playerId;

    private GameObject secondPlayer;
    private Animator player2Animator;
    private PlayerAttack player2Attack;
    private Animator anim;
    private PlayerHealth playerHealth;

    private bool isCoroutineAlreadyRunning = false;

    // Use this for initialization
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (name == "Player1")
        {
            playerId = 0;
            if (players[0].name == "Player1")
            {
                secondPlayer = players[1];
            }
            else
            {
                secondPlayer = players[0];
            }
        }
        else
        {
            if (name == "Player2")
            {
                playerId = 1;
            }
            else
            {
                playerId = 2;
            }

            if (players[0].name == "Player1")
            {
                secondPlayer = players[0];
            }
            else
            {
                secondPlayer = players[1];
            }
        }

        player2Attack = secondPlayer.GetComponent<PlayerAttack>();
        player2Animator = secondPlayer.GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
        attackList = new char[baseAttackCount];
        attackList[0] = FightCharTab.Nothing;
        realAttackCount++;
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
            if (Input.GetButtonUp(InputManager.Input(InputKey.x, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.GuardPunch;
                ActionHolder.main.PlayerActionAdd(Card.CardType.guardPunch, playerId);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.a, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.GuardKick;
                ActionHolder.main.PlayerActionAdd(Card.CardType.guardKick, playerId);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.b, playerId)))
            {
                attackList[realAttackCount] = FightCharTab.GuardLaser;
                ActionHolder.main.PlayerActionAdd(Card.CardType.guardLazer, playerId);
                realAttackCount++;
            }
        }
        else
        {
            if (Input.GetButtonUp(InputManager.Input(InputKey.x, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.LittlePunch;
                ActionHolder.main.PlayerActionAdd(Card.CardType.littlePunch, playerId);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.rb, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.BigPunch;
                ActionHolder.main.PlayerActionAdd(Card.CardType.bigPunch, playerId);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.a, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.LittleKick;
                ActionHolder.main.PlayerActionAdd(Card.CardType.littleKick, playerId);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.lb, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.BigKick;
                ActionHolder.main.PlayerActionAdd(Card.CardType.bigKick, playerId);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.b, playerId)))
            {
                attackList[realAttackCount] = FightCharTab.Laser;
                ActionHolder.main.PlayerActionAdd(Card.CardType.lazer, playerId);
                realAttackCount++;
            }
        }

        if (realAttackCount == baseAttackCount)
        {
            realAttackCount = 0;
            isSelectingAttack = false;
        }
    }

    void AttackAuto()
    {
        int numAction = 0;
        char action = FightCharTab.Nothing;
        do
        {
            numAction = (int) Mathf.Floor(UnityEngine.Random.Range(0f, 8f));
            switch (numAction)
            {
                case 0:
                    if (playerHealth.CurrentArmsHealth > 0)
                    {
                        action = FightCharTab.LittlePunch;
                        ActionHolder.main.PlayerActionAdd(Card.CardType.littlePunch, playerId);
                    }

                    break;
                case 1:
                    action = FightCharTab.GuardLaser;
                    ActionHolder.main.PlayerActionAdd(Card.CardType.guardLazer, playerId);
                    break;
                case 2:
                    if (playerHealth.CurrentLegsHealth > 0)
                    {
                        action = FightCharTab.BigKick;
                        ActionHolder.main.PlayerActionAdd(Card.CardType.bigKick, playerId);
                    }

                    break;
                case 3:
                    if (playerHealth.CurrentArmsHealth > 0)
                    {
                        action = FightCharTab.GuardPunch;
                        ActionHolder.main.PlayerActionAdd(Card.CardType.guardPunch, playerId);
                    }

                    break;
                case 4:
                    action = FightCharTab.Laser;
                    ActionHolder.main.PlayerActionAdd(Card.CardType.lazer, playerId);
                    break;
                case 5:
                    if (playerHealth.CurrentLegsHealth > 0)
                    {
                        action = FightCharTab.LittleKick;
                        ActionHolder.main.PlayerActionAdd(Card.CardType.littleKick, playerId);
                    }

                    break;
                case 6:
                    if (playerHealth.CurrentArmsHealth > 0)
                    {
                        action = FightCharTab.BigPunch;
                        ActionHolder.main.PlayerActionAdd(Card.CardType.bigPunch, playerId);
                    }

                    break;
                case 7:
                    if (playerHealth.CurrentLegsHealth > 0)
                    {
                        action = FightCharTab.GuardKick;
                        ActionHolder.main.PlayerActionAdd(Card.CardType.guardKick, playerId);
                    }

                    break;
            }
        } while (action == FightCharTab.Nothing);

        attackList[realAttackCount] = action;
        realAttackCount++;
        if (realAttackCount == baseAttackCount)
        {
            realAttackCount = 0;
            isSelectingAttack = false;
        }
    }

    IEnumerator ComboAttack()
    {
        isCoroutineAlreadyRunning = true;
        if (!player2Attack.isSelectingAttack &&
            anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle") &&
            player2Animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
        {
            if (realAttackCount == 0)
            {
                StartCoroutine(BattleBar.main.FightSequence(BattleBar.main.FightDuration, BattleBar.main.actions));
                yield return new WaitForSeconds(1f);
            }

            if (realAttackCount == baseAttackCount)
            {
                realAttackCount = 0;
                isSelectingAttack = true;
                attackList = new char[baseAttackCount];
                attackList[0] = FightCharTab.Nothing;
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
                        FightManager.setAttackPlayer1('n');
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
                        FightManager.setAttackPlayer2('n');
                    }
                }
            }

            realAttackCount++;
        }
        /*else if (player2Attack.isSelectingAttack &&
                 anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle") &&
                 player2Animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle") &&
                 realAttackCount == baseAttackCount)
        {
            realAttackCount = 0;
            isSelectingAttack = true;
            attackList = new char[baseAttackCount];
            attackList[0] = FightCharTab.Nothing;
        }*/

        isCoroutineAlreadyRunning = false;
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
            if (!isCoroutineAlreadyRunning)
            {
                StartCoroutine(ComboAttack());
            }
        }
    }
}

/*if ((SystemInfo.operatingSystem.contains("Windows") &&
             (Input.GetAxis(InputManager.Input(InputKey.lt, playerId)) > 0 ||
              Input.GetAxis(InputManager.Input(InputKey.rt, playerId)) > 0)) ||
            (SystemInfo.operatingSystem.contains("Linux") && 
                (Input.GetButton(InputManager.Input(InputKey.lt, playerId)) ||
                Input.GetButton(InputManager.Input(InputKey.rt, playerId)))))*/