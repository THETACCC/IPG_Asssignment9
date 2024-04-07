using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


public class JSONGachaPool : MonoBehaviour
{

    [SerializeField]
    string url = "https://pokeapi.co/api/v2/pokemon";
    public delegate void JSONRefreshedGacha(JSONNode json);
    public JSONRefreshedGacha jsonRefreshed;

    public JSONNode currentJSONGacha;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StartRefreshJSONGacha()
    {
        //For external use
        StartCoroutine(RefreshJSONGacha());
    }

    IEnumerator RefreshJSONGacha()
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.error == null)
        {
            currentJSONGacha = JSON.Parse(www.text);
            jsonRefreshed.Invoke(currentJSONGacha);
        }
        else
        {
            //Display disconnected
            Debug.Log("ERROR: " + www.error);
        }
        StopCoroutine(RefreshJSONGacha());
    }
}
