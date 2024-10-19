using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelFiller : MonoBehaviour
{
    [SerializeField]
    private GameObject emptyButton;
    public MenuManager menuManager;
    [SerializeField]
    private int minIndex;


    // Start is called before the first frame update
    void Start()
    {
        for (int sceneIn = 0; sceneIn < SceneManager.sceneCountInBuildSettings; sceneIn++)
        {
            if(sceneIn > minIndex && sceneIn < SceneManager.sceneCountInBuildSettings)
            {
                GameObject btn = Instantiate(emptyButton,transform);
                //Despite of my attempts it always adds put SceneManager.sceneCountInBuildSettings in the method
                //btn.GetComponent<Button>().onClick.AddListener(() => menuManager.ChangeSceneByIndex(sceneIn));
                btn.GetComponent<LevelButton>().sceneIndex = sceneIn;
                if(sceneIn > PlayerPrefs.GetInt(GameManager.levelBeatenPrefName))
                {
                    btn.GetComponent<Button>().interactable = false;
                }

                btn.GetComponentInChildren<TMP_Text>().text = sceneIn.ToString();
                print("Indice"+sceneIn);
            }
        }
    }

    
}
