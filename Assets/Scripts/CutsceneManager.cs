using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    public static CutsceneManager instance;
    public GameObject[] videoPlayer;
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
        videoPlayer[cutsceneId].SetActive(true);
        //videoPlayer.clip = cutscenes[0];
    }

    // Update is called once per frame
    /*void Update()
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
        bool returnToScene = true;
        foreach (var player in videoPlayer)
        {
            if (player.activeSelf == true)
            {
                returnToScene = false;
            }
        }
        if (returnToScene || currentSkipTime >= skipTime)
        {
            if (cutsceneId == videoPlayer.Length - 1)
            {
                SceneManager.LoadScene("start");
            }
            else
            {
                SceneManager.LoadScene("world");
            }
        }
    }*/
}
