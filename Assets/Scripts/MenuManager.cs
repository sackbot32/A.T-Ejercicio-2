using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text highScore;
    [SerializeField]
    private GameObject mainPanel;
    [SerializeField]
    private GameObject levelPanel;

    private void Start()
    {
        levelPanel.SetActive(false);
        int i = PlayerPrefs.GetInt(GameManager.highPrefName);
        if(i < 0)
        {
            i = 0;
        }
        highScore.text = i.ToString("00000");
    }
    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Changes the scene by sceneName
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ChangeSceneByIndex(int sceneIn)
    {
        print("Indice a cambiar "+ sceneIn);
        SceneManager.LoadScene(sceneIn);
    }

    public void ExitBt()
    {
        Application.Quit();
    }

    public void InterChangePanel()
    {
        mainPanel.SetActive(!mainPanel.activeSelf);
        levelPanel.SetActive(!levelPanel.activeSelf);
    }
}
