// Created by Timo Heijne

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation))]
public class PlayerAttack : MonoBehaviour {

    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private Transform _enemy;

    // Use this for initialization
    void Start() {
        if (_playerAnimation == null)
            Debug.LogError("PlayerAttack :: Player Animation Is Null");
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {

            float distance = Vector3.Distance(transform.position, _enemy.position);
            float dot = Vector3.Dot(Vector3.forward, transform.InverseTransformPoint(_enemy.position));

            if (dot > 0.4 && distance < 5) {
                switch (_playerAnimation.GetAttackType()) {
                    case PlayerAnimation.AttackType.None:
                        _playerAnimation.PlayAttackNumber(PlayerAnimation.AttackType.One);
                        break;
                    case PlayerAnimation.AttackType.One:
                        _playerAnimation.PlayAttackNumber(PlayerAnimation.AttackType.Two);
                        break;
                    case PlayerAnimation.AttackType.Two:
                        _playerAnimation.PlayAttackNumber(PlayerAnimation.AttackType.Three);
                        break;
                    case PlayerAnimation.AttackType.Three:
                        _playerAnimation.PlayAttackNumber(PlayerAnimation.AttackType.One);
                        break;
                }
            } else {
                //_playerAnimation.PlayAttackNumber(PlayerAnimation.AttackType.Fail);
                Debug.LogWarning("Test");
            }
        }
    }    
}
