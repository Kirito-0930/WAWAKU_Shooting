using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private BombSpawn _bombSpawn = null;
    [SerializeField]
    private GameObject _bullet = null;

    private List<GameObject> _list;

    //爆発時の弾の数
    public int _bulletNum;

    //爆弾の耐久値
    public int _hp = 5;

    private bool is_life = true;

    public void Start()
    {
        _bombSpawn = FindObjectOfType<BombSpawn>();
        _list = new List<GameObject>();
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            BombAction();
        }
    }

    private void OnTriggerEnter(Collider other)//ぶつかったら｛｝内の処理が実行
    {
        if (!is_life) return;
        if (other.name == "Bullet1(Clone)")
        {
            BombAction();
        }
        else if (other.name == "BombBullet(Clone)" || other.name == "BombBullet2(Clone)")
        {
            _hp--;
            if (_hp == 0) BombAction();
        }
    }

    private GameObject CreateBullet()
    {
        var bullet = GameObject.Instantiate(_bullet);
        Vector3 pos = this.gameObject.transform.position;
        bullet.transform.Translate(pos);
        return bullet;
    }

    private void BombAction() {
        is_life = false;
        _bombSpawn.DecrementTotalbombsCount();
        for (int i = 0; i < _bulletNum; ++i)
        {
            _list.Add(CreateBullet());
        }
        Destroy(this.gameObject);
    }

}
