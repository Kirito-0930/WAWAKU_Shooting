using System.Collections;
using UnityEngine;

public class GameView : MonoBehaviour
{
    static GameView mySelf;
    static public GameView Get() { return mySelf; }

    //player2が勝利した時の演出オブジェクト
    [SerializeField] GameObject blueWin;
    [SerializeField] GameObject player1Prefab;
    [SerializeField] GameObject player2Prefab;
    //player1が勝利した時の演出オブジェクト
    [SerializeField] GameObject redWin;

    //各BGM,SEの音源データ
    [SerializeField] AudioClip bgmGame;
    [SerializeField] AudioClip bgmResult;
    [SerializeField] AudioClip seBomb;
    [SerializeField] AudioClip seButton;
    [SerializeField] AudioClip seCountDown;
    [SerializeField] AudioClip seEnd;
    [SerializeField] AudioClip seShot;
    [SerializeField] AudioClip seStart;
  
    AudioSource bgmSource;
    AudioSource seSource;

    //プレイヤーの生成する位置に使用する
    Vector3 centerPoint;
    Vector3 pos;

    public enum SEType { bomb, shot };

    static public bool isGamePlay = false;
    static public bool isPlayer1Dead = false;
    static public bool isPlayer2Dead = false;

    bool isFinishCheck;

    void Awake()
    {
        mySelf = this;              //MySelfにこのスクリプトを入れる
        isPlayer1Dead = false; isPlayer2Dead = false; isGamePlay = false;   //最初プレイヤーは入力は受け付けない
        isFinishCheck = false;   //どちらかのプレイヤーのHPが0になるとtureになる
    }

    void Start()
    {
        centerPoint = new Vector3(Random.Range(-1.0f, 1.3f), -5.0f, 0.0f);   //円の原点を指定

        GetAudioSources();

        PlayerCreate(player1Prefab);
        PlayerCreate(player2Prefab);

        StartCoroutine(GameStart());
    }

    void Update()
    {
        if (isPlayer1Dead || isPlayer2Dead)
        {
            if (!isFinishCheck)
            {
                GameFinish();
                isFinishCheck = true;
            }
            GoToTitle();
        }
    }

    void Fade()
    {
        FadeManager.FadeOut(0);
    }

    /// <summary>ゲーム終了</summary>
    /// <returns>SEが鳴り終わるまで待つ</returns>
    void GameFinish()
    {
        bgmSource.Stop();
        seSource.PlayOneShot(seEnd);

        //勝った方の勝利演出をtureにする
        if (isPlayer1Dead) redWin.SetActive(true);
        else if (isPlayer2Dead) blueWin.SetActive(true);

        //プレイヤー入力の受付を停止
        isGamePlay = false;
        Invoke("FinishBGM", 0.3f);
    }

    void GetAudioSources()
    {
        bgmSource = GetComponents<AudioSource>()[0];
        seSource = GetComponents<AudioSource>()[1];
    }

    void FinishBGM()
    {
        bgmSource.clip = bgmResult;
        bgmSource.Play();
    }

    /// <summary>タイトルに戻る</summary>
    void GoToTitle()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            seSource.PlayOneShot(seButton);
            Invoke("Fade", 0.5f);
        }
    }

    /// <summary>ゲームスタート時にPlayerを生成</summary>
    /// <param name="_player">_player1Prefabか_player2Prefabが渡される</param>
    void PlayerCreate(GameObject _player)
    {
        if (_player == player1Prefab)
        {
            //半径1の円周上のランダムな点を指定
            pos = new Vector3(Random.Range(-1.0f, 1.0f), 0, Random.Range(-1.0f, 1.0f)).normalized * 3.0f;
            Instantiate(_player, centerPoint + pos, Quaternion.identity);
        }
        else if (_player == player2Prefab)
        {
            Instantiate(_player, centerPoint - pos, Quaternion.identity);
        }
    }

    /// <summary>SE音を鳴らす</summary>
    /// <param name="_seName">SEの種類</param>
    public void SE(SEType type)
    {
        seSource.Stop();
        switch (type)
        {
            case SEType.bomb:
                seSource.PlayOneShot(seBomb);
                break;
            case SEType.shot:
                seSource.PlayOneShot(seShot);
                break;
            default:
                break;
        }
    }

    // 3秒カウントしてからゲームを開始する
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.1f);
        bgmSource.clip = bgmGame;
        for (int i = 0; i < 3; i++)
        {
            seSource.PlayOneShot(seCountDown);
            yield return new WaitForSeconds(1f);
        }
        seSource.PlayOneShot(seStart);
        isGamePlay = true;
        yield return new WaitForSeconds(0.2f);
        bgmSource.Play();
    }
}
