using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public int sceneIndex;
    // Start is called before the first frame update
    public void ChangeSceneByIndex()
    {
        print("Indice a cambiar " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
}
