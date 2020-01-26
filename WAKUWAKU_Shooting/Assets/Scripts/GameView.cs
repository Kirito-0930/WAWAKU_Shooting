using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    /// <summary> Player1のPrefab </summary>
    [SerializeField] GameObject _player1Prefab;
    /// <summary> Player2のPrefab </summary>
    [SerializeField] GameObject _player2Prefab;
    [SerializeField] GameObject blue;
    [SerializeField] GameObject red;

    Vector3 _centerPoint;
    Vector3 _pos;

    static public bool _player1 = false;
    static public bool _player2 = false;
    static public bool _gamePlay = false;

    void Start()
    {
        _centerPoint = new Vector3(UnityEngine.Random.Range(-1.0f, 1.3f), -5.0f, 0.0f);
        PlayerCreate(_player1Prefab);
        PlayerCreate(_player2Prefab);
        StartCoroutine(GameStart());
    }

    void Update()
    {
        if (_player1) {
            _gamePlay = false;
            red.SetActive(true);
        }
        else if (_player2) {
            _gamePlay = false;
            blue.SetActive(true);
        }

        if (!_gamePlay) {
            GoToTitle();
        }
    }

    void GoToTitle()
    {
        if (Input.GetButtonDown("Fire2")) {
            FadeManager.FadeOut(0);
        }
    }

    /// <summary>
    /// ゲームスタート時にPlayerを生成
    /// </summary>
    /// <param name="_player">_player1Prefab、_player2Prefabが渡される</param>
    void PlayerCreate(GameObject _player)
    {
        if (_player == _player1Prefab) {
            _pos = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized * 3.0f;
            Instantiate(_player, _centerPoint + _pos, Quaternion.identity);
        } else if (_player == _player2Prefab) {
            Instantiate(_player, _centerPoint + (-_pos), Quaternion.identity);
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3f);
        _gamePlay = true;
    }
}
