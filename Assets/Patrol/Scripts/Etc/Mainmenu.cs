using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("InGame");
    }
    public void GameEnd()
    {
        Application.Quit();
    }
}
