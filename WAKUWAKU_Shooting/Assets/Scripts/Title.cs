using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    /// <summary> ボタンを押してからシーンが変わるまでの時間 </summary>
    [SerializeField] float _interval = 1.0f;
    [SerializeField] AudioClip _buttonSound;
    AudioSource _se;
    AudioSource _bgm;

    bool isStart;

    void Start()
    {
        isStart = true;
        FadeManager.FadeIn();
        _se = GetComponents<AudioSource>()[0];
        _bgm = GetComponents<AudioSource>()[1];
    }
 
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && isStart) 
        {
            isStart = false;
            _bgm.Stop();
            _se.PlayOneShot(_buttonSound);
            StartCoroutine(NextScene(_interval));
        }
    }

    IEnumerator NextScene(float _interval)
    {
        yield return new WaitForSeconds(_interval);
        FadeManager.FadeOut(1);
    }
}
