using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public AudioClip bgmAudio;
    public GameObject playerObj;
    public Player player;
    private bool switchAudio =true;
    private bool switchActive =true;
    private void Start()
    {
        playerObj = GameObject.Find("Player").gameObject;
        player = playerObj.GetComponent<Player>();
    }
    void Update()
    {
        if(switchAudio)
        {
            SoundManager.instance.Play(bgmAudio, transform, true);
            switchAudio = false;
        }
    }
}
