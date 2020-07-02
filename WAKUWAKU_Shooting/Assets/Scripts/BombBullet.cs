using UnityEngine;

public class BombBullet : MonoBehaviour
{
    Rigidbody rb;

    //弾の速度
    [SerializeField] float speed = 3.0f;

    //弾が消失するまでの時間
    float destroyTime = 2.0f;
    /// <summary>移動方向のベクトル</summary>
    float moveVector = 1.0f;
    //弾が生成されてからの時間
    float spawnTime = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //弾の移動
        float x, z;
        x = Random.Range(-moveVector, moveVector);
        z = Random.Range(-moveVector, moveVector);
        Vector3 pos = new Vector3(x, 0, z);
        gameObject.transform.Translate(pos);
        gameObject.transform.Rotate(pos);

        transform.rotation = Quaternion.LookRotation(pos.normalized);

        rb.velocity = transform.forward * speed;
    }

    void Update()
    {
        if (spawnTime > destroyTime) {
            MyKill();
        }
        MoveClamp();
    }

    void FixedUpdate()
    {
        spawnTime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")           MyKill();
        else if (other.gameObject.tag == "Bomb")     MyKill();
        else if (other.gameObject.tag == "Obstacle") MyKill();
    }

    /// <summary>左右の壁に当たったら反射させる処理</summary>
    void MoveClamp()
    {
        if (transform.position.x <= -5.8f || 5.9f <= transform.position.x) 
            rb.velocity = new Vector3(rb.velocity.x * -1, rb.velocity.y, rb.velocity.z);
    }

    /// <summary>自分を消す処理</summary>
    void MyKill() 
    {
        Destroy(gameObject);
    }
}
