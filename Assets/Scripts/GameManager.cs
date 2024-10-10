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
    private int defaultTime;
    private float levelTime;
    private int points;
    [SerializeField]
    [Tooltip("GameObject that holds all points")]
    private GameObject pointHolder;

    public float LevelTime { get => levelTime; set => levelTime = value; }

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        levelTime = defaultTime;
        hudController = GetComponent<HudController>();
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
        Invoke("DoesWin",0.05f);
    }
    
    private void DoesWin()
    {
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
        if (win)
        {
            endPanel.GetComponent<Image>().color = Color.green - new Color(0, 0, 0, 0.5f); ;
            endPanel.GetComponentInChildren<TMP_Text>().text = "Ganaste";
        } else
        {
            endPanel.GetComponent<Image>().color = Color.red - new Color(0,0,0,0.5f);
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
