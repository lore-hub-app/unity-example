using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class SomeScriptEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Dialogue myScript = (Dialogue)target;
        if(GUILayout.Button("Generate Dialogue"))
        {
            myScript.GenerateDialogue();
        }
    }
}
