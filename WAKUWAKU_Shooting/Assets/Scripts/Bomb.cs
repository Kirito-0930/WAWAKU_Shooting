using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject _bullet;

    /// <summary> 爆発時の弾の数 </summary>
    int _bulletNum;

    /// <summary> 爆弾の耐久値 </summary>
    int _hp;

    bool isLife = true;

    /// <summary>
    /// Resources/PrefabsのフォルダからBombを出す
    /// </summary>
    /// <param name="_pos"> 出現ポイントが渡される </param>
    /// <returns> 自分（Bomb）を返す </returns>
    static public Bomb Create(Vector3 _pos)
    {
        var go = GameObject.Find("/Bombs");
        var e = Resources.Load<Bomb>("Prefabs/Bomb");
        var ins = Instantiate(e, go.transform);
        ins.transform.position = _pos;
        return ins;
    }

    void Start()
    {
        _hp = Random.Range(1, 5);
        _bulletNum = Random.Range(10, 25);
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Action();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isLife) return;
        if (other.gameObject.tag == "Bullet") {
            _hp--;
            if (_hp == 0) Action();
        }
        else if (other.gameObject.tag == "BombBullet") {
            _hp--;
            if (_hp == 0) Action();
        }
    }

    GameObject CreateBullet()
    {
        var bullet = Instantiate(_bullet);
        Vector3 pos = gameObject.transform.position;
        bullet.transform.Translate(pos);
        return bullet;
    }

    void Action() {
        isLife = false;
        for (int i = 0; i < _bulletNum; i++) {
            CreateBullet();
        }
        Destroy(gameObject);
    }
}
