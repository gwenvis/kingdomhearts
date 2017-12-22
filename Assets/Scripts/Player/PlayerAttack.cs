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
            
            _playerAnimation.AnimController.SetBool("Walking", false);
            _playerMovement.ToggleCanMove(false);
        }
        
        var anim = _playerAnimation.AnimController;
        if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            _playerMovement.ToggleCanMove(true);
        }
        else
        {
            transform.position += (_enemy.position - transform.position).normalized * forwardSpeed * Time.deltaTime;
            RotateToEnemy();
            _playerMovement.ToggleCanMove(false);
        }
    }

    private void RotateToEnemy()
    {
        float speed = 10 * Time.deltaTime;
        var vector = _enemy.transform.position - transform.position;
        vector.y = 0;
        vector.Normalize();
        var lookrot = Quaternion.LookRotation(vector);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, lookrot, speed);
    }
}
