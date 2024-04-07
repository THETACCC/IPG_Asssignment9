using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class GachaManager : MonoBehaviour
{

    [SerializeField]
    Image[] ResultImage;
    [SerializeField]
    TextMeshProUGUI[] ResultName;
    [SerializeField]
    GachaResults[] GachaResults;

    public int resultInt = -1;
    public GameObject MaxDisplay;
    public Sprite Default;
    // Start is called before the first frame update
    void Update()
    {
        DisplayGachaResults();
        if (resultInt >= 4)
        {
            MaxDisplay.SetActive(true);
        }
        else
        {
            MaxDisplay.SetActive(false);
        }


    }

    public void DisplayGachaResults()
    {
        ResultImage[resultInt].sprite = GachaResults[resultInt].myImg;
        ResultName[resultInt].text = GachaResults[resultInt].myName;

    }


    public void Restart()
    {
        for (int i = 0; i < Mathf.Min(ResultImage.Length, ResultName.Length); i++)
        {
           ResultName[i].text = "";
           ResultImage[i].sprite = Default;
        }
        resultInt = -1;
    }

}
