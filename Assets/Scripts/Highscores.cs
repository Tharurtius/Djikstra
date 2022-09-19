using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscores : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Setup"))//setup
        {
            PlayerPrefs.SetInt("Setup", 1);//sets up some default values
            PlayerPrefs.SetInt("First Place", 1000);
            PlayerPrefs.SetString("First Name", "Jeff");
            PlayerPrefs.SetInt("Second Place", 100);
            PlayerPrefs.SetString("Second Name", "Jimbob");
            PlayerPrefs.SetInt("Third Place", 10);
            PlayerPrefs.SetString("Third Name", "Andrew");
        }
        //get playerprefs
        int first = PlayerPrefs.GetInt("First Place");
        int second = PlayerPrefs.GetInt("Second Place");
        int third = PlayerPrefs.GetInt("Third Place");

        string winner = PlayerPrefs.GetString("First Name");
        string firstLoser = PlayerPrefs.GetString("Second Name");
        string gitgud = PlayerPrefs.GetString("Third Name");
        //print
        gameObject.GetComponent<Text>().text = $"Highscores:\n{winner} : {first}\n{firstLoser} : {second}\n{gitgud} : {third}";
        //send lowest score to GM
        GameManager.lowestScore = third;
    }
}
