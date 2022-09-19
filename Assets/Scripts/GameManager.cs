using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum GamePlayStates
    {
        Game,
        Pause,
        End
    }

    public static GamePlayStates state;

    public static int lives = 30;
    public Text livesCounter;

    public static int money = 200;
    public Text moneyCounter;

    public static float score = 0;
    public Text scoreCounter;

    public GameObject instructions;

    public Button[] buttons;
    public GameObject loseText;
    public GameObject selected;
    public Camera cam;
    public Vector3 point;
    public GameObject nameField;
    public Text inputField;

    public static int lowestScore;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        state = GamePlayStates.Pause;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //update lives text
        livesCounter.text = $"Lives: {lives}";
        //update money text
        moneyCounter.text = $"${money}";
        //update score text
        scoreCounter.text = $"Score: {score.ToString("N0")}";
        //find mouse position
        point = Input.mousePosition;
        if (selected != null)
        {
            selected.transform.position = cam.ScreenToWorldPoint(new Vector3(point.x, point.y, 40));
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit, float.PositiveInfinity, 1 << 8))//layermask = tower base
            {
                //Debug.Log(hit.transform.parent.childCount); //should be 1 on empty base
                if (hit.transform.parent.childCount == 1 && money >= 50)
                {
                    selected.transform.SetParent(hit.transform.parent);
                    selected.transform.localPosition = new Vector3(0, 1, 0);
                    selected.GetComponentInChildren<ShootLaser>().enabled = true;
                    selected = null;
                    money -= 50;
                }
            }
        }
        //lose code
        if (lives <= 0)
        {
            lives = 999;
            Lose();
            state = GamePlayStates.End;
        }
    }

    public void SelectTower(GameObject tower)
    {
        if (selected == null)
        {
            selected = Instantiate(tower, point, Quaternion.identity);
        }
        else
        {
            Destroy(selected);
            selected = null;
        }
    }

    public void StartGame()
    {
        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
        state = GamePlayStates.Game;
        instructions.SetActive(false);
    }

    public void Lose()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }
        loseText.SetActive(true);
        if (score > lowestScore)
        {
            nameField.SetActive(true);
        }
    }

    public void Quit()
    {
        if (score > lowestScore)
        {
            NewScore();
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void NewScore()
    {
        Dictionary<string, int> scores = new Dictionary<string, int>();
        //initial value
        scores.Add(PlayerPrefs.GetString("First Name"), PlayerPrefs.GetInt("First Place"));
        scores.Add(PlayerPrefs.GetString("Second Name"), PlayerPrefs.GetInt("Second Place"));
        scores.Add(PlayerPrefs.GetString("Third Name"), PlayerPrefs.GetInt("Third Place"));

        //get name to 6 letters
        string name;
        if (inputField.text.Length > 6)
        {
            name = inputField.text.Substring(0, 6);
        }
        else
        {
            name = inputField.text;
        }
        //new value
        scores.Add(name, Mathf.CeilToInt(score));

        //sort
        scores = scores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        //save
        PlayerPrefs.SetString("First Name", scores.Keys.First());
        PlayerPrefs.SetInt("First Place", scores.Values.First());
        scores.Remove(scores.Keys.First());//remove first element of the list
        PlayerPrefs.SetString("Second Name", scores.Keys.First());
        PlayerPrefs.SetInt("Second Place", scores.Values.First());
        scores.Remove(scores.Keys.First());
        PlayerPrefs.SetString("Third Name", scores.Keys.First());
        PlayerPrefs.SetInt("Third Place", scores.Values.First());
    }
}
