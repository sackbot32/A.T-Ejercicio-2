using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinHud : MonoBehaviour
{
    //Components
    [SerializeField]
    private TMP_Text finalPointsText;
    [SerializeField]
    private Image[] stars;
    void Start()
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
                    print("3 stars");
                    //Goes throgh all 3 and makes them visible
                    foreach (Image star in stars)
                    {
                        star.color = Color.yellow;
                    }
                break;
            case int i when ((points * 100) < (maxPoints * percentForMax)) && ((points * 100) >= (maxPoints * (percentForMax/2))):
                    print("2 stars");
                    //Goes through all stars except last one, which is done by taking the lenght -1  
                    for (int j = 0; j < stars.Length - 1; j++)
                    {
                        stars[j].color = Color.yellow;
                    }
                break;
            case int i when ((points * 100) < (maxPoints * (percentForMax / 2)) && ((points * 100) >= (maxPoints * (percentForMax / 4)))):
                    print("1 star");
                    //Only needs to do it for the first star
                    stars[0].color = Color.yellow;

                break;
            case int i when (points * 100) <= (percentForMax / 4):
                    print("0 stars");
                    //Nothing happens but needs to be taken into account
                break;
        }
    }
}
