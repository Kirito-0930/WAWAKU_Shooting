using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{

    Vector3 _pos;

    private float _time = 0.0f;

    //消滅時間
    public float _destroyTime = 2.0f;

    //移動スピード
    public float _moveVol = 1.0f;

    [SerializeField] float _speed = 3.0f;

    Rigidbody _rb;

    void Start()
    {
        _pos.x = Random.Range(_moveVol * -1, _moveVol);
        _pos.z = Random.Range(_moveVol * -1, _moveVol);
        gameObject.transform.Translate(_pos);
        gameObject.transform.Rotate(_pos);
        _rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(_pos.normalized);
        _rb.velocity = transform.forward * _speed;
    }

    void Update()
    {
        if (_time > _destroyTime) {
            Destroy(gameObject);
        }
        Clamp();
    }

    void FixedUpdate()
    {
        _time += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
    }

    void Clamp()
    {
        if (transform.position.x <= -5.6f || 5.9f <= transform.position.x) {
            _rb.velocity = new Vector3(_rb.velocity.x * -1, _rb.velocity.y, _rb.velocity.z);
        }
    }
}
