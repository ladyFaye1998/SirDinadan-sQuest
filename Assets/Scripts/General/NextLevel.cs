using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string levelName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(levelName);
    }
}
