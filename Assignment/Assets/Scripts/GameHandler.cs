using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public static int levelIndex;
    int totalCoins;

    private void Save()
    {
        totalCoins = PlayerController.totalCoinCount;
        levelIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Saved: Index is: " + levelIndex + ", Coins is: " + totalCoins);

        string[] lines = { levelIndex.ToString(), totalCoins.ToString() };
        File.WriteAllLines("Assets/SaveData.txt", lines);

    }

    private void Load()
    {
        string[] lines = File.ReadAllLines("Assets/SaveData.txt");
        Debug.Log(lines[0] + ":  " + lines[1]);
        levelIndex = int.Parse(lines[0]);
        totalCoins = int.Parse(lines[1]);

        PlayerController.totalCoinCount = totalCoins;
        SceneManager.LoadScene(levelIndex);
    }
}
