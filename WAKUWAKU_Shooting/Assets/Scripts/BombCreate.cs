using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCreate : MonoBehaviour
{
    /// <summary> 画面に置ける爆弾の最大数 </summary>
    [SerializeField, Header("画面に置ける爆弾の最大数")] int _maxBombNum = 100;
    /// <summary> 開始時の爆弾の数 </summary>
    [SerializeField, Header("開始時の爆弾の数")] int _startBombNum = 3;
    /// <summary> 爆弾の出現間隔 </summary>
    [SerializeField, Header("爆弾の出現間隔")] float _bombCreateInterval = 10;
    /// <summary> 爆弾の出現間隔が減る度合 </summary>
    [SerializeField, Header("爆弾の出現間隔が減る度合")] float _decreaseInterval = 0.5f;
    /// <summary> 爆弾の最速出現間隔 </summary>
    [SerializeField, Header("爆弾の最速出現時間")] float _minInterval = 2.0f;
    /// <summary> 画面にある爆弾の数 </summary>
    [SerializeField] List<Bomb> _bombs = new List<Bomb>();

    float _time;

    void Start()
    {
        for (int i = 0; i < _startBombNum; i++) {
            Vector3 _pos = new Vector3(Random.Range(-5.0f, 5.0f), -5.3f, Random.Range(-3.1f, 3.1f));
            _bombs.Add(Bomb.Create(_pos));
        }
    }

    void Update()
    {
        if (_bombs.Count >= _maxBombNum) {
            
        }
        if (_bombCreateInterval <= _minInterval) {
            _bombCreateInterval = _minInterval;
        }
    }

    void FixedUpdate()
    {
        _time += Time.deltaTime;
        if (_bombCreateInterval <= _time) {
            _bombs.Add(Bomb.Create(new Vector3(Random.Range(-5.0f, 5.0f), -5.3f, Random.Range(-3.1f, 3.1f))));
            _bombCreateInterval -= _decreaseInterval;
            _time = 0;
        }
    }
}
