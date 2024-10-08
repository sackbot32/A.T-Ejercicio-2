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
    [Header("Panels")]
    [SerializeField] private GameObject endPanel;

    
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        Time.timeScale = 1.0f;
        endPanel.SetActive(false);
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
