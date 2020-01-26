using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{

    private Vector3 _pos;

    private float _time = 0.0f;

    //消滅時間
    public float _destroyTime = 2.0f;

    //移動スピード
    public float _moveVol = 1.0f;

    [SerializeField]
    private float _speed = 3.0f;

    Rigidbody _rb;

    void Start()
    {
        _pos.x = Random.Range(-_moveVol, _moveVol);
        _pos.z = Random.Range(-_moveVol, _moveVol);
        gameObject.transform.Translate(_pos);
        gameObject.transform.Rotate(_pos);
        _rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(_pos.normalized);
        GetComponent<Rigidbody>().velocity = transform.forward * _speed;
    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time > _destroyTime) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)//ぶつかったら｛｝内の処理が実行
    {

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Wall")
        {
            //1.動いてる物体(this)と当たる物体(other)から反射ベクトルをとる
            Vector3 reflectVec = Vector3.Reflect(this.transform.forward, other.transform.up);
            //2.動いてる物体のrotationを変更する
            this.transform.rotation = Quaternion.LookRotation(reflectVec.normalized);
            //3.動いてる物体のrigidbodyのベロシティを変更する
            _rb.velocity = _speed * this.transform.forward;

        }
    }

}
