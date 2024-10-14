using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource[] audioSources;
    //0 Music
    //1 Power Up
    //2 Damage
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            int j = 0;
            audioSources = new AudioSource[transform.childCount];
            foreach (Transform child in transform)
            {
                if(child.GetComponent<AudioSource>() != null)
                {
                    audioSources[j] = child.GetComponent<AudioSource>();
                    j++;
                }
            }
        } else
        {
            Destroy(gameObject);
        }
        
    }

    public void PlayAudio(int whichOne)
    {
        if (audioSources[whichOne] != null)
        {
            audioSources[whichOne].Play();
            print("AudioPlayed: " + audioSources[whichOne].name);
        } else
        {
            print("audio error!");
        }
    }




    
}
