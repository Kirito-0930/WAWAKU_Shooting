using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    [SerializeField]
    private Bomb _bomb = null;

    public int _maxBombNum = 100;
    private Bomb[] _bombs = null;

    public int _startBombNum = 5;
    public float _startSpawnTime = 5.0f;
    public float _bombSpawnTimeInterval = 0.5f;
    public float _maxSpawnTime = 2.0f;

    private int _bombsCount = 0;

    void Start()
    {
        _bombsCount = _startBombNum;
        _bombs = new Bomb[_maxBombNum];
        for (int i = 0; i < _startBombNum; ++i)
        {
            CreateBomb();
        }
        StartCoroutine(SpawnBomb());
    }

    void Update()
    {
        var a = Time.deltaTime;
    }

    private void CreateBomb()
    {
        _bombs[_bombsCount] = GameObject.Instantiate(_bomb);
        Vector3 pos;
        pos.x = Random.Range(0, 10);
        pos.y = 0;
        pos.z = Random.Range(0, 10);
        _bombs[_bombsCount].transform.Translate(pos);
        _bombsCount++;
    }

    private IEnumerator CreateBombCoroutine() {
        CreateBomb();
        yield return null;//1フレーム
    }
    private IEnumerator SpawnBomb()
    {
        while (true)
        {
            yield return new WaitForSeconds(_startSpawnTime);//秒待つ
            yield return StartCoroutine(CreateBombCoroutine());
        }
    }
}
