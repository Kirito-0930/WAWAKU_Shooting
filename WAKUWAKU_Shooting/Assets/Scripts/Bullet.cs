﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary> 弾の速度 </summary>
    [SerializeField] float _speed = 5;
 
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = -transform.forward * _speed;
        transform.Rotate(transform.forward, Random.Range(0, 360f));
    }

    void Update()
    {
        
    }
}
