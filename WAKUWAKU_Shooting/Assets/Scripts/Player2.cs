using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    /// <summary> Playerのスピード </summary>
    [SerializeField] int _speed = 3;
    /// <summary> 弾のプレハブ </summary>
    [SerializeField] GameObject _bulletPrefab;
    /// <summary> Player2のHPゲージ </summary>
    [SerializeField] Image _hpGauge;
    /// <summary> 弾の発射点 </summary>
    [SerializeField] Transform _bulletSpawn;
    /// <summary> 弾の発射間隔 </summary>
    [SerializeField] float _interval = 0.3f;

    float _moveHorizontal;
    float _moveVertical;
    /// <summary> 弾発射中の経過時間 </summary>
    float _elapsedTime;
    /// <summary> Player2のHP </summary>
    public int _hp = 10;
    int _maxHp;

    void Start()
    {
        _maxHp = _hp;
    }

    void Update()
    {
        if (GameView._gamePlay) {
            Direction();
            Move();
            Clamp();
            DeathJudgment();
            PhysicalView();
        }
    }

    void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(_moveHorizontal * _speed, 0.0f, _moveVertical * _speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet") {
            _hp--;
        }
    }

    /// <summary>
    /// Player2の方向
    /// </summary>
    void Direction()
    {
        float _directionHorizontal = Input.GetAxis("Horizontal2_2");
        float _directionVertical = Input.GetAxis("Vertical2_2");
        if (_directionHorizontal != 0 || _directionVertical != 0) {
            var _direction = new Vector3(_directionHorizontal, 0, _directionVertical);
            transform.localRotation = Quaternion.LookRotation(_direction);
            Shoot();
        }
        else if (_moveHorizontal != 0 || _moveVertical != 0) {
            var _direction = new Vector3(-_moveHorizontal, 0, -_moveVertical);
            transform.localRotation = Quaternion.LookRotation(_direction);
        }
    }

    /// <summary>
    /// Player2の移動
    /// </summary>
    void Move()
    {
        _moveHorizontal = Input.GetAxis("Horizontal1_2");
        _moveVertical = Input.GetAxis("Vertical1_2");
    }

    /// <summary>
    /// Playerの移動制限
    /// </summary>
    void Clamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5.0f, 5.3f), transform.position.y, Mathf.Clamp(transform.position.z, -3.1f, 3.1f));
    }

    /// <summary>
    /// 弾を撃つ
    /// </summary>
    void Shoot()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _interval) {
            Instantiate(_bulletPrefab, _bulletSpawn.position, transform.localRotation);
            _elapsedTime = 0f;
        }
    }

    /// <summary>
    /// Player2が死んだかどうかの判定
    /// </summary>
    void DeathJudgment()
    {
        if (_hp <= 0) {
            GameView._player1 = true;
        }
    }

    /// <summary>
    /// PlayerHPの表示
    /// </summary>
    void PhysicalView()
    {
        _hpGauge.fillAmount = (float)_hp / _maxHp;
    }
}
