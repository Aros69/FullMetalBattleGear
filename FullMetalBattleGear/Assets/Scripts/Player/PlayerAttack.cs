using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public int baseAttackCount = 5;
	public int realAttackCount = 0;
	public bool isSelectingAttack = true;
	public char[] attackList;

	Animator anim;
	// Use this for initialization
	void Start () {
		// Get attack count and attack list of the other player
		// Get player state attack ??
		attackList = new char[baseAttackCount];
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isSelectingAttack)
		{
			if (Input.GetButtonUp("Fire1"))
			{
				attackList[realAttackCount] = 'a';
				realAttackCount++;
			}
			else if(Input.GetButtonUp("Fire2"))
			{
				attackList[realAttackCount] = 'b';
				realAttackCount++;
			}

			if (realAttackCount == baseAttackCount)
			{
				isSelectingAttack = false;
			}
		}
		else if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerIdle"))
		{
			realAttackCount--;
			if (realAttackCount==-1)
			{
				isSelectingAttack = true;
				attackList = new char[baseAttackCount];
				anim.SetBool("Attack", false);
				anim.SetBool("Hit", false);
			}
			else
			{
				if (attackList[realAttackCount] == 'a')
				{
					anim.SetBool("Attack", true);
				}
				else if (attackList[realAttackCount] == 'b')
				{
					anim.SetBool("Attack", false);
					anim.SetBool("Hit", true);
				}

				attackList[realAttackCount] = ' ';
			}
		}

	}
}
