using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    JSONLoader jsonLoader;
    [SerializeField]
    JSONGachaPool jsonGachaPool;
    [SerializeField]
    TextMeshProUGUI tX_Name;
    [SerializeField]
    Image img;
    [SerializeField]
    GachaResults[] GachaResults;
    private int currentGacha = 0;
    public string GachaURL;

    public GachaManager gachaManager;

    // Start is called before the first frame update
    void Start()
    {
        jsonLoader = GetComponent<JSONLoader>();

    }

    public void StartGacha()
    {
        if(gachaManager.resultInt < 4)
        {
            GachaRandomize();
            jsonGachaPool.StartRefreshJSONGacha();
            jsonGachaPool.jsonRefreshed += ReadJSON;
        }


    }

    public void GachaRandomize()
    {
        currentGacha = Random.Range(0, 19);
    }

	private void OnDestroy()
    {
        jsonGachaPool.jsonRefreshed -= ReadJSON;
        jsonLoader.jsonRefreshed -= ReadImageJSON;
    }

	// Update is called once per frame
	public void ReadJSON(JSONNode json)
    {
        print(json["results"][currentGacha]["url"]);
        GachaURL = json["results"][currentGacha]["url"];
        //print(json["abilities"][0]["ability"]["name"]);
        PassToImageLoader();

        //print(imageURL);

    }

    public void PassToImageLoader()
    {
        jsonLoader.urlRead = GachaURL;
        jsonLoader.StartRefreshJSON();
        jsonLoader.jsonRefreshed += ReadImageJSON;
    }

    public void ReadImageJSON(JSONNode json)
    {
        tX_Name.text = json["name"];
        string imageURL = json["sprites"]["other"]["home"]["front_default"];
        StartCoroutine(DownloadImage(imageURL));
        GachaResults[gachaManager.resultInt].myName = tX_Name.text;

    }

    IEnumerator DownloadImage(string imageURL)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
		else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            img.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            GachaResults[gachaManager.resultInt].myImg = img.sprite;

        }
    }

    public void GachaInventoryUpdate()
    {
        gachaManager.resultInt += 1;
    }
}
