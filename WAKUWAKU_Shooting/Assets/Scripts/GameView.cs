using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    static GameView mySelf;
    static public GameView Get() { return mySelf; }

    /// <summary> Player1のPrefab </summary>
    [SerializeField] GameObject _player1Prefab;
    /// <summary> Player2のPrefab </summary>
    [SerializeField] GameObject _player2Prefab;
    [SerializeField] GameObject _blue;
    [SerializeField] GameObject _red;
    [SerializeField] AudioClip _seEnd;
    [SerializeField] AudioClip _seCountDown;
    [SerializeField] AudioClip _seStart;
    [SerializeField] AudioClip _seShot;
    [SerializeField] AudioClip _seBomb;
    [SerializeField] AudioClip _seButton;
    [SerializeField] AudioClip _bgmGame;
    [SerializeField] AudioClip _bgmResult;
    AudioSource _se;
    AudioSource _bgm;

    Vector3 _centerPoint;
    Vector3 _pos;

    public enum SEType { bomb, shot };
    static public bool isPlayer1 = false;
    static public bool isPlayer2 = false;
    static public bool isGamePlay = false;
    bool isCheck;

    void Awake()
    {
        mySelf = this;
        isPlayer1 = false; isPlayer2 = false; isGamePlay = false;
        isCheck = true;
    }

    void Start()
    {
        _se = GetComponents<AudioSource>()[0];
        _bgm = GetComponents<AudioSource>()[1];
        _centerPoint = new Vector3(UnityEngine.Random.Range(-1.0f, 1.3f), -5.0f, 0.0f);
        PlayerCreate(_player1Prefab);
        PlayerCreate(_player2Prefab);
        StartCoroutine(GameStart());
    }

    void Update()
    {
        if (isPlayer1 || isPlayer2)
        {
            if (isCheck)
            {
                Finish();
                isCheck = false;
            }
            GoToTitle();
        }
    }

    /// <summary>
    /// タイトルに戻る
    /// </summary>
    void GoToTitle()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _se.PlayOneShot(_seButton);
            Invoke("Fade", 0.5f);
        }
    }

    void Fade()
    {
        FadeManager.FadeOut(0);
    }

    /// <summary>
    /// ゲームスタート時にPlayerを生成
    /// </summary>
    /// <param name="_player">_player1Prefab、_player2Prefabが渡される</param>
    void PlayerCreate(GameObject _player)
    {
        if (_player == _player1Prefab)
        {
            _pos = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized * 3.0f;
            Instantiate(_player, _centerPoint + _pos, Quaternion.identity);
        }
        else if (_player == _player2Prefab)
        {
            Instantiate(_player, _centerPoint - _pos, Quaternion.identity);
        }
    }

    /// <summary>
    /// SE音を鳴らす
    /// </summary>
    /// <param name="_seName">SEの種類</param>
    public void SE(SEType type)
    {
        _se.Stop();
        switch (type)
        {
            case SEType.bomb:
                _se.PlayOneShot(_seBomb);
                break;
            case SEType.shot:
                _se.PlayOneShot(_seShot);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ゲーム終了
    /// </summary>
    /// <returns>SEが鳴り終わるまで待つ</returns>
    void Finish()
    {
        _bgm.Stop();
        _se.PlayOneShot(_seEnd);
        if (isPlayer1) _red.SetActive(true);
        else if (isPlayer2) _blue.SetActive(true);
        isGamePlay = false;
        Invoke("FinishBGM", 0.3f);
    }

    void FinishBGM()
    {
        _bgm.clip = _bgmResult;
        _bgm.Play();
    }

    /// <summary>
    /// ゲームを開始する
    /// </summary>
    /// <returns>3秒待つ</returns>
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.1f);
        _bgm.clip = _bgmGame;
        for (int i = 0; i < 3; i++)
        {
            _se.PlayOneShot(_seCountDown);
            yield return new WaitForSeconds(1f);
        }
        _se.PlayOneShot(_seStart);
        isGamePlay = true;
        yield return new WaitForSeconds(0.2f);
        _bgm.Play();
    }
}
