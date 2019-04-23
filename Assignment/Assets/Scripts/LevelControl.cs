using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public int index = 1;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Loading level with build index
            SceneManager.LoadScene(index);

            //load level with scene name
            //SceneManager.LoadScene(levelName);

            //restart level
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
