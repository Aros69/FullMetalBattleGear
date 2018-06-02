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

    // Use this for initialization
    void Start()
    {
        secondPlayer = GameObject.FindGameObjectsWithTag("Player");

        player2Animator = secondPlayer.GetComponent<Animator>();
        attackList = new char[baseAttackCount];
        anim = GetComponent<Animator>();
    }

    void ClearState()
    {
        anim.SetBool("Attack", false);
        anim.SetBool("Hit", false);
    }

    void AttackSelection()
    {
        // Regarder si l'attack demander est dispo avant d'accepter l'input
        if (Input.GetButtonUp("Fire1"))
        {
            attackList[realAttackCount] = FightCharTab.LittlePunch;
            realAttackCount++;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }
        /* else if(Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }else if(Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }else if(Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }else if(Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }else if(Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }else if(Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }else if(Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }else if(Input.GetButtonUp("Fire2"))
        {
            attackList[realAttackCount] = FightCharTab.BigPunch;
            realAttackCount++;
        }*/

        if (realAttackCount == baseAttackCount)
        {
            isSelectingAttack = false;
        }
    }

    void ComboAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle") &&
            player2Animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
        {
            realAttackCount--;
            if (realAttackCount == -1)
            {
                realAttackCount = 0;
                isSelectingAttack = true;
                attackList = new char[baseAttackCount];
                ClearState();
            }
            else
            {
                FightManager.setAttackPlayer1(attackList[realAttackCount]);
                ClearState();
                switch (attackList[realAttackCount])
                {
                    case FightCharTab.LittlePunch:
                        anim.SetBool("Attack", true);
                        break;
                    case FightCharTab.BigPunch:
                        anim.SetBool("Hit", true);
                        break;
                    case FightCharTab.LittleKick:
                        break;
                    case FightCharTab.BigKick:
                        break;
                    case FightCharTab.Head:
                        break;
                    case FightCharTab.Laser:
                        break;
                    case FightCharTab.GuardPunch:
                        break;
                    case FightCharTab.GuardKick:
                        break;
                    case FightCharTab.GuardHead:
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
            AttackSelection();
        }
        else
        {
            ComboAttack();
        }
    }
}