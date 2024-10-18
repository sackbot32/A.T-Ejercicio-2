using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalPoints;

    private string pointName;

    public int TotalPoints { get => totalPoints; set => totalPoints = value; }

    // Start is called before the first frame update
    void Start()
    {
        pointName = "Total";
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            totalPoints = PlayerPrefs.GetInt(pointName);
        } else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Updates the new total points
    /// Returns them
    /// </summary>
    /// <param name="newScore"></param>
    /// <returns></returns>
    public int UpdateTotal(int newScore)
    {
        TotalPoints += newScore;
        PlayerPrefs.SetInt(pointName,totalPoints);
        LevelManager.instance?.hudController.UpdatePoints(TotalPoints);
        return totalPoints;
    }

    
}
