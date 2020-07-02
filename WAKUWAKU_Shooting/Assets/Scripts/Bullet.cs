using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary> 弾が消える時のパーティクル </summary>
    [SerializeField] GameObject deathParticlePrefab;

    /// <summary> 弾の速度 </summary>
    [SerializeField] float speed = 5;
   
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = -transform.forward * speed;
        transform.Rotate(transform.forward, Random.Range(0, 360f));
    }

    void Update()
    {
        MoveClamp();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bomb" || other.gameObject.tag == "Obstacle")
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            MyKill();
        }
    }

    //この範囲を出たら自分を消す
    void MoveClamp()
    {
        if (transform.position.x <= -5.6f || 5.9f <= transform.position.x)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
            MyKill();
        }
        else if (transform.position.z <= -3.7f || 3.7f <= transform.position.z)
        {
            MyKill();
        }
    }

    void MyKill()
    {
        Destroy(gameObject);
    }
}
