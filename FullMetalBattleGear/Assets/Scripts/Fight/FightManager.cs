using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightManager : MonoBehaviour
{
    // The current attack of player 1 done (space if none)
    private char attackPlayer1 = ' ';

    // The current attack of player 2 done (space if none)
    private char attackPlayer2 = ' ';

    // Reference to it self ?!
    public static FightManager main;

    // Reference to the two palyerq
    private GameObject[] players;

    // Reference to player 1 health
    private PlayerHealth Player1Health;

    // Reference to player 2 health
    private PlayerHealth Player2Health;

    // Reference to player 1 animator
    private Animator player1Animator;

    // Reference to player 2 animator
    private Animator player2Animator;

    // Reference to player 1 attack script
    private PlayerAttack player1Attack;

    // Reference to player 2 attack script
    private PlayerAttack player2Attack;

    // Use this for initialization
    void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        if (players[0].name == "Player1")
        {
            Player1Health = players[0].GetComponent<PlayerHealth>();
            Player2Health = players[1].GetComponent<PlayerHealth>();
            player1Animator = players[0].GetComponent<Animator>();
            player2Animator = players[1].GetComponent<Animator>();
            player1Attack = players[0].GetComponent<PlayerAttack>();
            player2Attack = players[1].GetComponent<PlayerAttack>();
        }
        else
        {
            Player1Health = players[1].GetComponent<PlayerHealth>();
            Player2Health = players[0].GetComponent<PlayerHealth>();
            player1Animator = players[1].GetComponent<Animator>();
            player2Animator = players[0].GetComponent<Animator>();
            player1Attack = players[1].GetComponent<PlayerAttack>();
            player2Attack = players[0].GetComponent<PlayerAttack>();
        }

        main = this;
    }

    // Return the longest animation of the two player
    public float DureeAnimation()
    {
        float a = 0;
        RuntimeAnimatorController rac = player1Animator.runtimeAnimatorController;
        foreach (AnimationClip ac in rac.animationClips)
        {
            if (ac.length > a)
                a = ac.length;
        }

        rac = player2Animator.runtimeAnimatorController;
        foreach (AnimationClip ac in rac.animationClips)
        {
            if (ac.length > a)
                a = ac.length;
        }

        return a;
    }

    static bool isAttacking(char attack)
    {
        if (attack == FightCharTab.LittlePunch || attack == FightCharTab.BigPunch ||
            attack == FightCharTab.LittleKick || attack == FightCharTab.BigKick || attack == FightCharTab.Head ||
            attack == FightCharTab.Laser)
        {
            return true;
        }

        return false;
    }

    // Function that manage each damage of each attack and zone
    static public void getInfoAttack(ref char attackZone, ref int damageDealed, char attack)
    {
        switch (attack)
        {
            case FightCharTab.LittlePunch:
                attackZone = 'a';
                damageDealed = 5;
                break;
            case FightCharTab.BigPunch:
                attackZone = 'a';
                damageDealed = 8;
                break;
            case FightCharTab.LittleKick:
                attackZone = 'l';
                damageDealed = 7;
                break;
            case FightCharTab.BigKick:
                attackZone = 'l';
                damageDealed = 11;
                break;
            case FightCharTab.Head:
                attackZone = 'h';
                damageDealed = 12;
                break;
            case FightCharTab.Laser:
                attackZone = 'c';
                damageDealed = 10;
                break;
            case FightCharTab.GuardPunch:
                attackZone = 'a';
                damageDealed = 0;
                break;
            case FightCharTab.GuardKick:
                attackZone = 'l';
                damageDealed = 0;
                break;
            case FightCharTab.GuardHead:
                attackZone = 'h';
                damageDealed = 0;
                break;
            case FightCharTab.GuardLaser:
                attackZone = 'c';
                damageDealed = 0;
                break;
            default:
                attackZone = ' ';
                damageDealed = 0;
                break;
        }
    }

    // Manager of anim trigger
    void setAnimTrigger(Animator playerAnimator, char attackPlayer, char attackEnnemy)
    {
        switch (attackPlayer)
        {
            case FightCharTab.LittlePunch:
                playerAnimator.SetTrigger("LittlePunch");
                break;
            case FightCharTab.BigPunch:
                playerAnimator.SetTrigger("BigPunch");
                break;
            case FightCharTab.LittleKick:
                playerAnimator.SetTrigger("LittleKick");
                break;
            case FightCharTab.BigKick:
                playerAnimator.SetTrigger("BigKick");
                break;
            case FightCharTab.Head:
                playerAnimator.SetTrigger("Head");
                break;
            case FightCharTab.Laser:
                if (attackEnnemy != FightCharTab.GuardLaser)
                {
                    playerAnimator.SetTrigger("Laser");
                }
                else
                {
                    playerAnimator.SetTrigger("FailLaser");
                }

                break;
            case FightCharTab.GuardPunch:
                playerAnimator.SetTrigger("GuardPunch");
                break;
            case FightCharTab.GuardKick:
                playerAnimator.SetTrigger("GuardKick");
                break;
            case FightCharTab.GuardHead:
                playerAnimator.SetTrigger("GuardHead");
                break;
            case FightCharTab.GuardLaser:
                playerAnimator.SetTrigger("GuardLaser");
                break;
            default:
                playerAnimator.SetTrigger("Fail");
                break;
        }
    }

    void Update()
    {
        if (!player1Attack.getIsSelectingAttack() && !player2Attack.getIsSelectingAttack() &&
            !player1Attack.getIsAttacking() && !player2Attack.getIsAttacking())
        {
            if (player1Attack.getRealAttackCount() == player1Attack.baseAttackCount)
            {
                player1Attack.newComboAttack();
                player2Attack.newComboAttack();
            }
            else
            {
                attackPlayer1 = player1Attack.getAttackList(player1Attack.getRealAttackCount());
                attackPlayer2 = player2Attack.getAttackList(player2Attack.getRealAttackCount());
                char attackZoneP1 = ' ';
                char attackZoneP2 = ' ';
                int damageP1 = 0;
                int damageP2 = 0;
                getInfoAttack(ref attackZoneP1, ref damageP1, attackPlayer1);
                getInfoAttack(ref attackZoneP2, ref damageP2, attackPlayer2);
                if (!player1Attack.canDoThatAttack(attackZoneP1))
                {
                    attackPlayer1 = 'n';
                }

                if (!player2Attack.canDoThatAttack(attackZoneP2))
                {
                    attackPlayer2 = 'n';
                }

                setAnimTrigger(player1Animator, attackPlayer1, attackPlayer2);
                setAnimTrigger(player2Animator, attackPlayer2, attackPlayer1);
                player1Attack.setIsAttacking(true);
                player2Attack.setIsAttacking(true);
                if (isAttacking(attackPlayer1))
                {
                    if (isAttacking(attackPlayer2))
                    {
                        Player1Health.getHit(attackZoneP2, damageP2);
                        Player2Health.getHit(attackZoneP1, damageP1);
                    }
                    else if (attackZoneP1 != attackZoneP2)
                    {
                        Player2Health.getHit(attackZoneP1, damageP1);
                    }
                    else
                    {
                        float reflect = (float) damageP1;
                        reflect = Mathf.Floor(reflect / 2);
                        Player1Health.getHit(attackZoneP1, (int) reflect + 1);
                    }
                }
                else if (isAttacking(attackPlayer2) && attackZoneP1 != attackZoneP2)
                {
                    Player2Health.getHit(attackZoneP2, damageP2);
                }
                else
                {
                    float reflect = (float) damageP1;
                    reflect = Mathf.Floor(reflect / 2);
                    Player2Health.getHit(attackZoneP2, (int) reflect + 1);
                }

                attackPlayer1 = ' ';
                attackPlayer2 = ' ';
                player1Attack.setRealAttackCount(player1Attack.getRealAttackCount() + 1);
                player2Attack.setRealAttackCount(player2Attack.getRealAttackCount() + 1);
            }
        }
    }
}