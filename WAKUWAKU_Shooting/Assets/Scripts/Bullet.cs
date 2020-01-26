using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary> 弾の速度 </summary>
    [SerializeField] float _speed = 5;
    /// <summary> 弾が消える時のパーティクル </summary>
    [SerializeField] GameObject _deathParticlePrefab;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = -transform.forward * _speed;
        transform.Rotate(transform.forward, Random.Range(0, 360f));
    }

    void Update()
    {
        Clamp();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Obstacle") {
            Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
            Death();
        }
    }

    /// <summary>
    /// 弾の生存範囲
    /// </summary>
    void Clamp()
    {
        if (transform.position.x <= -5.6f || 5.9f <= transform.position.x) {
            Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
            Death();
        } else if (transform.position.z <= -3.7f || 3.7f <= transform.position.z) {
            Death();
        }
    }

    /// <summary>
    /// 自分のObjectを消す
    /// </summary>
    void Death()
    {
        Destroy(gameObject);
    }
}
