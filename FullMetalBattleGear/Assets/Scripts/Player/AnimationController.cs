using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationController {

	public static void setAnimTrigger(GameObject player, char attackPlayer, char attackEnnemy) {
		Animator playerAnimator = player.GetComponent<Animator>();
		GuardManager playerGuardManager = player.GetComponentInChildren<GuardManager>();
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
				playerGuardManager.setGuardPunch();
                //playerAnimator.SetTrigger("GuardPunch");
                break;
            case FightCharTab.GuardKick:
				playerGuardManager.setGuardKick();
                //playerAnimator.SetTrigger("GuardKick");
                break;
            case FightCharTab.GuardLaser:
				playerGuardManager.setGuardLaser();
                //playerAnimator.SetTrigger("GuardLaser");
                break;
            default:
                playerAnimator.SetTrigger("Fail");
                break;
        }
	}

}
