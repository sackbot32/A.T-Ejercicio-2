using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    //Components
    public HudController hudController;
    [Header("Panels")]
    [SerializeField] private GameObject endPanel;

    //Level settings
    [Header("LevelSettings")]
    [SerializeField]
    [Tooltip("Time in seconds that the level has")]
    private int defaultTime;
    [SerializeField]
    [Tooltip("GameObject that holds all points")]
    private GameObject pointHolder;
    [SerializeField]
    [Tooltip("How many points will each second give when the level is over")]
    private int timePointsMultiplier;
    [SerializeField]
    [Tooltip("How many points will each health point give when the level is over")]
    private int healthPointsMultiplier;
    [SerializeField]
    [Tooltip("How many points will a power up give")]
    private int pointValue;
    [SerializeField]
    [Tooltip("What percent of max points does the player need to get 3 stars")]
    [Range(1f, 100f)]
    //1/2 percent will be the one needed for 2 stars, 1/4 for 1 star
    private int percentForMaxRating;



    //LevelInformation
    private float levelTime;
    private int points;
    [SerializeField]
    [Tooltip("Only on inspector to be seen, don't modify unless testing")]
    private int maxPosiblePoints;
    

    public float LevelTime { get => levelTime; set => levelTime = value; }

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        maxPosiblePoints = (defaultTime* timePointsMultiplier) + 
            (Mathf.CeilToInt(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().MaxHealth) * healthPointsMultiplier) + 
            pointHolder.transform.childCount * pointValue;
        hudController = GetComponent<HudController>();
        hudController.HudParent.SetActive(true);
        levelTime = defaultTime;
        hudController.UpdateLeft(pointHolder.transform.childCount);
        Time.timeScale = 1.0f;
        endPanel.SetActive(false);
    }

    private void Update()
    {
        if(levelTime <= 0f) 
        {
            EndState(false);
        }
    }
    /// <summary>
    /// Adds ammount of point provided by pointAdded
    /// If there are no points left in pointHolder will win the game
    /// </summary>
    /// <param name="pointAdded"></param>
    public void GetPoint(int pointAdded)
    {
        
        points += pointAdded;
        hudController.UpdatePoints(points);
        //We invoke to check if all the points have been collected, since its if its done immendiently it gets the previous ammount of child
        Invoke("DoesWin",0.05f);
    }
    /// <summary>
    /// Used to check if the player has collected all items and if the player has, it wins the game
    /// </summary>
    private void DoesWin()
    {
        hudController.UpdateLeft(pointHolder.transform.childCount);
        if (pointHolder.transform.childCount <= 0)
        {
            EndState(true);
        }
    }



    /// <summary>
    /// Activates the end panel and modifies it depending if it wins or loses
    /// </summary>
    /// <param name="win"></param>
    public void EndState(bool win)
    {
        Time.timeScale = 0;
        hudController.HudParent.SetActive(false);
        if (win)
        {
            endPanel.GetComponent<Image>().color = Color.green - new Color(0, 0, 0, 0.5f);
            //The child on the 1 position of the endPanel is a gameObject with all data that has to be shown to the player when they win
            endPanel.transform.GetChild(1).gameObject.SetActive(true);
            //The child on the 2 position of the endPanel is a gameObject with the button to go back (maybe add a retry button?)
            endPanel.transform.GetChild(2).gameObject.SetActive(false);
            int playerHealth = Mathf.CeilToInt(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().CurrentHealth);
            int finalResult = points + (Mathf.CeilToInt(levelTime) * timePointsMultiplier) + (playerHealth * healthPointsMultiplier);
            endPanel.transform.GetChild(1).gameObject.GetComponent<WinHud>().WinningShow(finalResult,maxPosiblePoints,percentForMaxRating);
            
            endPanel.GetComponentInChildren<TMP_Text>().text = "Ganaste";
        } else
        {
            endPanel.GetComponent<Image>().color = Color.red - new Color(0,0,0,0.5f);
            //The child on the 1 position of the endPanel is a gameObject with all data that has to be shown to the player when they win
            endPanel.transform.GetChild(1).gameObject.SetActive(false);
            //The child on the 2 position of the endPanel is a gameObject with the button to go back (maybe add a retry button?)
            endPanel.transform.GetChild(2).gameObject.SetActive(true);
            endPanel.GetComponentInChildren<TMP_Text>().text = "Perdiste :c";
        }
        endPanel.SetActive(true);
    }

    /// <summary>
    /// Changes the scene by sceneName
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
