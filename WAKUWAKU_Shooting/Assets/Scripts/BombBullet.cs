using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{

    [SerializeField] float _speed = 3.0f;
    float _time = 0.0f;
    /// <summary>消滅時間</summary>
    float _destroyTime = 2.0f;
    /// <summary>移動方向のベクトル</summary>
    float _moveVector = 1.0f;

    Rigidbody _rb;

    void Start()
    {
        Vector3 _pos = new Vector3(Random.Range(_moveVector * -1, _moveVector), 0, Random.Range(_moveVector * -1, _moveVector));
        gameObject.transform.Translate(_pos);
        gameObject.transform.Rotate(_pos);
        _rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(_pos.normalized);
        _rb.velocity = transform.forward * _speed;
    }

    void Update()
    {
        if (_time > _destroyTime) {
            Death();
        }
        Clamp();
    }

    void FixedUpdate()
    {
        _time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") Death();
        else if (other.gameObject.tag == "Bomb") Death();
        else if (other.gameObject.tag == "Obstacle") Death();
    }

    /// <summary>
    /// 左右の壁に当たった時の処理
    /// </summary>
    void Clamp()
    {
        if (transform.position.x <= -5.8f || 5.9f <= transform.position.x) 
            _rb.velocity = new Vector3(_rb.velocity.x * -1, _rb.velocity.y, _rb.velocity.z);
    }

    /// <summary>
    /// 自分が死ぬ処理
    /// </summary>
    void Death() 
    {
        Destroy(gameObject);
    }
}
