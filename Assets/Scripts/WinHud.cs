using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WinHud : MonoBehaviour
{
    //Components
    [SerializeField]
    private TMP_Text finalPointsText;
    [SerializeField]
    private Image[] stars;
    void Awake()
    {
        foreach (Image star in stars)
        {
            star.color = Color.black;
        }
    }

    public void WinningShow(int points, int maxPoints,int percentForMax)
    {
        finalPointsText.text = points.ToString("00000");
        //since we are using only int I am multiplying them by the percents to get the same result as if we were using decimal values
        switch(points)
        {
            case int i when (points*100) >= (maxPoints*percentForMax):
                        //Goes throgh all 3 and makes them visible
                        print("3 stars");
                        foreach (Image star in stars)
                        {
                            star.color = new Color(1, 0.99f, 0.16f);
                            print(star.color);
                        }
                break;
            case int i when ((points * 100) < (maxPoints * percentForMax)) && ((points * 100) >= (maxPoints * (percentForMax/2))):
                        //Goes through all stars except last one, which is done by taking the lenght -1  
                        print("2 stars");
                        for (int j = 0; j < stars.Length - 1; j++)
                        {
                            stars[j].color = new Color(1, 0.99f, 0.16f);
                            print(stars[j].color);
                        }
                break;
            case int i when ((points * 100) < (maxPoints * (percentForMax / 2)) && ((points * 100) >= (maxPoints * (percentForMax / 4)))):
                    print("1 star");
                    stars[0].color = new Color(1, 0.99f, 0.16f);
                    //new Color(255, 253f, 42f);
                    //Only needs to do it for the first star
                    print(stars[0].color);

                break;
            case int i when (points * 100) <= (percentForMax / 4):
                    print("0 stars");
                    //Nothing happens but needs to be taken into account
                break;
        }

    }
}
