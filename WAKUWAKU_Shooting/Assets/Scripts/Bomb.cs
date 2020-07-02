using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject bulletObject;

    /// <summary> 爆発時の弾の数 </summary>
    int bulletCount;
    /// <summary> 爆弾の耐久値 </summary>
    int bomb_Hp;

    //爆発した時、弾を出す関数
    GameObject CreateBullet()
    {
        var bullet = Instantiate(bulletObject);
        Vector3 pos = gameObject.transform.position;
        bullet.transform.Translate(pos);
        return bullet;
    }

    /// <summary>Resources/PrefabsのフォルダからBombを生成</summary>
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
        bomb_Hp = Random.Range(1, 6);
        bulletCount = Random.Range(10, 25);
        transform.Rotate(0, Random.Range(0, 360), 0);
    }

    void Update()
    {
        bomb_Hp = Mathf.Clamp(bomb_Hp, 0, 5);   //HPを0以下にしないようにする
        if (bomb_Hp == 0) Action();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")                bomb_Hp--;
        else if (other.gameObject.tag == "BombBullet") bomb_Hp--;
    }

    //爆発するときの関数
    void Action()
    {
        GameView.Get().SE(GameView.SEType.bomb);   //GameViewに爆発SEを鳴らすように指示

        Destroy(transform.GetChild(0).gameObject);      //爆弾の見た目を消す

        for (int i = 0; i < bulletCount; i++)
        {
            CreateBullet();
        }

        Destroy(gameObject);
    }
}
