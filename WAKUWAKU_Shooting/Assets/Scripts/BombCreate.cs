using System.Collections.Generic;
using UnityEngine;

public class BombCreate : MonoBehaviour
{
    /// <summary> 画面上にある爆弾の数 </summary>
    [SerializeField] List<Bomb> bombs = new List<Bomb>();

    /// <summary> 爆弾の生成間隔 </summary>
    [SerializeField, Header("爆弾の生成間隔")]                      float bombCreateInterval = 10;
    /// <summary> 爆弾の生成間隔が短くなる度合 </summary>
    [SerializeField, Header("爆弾の生成間隔が短くなる度合")] float decreaseInterval = 0.5f;
    /// <summary> 爆弾の最短生成間隔 </summary>
    [SerializeField, Header("爆弾の最短生成間隔")]                float minInterval = 2.0f;

    /// <summary> 画面に置ける爆弾の最大数 </summary>
    [SerializeField, Header("画面に置ける爆弾の最大数")]       int maxBombCount = 100;
    /// <summary> ゲームスタート時の爆弾の数 </summary>
    [SerializeField, Header("ゲームスタート時の爆弾の数")]    int startBombCount = 3;

    //経過時間を入れる
    float time;

    void Start()
    {
        for (int i = 0; i < startBombCount; i++)   //指定範囲内のランダムな位置に指定個数爆弾を生成
        {
            Vector3 _pos = new Vector3(Random.Range(-5.0f, 5.0f), -5.3f, Random.Range(-3.1f, 3.1f));
            bombs.Add(Bomb.Create(_pos));
        }
    }

    void Update()
    {
        if (bombCreateInterval <= minInterval)
        {
            bombCreateInterval = minInterval;
        }
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (bombCreateInterval <= time)   //指定時間経過したら爆弾を生成して生成間隔を短くする
        {
            bombs.Add(Bomb.Create(new Vector3(Random.Range(-5.0f, 5.0f), -5.3f, Random.Range(-3.1f, 3.1f))));
            bombCreateInterval -= decreaseInterval;
            time = 0;
        }
    }
}
