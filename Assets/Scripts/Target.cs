// Created by Timo Heijne

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour {

    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _player;

    [SerializeField] private Sprite _idle;
    [SerializeField] private Sprite _attackRange;
    [SerializeField] private Sprite _hit;

    [SerializeField] private Image _image;

	private bool _isAttacking = false;

	public static Target instance;

	private void Start() {
		instance = this;
	}

	private void Update () {	
		float dist = Vector3.Distance(_target.transform.position, _player.transform.position);

		if (_isAttacking) {
			_image.sprite = _hit;
		} else {
			_image.sprite = dist < 3f ? _attackRange : _idle;
		}			
		
		float size = Math.Min(450/dist, 70);
		_image.rectTransform.sizeDelta = new Vector2(size,size);
		
		Vector3 pos = Camera.main.WorldToScreenPoint(_target.transform.position);
		transform.position = pos;	
	}

	public void ShowAttack() {
		StartCoroutine(AttackIcon());
	}

	IEnumerator AttackIcon() {
		_isAttacking = true;
		yield return new WaitForSecondsRealtime(1f);
		_isAttacking = false;
	}
}
