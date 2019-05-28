using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCount : MonoBehaviour
{
    // Start is called before the first frame update
    public Text deathText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deathText.text = "You have died " + PlayerController.totalDeathCount + " times!";
    }
}
