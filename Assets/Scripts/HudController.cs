using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    //Level timer
    private int minutes = 0;
    private int seconds = 0;
    //Health

    //Components
    [Header("Hud Texts")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI pointLeftText;
    [SerializeField] private GameObject hudParent;

    public GameObject HudParent { get => hudParent; set => hudParent = value; }

    private void Start()
    {
    }
    private void OnGUI()
    {
        timeText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        LevelManager.instance.LevelTime -= Time.deltaTime;
        seconds = (int)LevelManager.instance.LevelTime % 60; //Remainder
        minutes = (int)LevelManager.instance.LevelTime / 60;
    }

    /// <summary>
    /// Recieves a health value to change the ammount of health shown
    /// </summary>
    /// <param name="health"></param>
    public void UpdateHealth(float health)
    {
        healthText.text = health.ToString("00");
    }
    /// <summary>
    /// Recieves a point value to change the ammount of points shown
    /// </summary>
    /// <param name="points"></param>
    public void UpdatePoints(int points)
    {
        pointText.text = points.ToString("00000");
    }
    /// <summary>
    /// Recieves value to change the ammount of the points left shown
    /// </summary>
    /// <param name="left"></param>
    public void UpdateLeft(int left)
    {
        pointLeftText.text = left.ToString("00");
    }
}
