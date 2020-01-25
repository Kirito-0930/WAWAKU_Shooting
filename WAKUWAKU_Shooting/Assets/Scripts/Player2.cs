using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Player1
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Player2の方向
    /// </summary>
    public override void Direction()
    {
        float _directionHorizontal = Input.GetAxis("Horizontal2_2");
        float _directionVertical = Input.GetAxis("Vertical2_2");
        if (_directionHorizontal != 0 || _directionVertical != 0) {
            var _direction = new Vector3(_directionHorizontal, 0, _directionVertical);
            transform.localRotation = Quaternion.LookRotation(_direction);
            Shoot();
        }
        else if (_moveHorizontal != 0 || _moveVertical != 0) {
            var _direction = new Vector3(-_moveHorizontal, 0, -_moveVertical);
            transform.localRotation = Quaternion.LookRotation(_direction);
        }
    }

    /// <summary>
    /// Player2の移動
    /// </summary>
    public override void Move()
    {
        _moveHorizontal = Input.GetAxis("Horizontal1_2");
        _moveVertical = Input.GetAxis("Vertical1_2");
    }
}
