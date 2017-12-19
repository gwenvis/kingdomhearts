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

	private void Update () {
		float dist = Vector3.Distance(_target.transform.position, _player.transform.position);
		
	    _image.sprite = dist < 3f ? _attackRange : _idle;
		
		float size = Math.Min(450/dist, 70);
		_image.rectTransform.sizeDelta = new Vector2(size,size);
		
	    Vector3 pos = Camera.main.WorldToScreenPoint(_target.transform.position);
        transform.position = pos;
	}
}
