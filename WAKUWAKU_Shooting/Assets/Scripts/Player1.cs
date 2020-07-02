using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    /// <summary> 弾のプレハブ </summary>
    [SerializeField] GameObject bulletPrefab;
    /// <summary> HPゲージ </summary>
    [SerializeField] Image hpGauge;
    /// <summary> 弾の発射点 </summary>
    [SerializeField] Transform bulletSpawn;

    /// <summary> 弾の発射間隔 </summary>
    [SerializeField] float interval = 0.5f;

    [SerializeField] int maxHp = 10;
    /// <summary> 移動スピード </summary>
    [SerializeField] int speed = 3;
 
    Rigidbody rb;

    //弾を発射してからの経過時間
    float elapsedTime;
    float moveHorizontal;
    float moveVertical;

    //現在のHP
    int hp;

    void Start()
    {
        hp = maxHp;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameView.isGamePlay) {   //ゲームが開始されたかどうか
            DeathJudgment();
            Direction();
            Move();
            MoveClamp();
            PhysicalView();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet") hp--;
        else if (other.gameObject.tag == "BombBullet") hp--;
    }

    //Playerが死んだらGameViewに伝える
    void DeathJudgment()
    {
        if (hp == 0) GameView.isPlayer2Dead = true;
    }

    //Playerの向いている方向
    void Direction()
    {
        float directionHorizontal = Input.GetAxis("Horizontal2_1");
        float directionVertical = Input.GetAxis("Vertical2_1");

        if (directionHorizontal != 0 || directionVertical != 0)   //方向キーが入力されている場合
        {
            var direction = new Vector3(directionHorizontal, 0, directionVertical);
            transform.localRotation = Quaternion.LookRotation(direction);
            Shoot();
        }
        else if(moveHorizontal != 0 || moveVertical != 0)   //方向キーが入力されていない場合
        {
            var direction = new Vector3(-moveHorizontal, 0, -moveVertical);
            transform.localRotation = Quaternion.LookRotation(direction);
        }
    }

    //Playerの移動キーの入力
    void Move()
    {
        moveHorizontal = Input.GetAxis("Horizontal1_1");
        moveVertical = Input.GetAxis("Vertical1_1");
    }

    //Playerの移動制限
    void MoveClamp()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5.0f, 5.3f), transform.position.y, Mathf.Clamp(transform.position.z, -3.1f, 3.1f));
    }

    //現在のPlayerHPの表示
    void PhysicalView()
    {
        hpGauge.fillAmount = (float)hp / maxHp;
    }

    //弾を撃つ
    void Shoot()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > interval) 
        {
            GameView.Get().SE(GameView.SEType.shot);
            Instantiate(bulletPrefab, bulletSpawn.position, transform.localRotation);
            elapsedTime = 0f;
        }
    }
}
