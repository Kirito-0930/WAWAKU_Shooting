using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{

    private Vector3 _pos;

    private float _time = 0.0f;

    public float _destroyTime = 2.0f;

    void Start()
    {
        _pos.x = Random.Range(-1.5f, 1.5f);
        _pos.z = Random.Range(-1.5f, 1.5f);
    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _destroyTime) {
            Destroy(this.gameObject);
        }
        transform.Translate(_pos);

    }
}
