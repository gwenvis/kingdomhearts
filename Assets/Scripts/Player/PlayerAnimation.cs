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

    private Status _playerStatus = Status.Idle;
    private AttackType _attackType = AttackType.None;
    private AttackType _attackQueue = AttackType.None;

    [SerializeField]
    private Animator _animController;

	// Use this for initialization
    private void Start () {
        if (_animController == null)
            Debug.LogError("PlayerAnimation :: Missing Animator");
	}

    /// <summary>
    /// Here we set the attack number, Which attack animation should we play?
    /// </summary>
    /// <param name="type"></param>
    public void PlayAttackNumber(AttackType type) {
        if (GetBool("Attack")) {
            // Animation is playing so we set Queue
            if(_attackQueue == AttackType.None) // If attackQueue is set we ignore this new input.
                _attackQueue = type;
        }
        else {
            // No animation is playing so we can play one
            _animController.SetInteger("Attack Type", (int)type);
            _animController.SetTrigger("Attack");
            _attackType = type;

            StartCoroutine(WaitForAnimToBeDone());
        }     
    }

    /// <summary>
    /// Here we change the player animation status. Which animation should we play?
    /// </summary>
    /// <param name="_playerStatus"></param>
    public void ChangePlayerStatus(Status _playerStatus) {
        this._playerStatus = _playerStatus;

        if(this._playerStatus == Status.Idle) {
            _animController.SetBool("Idle", true);
        } else {
            _animController.SetBool("Idle", false);
        }

        if(this._playerStatus == Status.Walking) {
            _animController.SetBool("Walking", true);
        } else {
            _animController.SetBool("Walking", false);
        }
    }

    /// <summary>
    /// Returns Current Attack type
    /// </summary>
    /// <returns>AttackType</returns>
    public AttackType GetAttackType() {
        return _attackType;
    }
    
    public bool GetBool(string name) {
        return _animController.GetBool(name);
    }

    private IEnumerator WaitForAnimToBeDone() {
        yield return new WaitUntil(() => GetBool("Attack") == false);
        yield return new WaitForSeconds(1f);
        if (_attackQueue != AttackType.None) {
            PlayAttackNumber(_attackQueue);
            _attackQueue = AttackType.None;
        } else {
            _attackType = AttackType.None;
        }
    }
}

