// Created by Timo Heijne

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAnimation)),
RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour {

    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Transform _enemy;

    private float forwardSpeed = 3;

    // Use this for initialization
    private void Start() {
        if (_playerAnimation == null)
            Debug.LogError("PlayerAttack :: Player Animation Is Null");
        _playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            float distance = Vector3.Distance(transform.position, _enemy.position);
            float dot = Vector3.Dot(Vector3.forward, transform.InverseTransformPoint(_enemy.position));

            switch (_playerAnimation.GetAttackType())
            {
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

            StartCoroutine(WaitForAnimMove());
        }
    }

    private IEnumerator WaitForAnimMove()
    {
        _playerMovement.ToggleCanMove(false);
        
        var anim = _playerAnimation.AnimController;
        while (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            yield return new WaitForEndOfFrame();
        }

        while (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        _playerMovement.ToggleCanMove(true);
    }
}
