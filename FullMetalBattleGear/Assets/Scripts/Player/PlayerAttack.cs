using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class PlayerAttack : MonoBehaviour
{
    public ActionBar actionBar;
    
    // Number of attack for the player
    public readonly int baseAttackCount = 5;

    // Number of attack already chosen for the player
    private int realAttackCount = 0;

    // Boolean if the player is currently choosing his future attack 
    private bool isSelectingAttack = true;

    // Boolean if the player is doing an attack
    private bool isAttacking = false;

    // The list of the player attack
    private char[] attackList;

    // The id of our player
    public int playerId;
    
    // A reference to our animator
    private Animator anim;

    // A reference to our health
    private PlayerHealth playerHealth;

    // Use this for initialization
    void Start()
    {
        if (name == "Player1")
        {
            playerId = 0;
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
        }

        playerHealth = GetComponent<PlayerHealth>();
        attackList = new char[baseAttackCount];
        anim = GetComponent<Animator>();
    }

    public int getRealAttackCount()
    {
        return realAttackCount;
    }

    public void setRealAttackCount(int newRealAttackCount)
    {
        realAttackCount = newRealAttackCount;
    }

    public bool getIsSelectingAttack()
    {
        return isSelectingAttack;
    }

    public void setIsSelectingAttack(bool newIsSelectingAttack)
    {
        isSelectingAttack = newIsSelectingAttack;
    }

    public bool getIsAttacking()
    {
        return isAttacking;
    }

    public void setIsAttacking(bool newIsAttancking)
    {
        isAttacking = newIsAttancking;
    }

    public char getAttackList(int indexAttack)
    {
        return attackList[indexAttack];
    }

    public void setAttackList(char newAttack, int indexAttack)
    {
        attackList[indexAttack] = newAttack;
    }

    public void newComboAttack()
    {
        realAttackCount = 0;
        isSelectingAttack = true;
        attackList = new char[baseAttackCount];
        actionBar.ResetCards();
    }

    public bool canDoThatAttack(char zone)
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

    // TODO improve (function qui fait tout suivant l'action choisis) 
    void AttackSelection()
    {
        if (Input.GetAxis(InputManager.Input(InputKey.lt, playerId)) > 0 ||
            Input.GetAxis(InputManager.Input(InputKey.rt, playerId)) > 0)
        {
            if (Input.GetButtonUp(InputManager.Input(InputKey.x, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.GuardPunch;
                actionBar.PlayerActionAdd(Card.CardType.guardPunch);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.a, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.GuardKick;
                actionBar.PlayerActionAdd(Card.CardType.guardKick);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.b, playerId)))
            {
                attackList[realAttackCount] = FightCharTab.GuardLaser;
                actionBar.PlayerActionAdd(Card.CardType.guardLazer);
                realAttackCount++;
            }
        }
        else
        {
            if (Input.GetButtonUp(InputManager.Input(InputKey.x, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.LittlePunch;
                actionBar.PlayerActionAdd(Card.CardType.littlePunch);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.rb, playerId)) && playerHealth.CurrentArmsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.BigPunch;
                actionBar.PlayerActionAdd(Card.CardType.bigPunch);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.a, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.LittleKick;
                actionBar.PlayerActionAdd(Card.CardType.littleKick);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.lb, playerId)) && playerHealth.CurrentLegsHealth > 0)
            {
                attackList[realAttackCount] = FightCharTab.BigKick;
                actionBar.PlayerActionAdd(Card.CardType.bigKick);
                realAttackCount++;
            }
            else if (Input.GetButtonUp(InputManager.Input(InputKey.b, playerId)))
            {
                attackList[realAttackCount] = FightCharTab.Laser;
                actionBar.PlayerActionAdd(Card.CardType.lazer);
                realAttackCount++;
            }
        }

        if (realAttackCount == baseAttackCount)
        {
            realAttackCount = 0;
            isSelectingAttack = false;
        }
    }

    // TODO Improve (function qui fait tout suivant l'action choisis)
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
                        actionBar.PlayerActionAdd(Card.CardType.littlePunch);
                    }

                    break;
                case 1:
                    action = FightCharTab.GuardLaser;
                    actionBar.PlayerActionAdd(Card.CardType.guardLazer);
                    break;
                case 2:
                    if (playerHealth.CurrentLegsHealth > 0)
                    {
                        action = FightCharTab.BigKick;
                        actionBar.PlayerActionAdd(Card.CardType.bigKick);
                    }

                    break;
                case 3:
                    if (playerHealth.CurrentArmsHealth > 0)
                    {
                        action = FightCharTab.GuardPunch;
                        actionBar.PlayerActionAdd(Card.CardType.guardPunch);
                    }

                    break;
                case 4:
                    action = FightCharTab.Laser;
                    actionBar.PlayerActionAdd(Card.CardType.lazer);
                    break;
                case 5:
                    if (playerHealth.CurrentLegsHealth > 0)
                    {
                        action = FightCharTab.LittleKick;
                        actionBar.PlayerActionAdd(Card.CardType.littleKick);
                    }

                    break;
                case 6:
                    if (playerHealth.CurrentArmsHealth > 0)
                    {
                        action = FightCharTab.BigPunch;
                        actionBar.PlayerActionAdd(Card.CardType.bigPunch);
                    }

                    break;
                case 7:
                    if (playerHealth.CurrentLegsHealth > 0)
                    {
                        action = FightCharTab.GuardKick;
                        actionBar.PlayerActionAdd(Card.CardType.guardKick);
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

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
        {
            setIsAttacking(false);
        }

        if (isSelectingAttack)
        {
            if (playerId < 2)
            {
                AttackSelection(); // Player
            }
            else
            {
                AttackAuto(); // Bot/Computer
            }
        }

        /*else
        {
            ComboAttack();
        }*/
    }
}