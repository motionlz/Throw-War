#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GoogleSheetData))]
public class ButtonEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        DrawDefaultInspector();
        
        GoogleSheetData script = (GoogleSheetData)target;

        if (GUILayout.Button("Get Google Sheet Data"))
        {
            script.GetGoogleSheetData();
        }
    }
}
#endif
