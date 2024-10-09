using UnityEditor;
using UnityEngine;
using System;
using System.Text;

public class scene_setup_window : EditorWindow
{
    [MenuItem("Window/IYWTDTLDIT/Add Scene")]
    public static void OpenCustomWindow(){
        var window = EditorWindow.GetWindow(typeof(scene_setup_window));
        var title = new GUIContent();
        title.text = "Scene Setup";
        window.titleContent = title;
    }
    
    private void OnGUI(){
        GUIStyle style = new GUIStyle();

        //Scene name 
        GUILayout.BeginHorizontal();
        GUILayout.Label("Enter scene name: ");

        string scene_name = "";
        scene_name = GUILayout.TextField(scene_name, 25);

        GUILayout.EndHorizontal();


        //Map position reference
        GUILayout.BeginHorizontal();
        GUILayout.Label("Map position reference: ");

        string map_ref = "";
        map_ref = GUILayout.TextField(map_ref, 25);

        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        if (GUILayout.Button("Create Scene")){
            int map_ref_int = int.Parse(map_ref);
            Debug.Log(style.alignment  + scene_name + " ID: " + map_ref_int);
        }
    }

    private void CreateScene(){

    }
}
