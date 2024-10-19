using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int totalPoints;

    public const string totalPrefName = "Total";
    public const string highPrefName = "High";
    public const string levelBeatenPrefName = "LvlBeaten";

    public int TotalPoints { get => totalPoints; set => totalPoints = value; }

    // Start is called before the first frame update
    void Start()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            totalPoints = PlayerPrefs.GetInt(totalPrefName);
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
        PlayerPrefs.SetInt(totalPrefName,totalPoints);
        LevelManager.instance?.hudController.UpdatePoints(TotalPoints);
        return totalPoints;
    }

    
}
