using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    GameObject _bullet = null;

    private List<GameObject> _list;

    public int _bulletNum;

    public int _hp = 5;

    private float _time=0.0f;

    public void Start()
    {
        _list = new List<GameObject>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time>3.0f) { BombAction(); }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            BombAction();
        }
    }

    private void OnTriggerEnter(Collider other)//ぶつかったら｛｝内の処理が実行
    {
        if (other.name != "BombBullet(Clone)") return;
        Debug.Log("当たった！");
        _hp--;
        if (_hp<0) { BombAction(); }
    }




    private GameObject CreateBullet()
    {
        var bullet = GameObject.Instantiate(_bullet);
        return bullet;
    }

    private void BombAction() {
        for (int i = 0; i < _bulletNum; ++i)
        {
            _list.Add(CreateBullet());
        }
        Destroy(this.gameObject);
    }

}
