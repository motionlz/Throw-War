using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GoogleSheetData : MonoBehaviour
{
    [Header("API Settings")]
    [SerializeField]private string baseUrl = "https://sheets.googleapis.com/v4/spreadsheets";
    [SerializeField]private string apiKey;
    [SerializeField]private string sheetId;
    [SerializeField]private string range;
    [Header("Data List")]
    [SerializeField] private List<GameSettingData> resultList = new List<GameSettingData>();
    private GameSettingData[] dataArray => resultList.ToArray();

    private void Start()
    {
        StartCoroutine(FetchDataFromGoogleSheet());
    }

    private IEnumerator FetchDataFromGoogleSheet()
    {
        string url = $"{baseUrl}/{sheetId}/values/{range}?key={apiKey}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error fetching data: {request.error}");
            }
            else
            {
                var jsonResponse = JsonConvert.DeserializeObject<GoogleSheetResponse>(request.downloadHandler.text);

                ResponseToList(jsonResponse);
            }
        }
    }

    private void ResponseToList(GoogleSheetResponse jsonRp)
    {
        resultList.Clear();

        foreach (var item in jsonRp.values)
        {
            GameSettingData settingData = new GameSettingData
            {
                keyID = item[0],
                value = item[1]
            };

            resultList.Add(settingData);
        }
    }

    public float GetValueByKey(string key)
    {
        return float.Parse(dataArray.FirstOrDefault(d => d.keyID == key).value);
    }

}

[System.Serializable]
public class GoogleSheetResponse
{
    public List<List<string>> values;
}
