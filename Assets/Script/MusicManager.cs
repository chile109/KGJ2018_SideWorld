using UnityEngine;
using System.Collections;

//拉到一個空物件後便能在任何程式碼中使用
//使用方式是 "MusicManager.order.指令"
//這東西有防止再次生成，只需要放一次在場上就不會消失,隨時都能使用

[RequireComponent(typeof(AudioSource))]

public class MusicManager : MonoBehaviour {
    //撥放器
    private AudioSource audioSource;
    //指標
    public static MusicManager order;
    //播放清單
    public AudioClip[] musicList;
    //防止再次生成
    public static bool hasOne;

    void Awake() {
        if(hasOne) {
            Destroy(gameObject);
        } else {
            audioSource = gameObject.GetComponent<AudioSource>();
            order = this;
            DontDestroyOnLoad(gameObject);
            hasOne = true;
        }
    }

    //撥放音樂  
    public void Play() {
        audioSource.Play();
    }

    //停止音樂
    public void Stop() {
        audioSource.Stop();
    }

    //停止音樂
    public void Pause() {
        audioSource.Pause();
    }

    //換音樂		(第一首是0)
    public void Change(int number) {
        if(musicList.Length - 1 >= number) {
            audioSource.clip = musicList[number];
        } else {
            print("MusicList -> Out of range!");
        }
    }

    //用來撥音效
    public void PlaySound(int number) {
        if(musicList.Length - 1 >= number) {
            audioSource.PlayOneShot(musicList[number]);
        } else {
            print("MusicList -> Out of range!");
        }
    }
}
