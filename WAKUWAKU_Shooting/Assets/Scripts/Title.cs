using System.Collections;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] AudioClip buttonSound;

    /// <summary> ボタンを押してからシーンが変わるまでの時間 </summary>
    [SerializeField] float interval = 1.0f;

    AudioSource bgmSource;
    AudioSource seSource;

    void Start()
    {
        FadeManager.FadeIn();

        GetAudioSources();
    }
 
    void Update()
    {
        if (Input.GetButtonDown("Fire2")) 
        {
            bgmSource.Stop();
            seSource.PlayOneShot(buttonSound);
            StartCoroutine(NextScene(interval));
        }
    }

    void GetAudioSources()
    {
        bgmSource = GetComponents<AudioSource>()[0];
        seSource = GetComponents<AudioSource>()[1];
    }

    IEnumerator NextScene(float interval)
    {
        yield return new WaitForSeconds(interval);
        FadeManager.FadeOut(1);
    }
}
