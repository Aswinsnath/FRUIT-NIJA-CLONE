using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    public void Playy()
    {
        SceneManager.LoadScene("LEVEL1");
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

}
