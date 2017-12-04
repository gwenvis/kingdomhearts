// Created by Timo Heijne

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    public enum Status {
        Idle,
        Walking
    }

    public enum AttackType {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Fail = 4       
    }

    private Status playerStatus = Status.Idle;
    private AttackType attackType = AttackType.None;
    private AttackType attackQueue = AttackType.None;

    [SerializeField]
    private Animator animController;

	// Use this for initialization
	void Start () {
        if (animController == null)
            Debug.LogError("PlayerAnimation :: Missing Animator");
	}

    /// <summary>
    /// Here we set the attack number, Which attack animation should we play?
    /// </summary>
    /// <param name="type"></param>
    public void PlayAttackNumber(AttackType type) {
        Debug.Log(type + " " + attackQueue);

        if (GetBool("Attack")) {
            // Animation is playing so we set Queue
            if(attackQueue == AttackType.None) // If attackQueue is set we ignore this new input.
                attackQueue = type;
        }
        else {
            // No animation is playing so we can play one
            animController.SetInteger("Attack Type", (int)type);
            animController.SetTrigger("Attack");
            attackType = type;

            StartCoroutine(WaitForAnimToBeDone());
        }     
    }

    /// <summary>
    /// Here we change the player animation status. Which animation should we play?
    /// </summary>
    /// <param name="_playerStatus"></param>
    public void ChangePlayerStatus(Status _playerStatus) {
        playerStatus = _playerStatus;

        if(playerStatus == Status.Idle) {
            animController.SetBool("Idle", true);
        } else {
            animController.SetBool("Idle", false);
        }

        if(playerStatus == Status.Walking) {
            animController.SetBool("Walking", true);
        } else {
            animController.SetBool("Walking", false);
        }
    }

    public AttackType GetAttackType() {
        return attackType;
    }

    public bool GetBool(string name) {
        return animController.GetBool(name);
    }

    IEnumerator WaitForAnimToBeDone() {
        yield return new WaitUntil(() => GetBool("Attack") == false);
        yield return new WaitForSeconds(2f);
        if (attackQueue != AttackType.None) {
            PlayAttackNumber(attackQueue);
            attackQueue = AttackType.None;
        } else {
            attackType = AttackType.None;
        }
    }
}

