using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GoogleSheetData gameSettingData;
    public GoogleSheetData gameSetting => gameSettingData;
    protected override void InitAfterAwake()
    {
        
    }

    private void Start() 
    {
        
    }
}
