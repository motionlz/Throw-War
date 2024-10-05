using System;
using System.Collections.Generic;
using UnityEngine;
using PhEngine.GoogleSheets;
using Newtonsoft.Json;
using System.Linq;

public class GetGoogleSheetData : MonoBehaviour
{
    [SerializeField] SheetDownloader downloader;
    [SerializeField] string spreadsheetId;
    [SerializeField] string sheetNameAndRange;
    [SerializeField] public static List<GameSettingData> resultList = new List<GameSettingData>();
    GameSettingData[] dataArray => resultList.ToArray();
        
    void Start()
    {
        downloader
            .CreateRequest<GameSettingData>(spreadsheetId, sheetNameAndRange)
            .OnSuccess(list => resultList = list)
            .Download();
    }

    public float GetValueByKey(string key)
    {
        return float.Parse(dataArray.FirstOrDefault(d => d.keyID == key).value);
    }
}

[Serializable]
public class GameSettingData
{
    [JsonProperty("Key_ID")]
    public string keyID;
    [JsonProperty("Value")]
    public string value;
}
