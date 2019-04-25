using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MonoTexture))]
public class MonoTextureEditor : Editor
{
    MonoTexture monoTexture;

    string newPrefabName = "NewPrefab";
    string prefabFolder = "Assets/Prefabs";

    void OnEnable()
    {
        monoTexture = (MonoTexture)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        monoTexture.OutlineTexture = EditorGUILayout.ObjectField("Texture", monoTexture.OutlineTexture, typeof(Texture2D), true) as Texture2D;

        EditorGUILayout.Space();
        monoTexture.Width = EditorGUILayout.IntField(new GUIContent("Width", "Set the width of the texture."), monoTexture.Width);

        monoTexture.Height = EditorGUILayout.IntField(new GUIContent("Height", "Set the height of the texture."), monoTexture.Height);
        EditorGUILayout.Space();

        monoTexture.Zindex = EditorGUILayout.IntField(new GUIContent("Z-Index", "Set which component is at top of others."), monoTexture.Zindex);
        monoTexture.KeepAspectRatio = EditorGUILayout.Toggle(new GUIContent("Keep Aspect Ratio", "Keep the aspect ratio-proportions of the texture.") , monoTexture.KeepAspectRatio);
        
        EditorGUILayout.Space();
        Separator();
        EditorGUILayout.Space();

        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Create New Prefab"))
        {
            if (prefabFolder == "")
            {
                prefabFolder = EditorUtility.OpenFolderPanel("Prefab Folder", "Assets", "Prefabs");
            }
            prefabFolder = EditorUtility.OpenFolderPanel("Prefab Folder", "Assets", "Prefabs");


            string tmp = System.Text.RegularExpressions.Regex.Split(prefabFolder, "/Assets")[1];
            string path = "Assets" + tmp + "/" + newPrefabName + ".prefab";

            Object newPrefab = PrefabUtility.CreateEmptyPrefab(path);
            if (newPrefab == null)
            {
                Debug.LogError("Couldn't Create the Asset. Path: " + path);
                return;
            }

            PrefabUtility.ReplacePrefab(monoTexture.gameObject, newPrefab, ReplacePrefabOptions.ReplaceNameBased);
            MonoHealthbar temp = newPrefab as MonoHealthbar;
            if (temp == null)
            {
                Debug.Log("Null");
                return;
            }

            Debug.Log("Prefab Succefully Created. (" + path + ")");
        }

        GUIStyle style = new GUIStyle();
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleLeft;

        newPrefabName = EditorGUILayout.TextField(newPrefabName);

        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

    }

    void Separator()
    {
        GUILayout.Box("", GUILayout.Width(Screen.width - 10), GUILayout.Height(3));
    }
}
