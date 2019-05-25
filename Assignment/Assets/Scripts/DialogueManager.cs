using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    GameObject miniMap;
    public Text dialogueText;
    public List<string> diagText = new List<string>();

    // Start is called before the first frame update
    void Start()
    {

        miniMap = GameObject.Find("HUDCanvas_MiniMap/MiniMap");
        
        diagText.Add("To jump press W/Up");
        diagText.Add("To double jump press W/Up again when in the air");
        diagText.Add("To run, press and hold shift");
        diagText.Add("You can also jump up walls!");
        diagText.Add("Collect coins for a high Score");
        diagText.Add("Watch out for sharp or hungry things!");
        diagText.Add("Check out the minimap to help you");
        diagText.Add("This is the portal that takes you home");
    }

    public static int tutorialCounter = 0;

    private void OnTriggerEnter2D(Collider2D theCollision)
    {
        
        if (theCollision.gameObject.CompareTag("Player"))
        {
            Debug.Log(diagText[tutorialCounter]);
            dialogueText.text = diagText[tutorialCounter];
            GetComponent<BoxCollider2D>().enabled = false;
            tutorialCounter++;

            /*if (tutorialCounter < 1) {
                miniMap.SetActive(false);
            }
            else
            {
                miniMap.SetActive(true);
            }*/
        }
    }
}
