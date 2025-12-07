using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;
    public VideoClip[] cutscenes;
    public VideoPlayer videoPlayer;
    int cutsceneId;
    float currentSkipTime = 0;
    float skipTime = 3;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Time.timeScale = 1;
        cutsceneId = PlayerPrefs.GetInt("cutsceneId");
        videoPlayer.clip = cutscenes[cutsceneId];
        //videoPlayer.clip = cutscenes[0];
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(videoPlayer.frame + " " + videoPlayer.frameCount);
        if (Input.GetKey(KeyCode.Return))
        {
            currentSkipTime += Time.deltaTime;
            Debug.Log(Time.deltaTime);
        }
        else
        {
            currentSkipTime = 0;
        }
        if ((ulong)videoPlayer.frame + 1 == videoPlayer.frameCount || currentSkipTime >= skipTime)
        {
            Debug.Log("koniec");
            SceneManager.LoadScene("world");
        }
    }
}
