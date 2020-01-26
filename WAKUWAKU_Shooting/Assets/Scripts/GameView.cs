using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] GameObject _player1Prefab;
    [SerializeField] GameObject _player2Prefab;

    Vector3 _centerPoint;
    Vector3 _pos;

    void Start()
    {
        _centerPoint = new Vector3(UnityEngine.Random.Range(-1.0f, 1.3f), -5.0f, 0.0f);
        PlayerCreate(_player1Prefab);
        PlayerCreate(_player2Prefab);

        var controllerNames = Input.GetJoystickNames();
        Debug.Log(controllerNames[0] + "/" + controllerNames[1]);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) PlayerCreate(_player1Prefab);
        //KeyDownCheck();
    }

    void PlayerCreate(GameObject _player)
    {
        if (_player == _player1Prefab) {
            _pos = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f)).normalized * 3.0f;
            Instantiate(_player, _centerPoint + _pos, Quaternion.identity);
        } else if (_player == _player2Prefab) {
            Instantiate(_player, _centerPoint + (-_pos), Quaternion.identity);
        }
    }

    /// <summary>
    /// どのボタンが押されたかをチェックする
    /// </summary>
    void KeyDownCheck()
    {
        if(Input.anyKeyDown)
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode))) {
            if (Input.GetKeyDown(code)) {
                Debug.Log(code);
                break;
            }
        }
    }
}
