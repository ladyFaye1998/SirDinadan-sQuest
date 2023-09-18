using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Menu : MonoBehaviour
{

    public void StartGame()
    {
        Debug.Log("calledstartgame");
        Application.LoadLevel(1);
    }


    public void quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
