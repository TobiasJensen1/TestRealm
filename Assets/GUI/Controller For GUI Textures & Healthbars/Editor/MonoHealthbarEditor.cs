using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MonoHealthbar))]
public class MonoHealthbarEditor : Editor
{
    MonoHealthbar monoTexture;

    string newPrefabName = "NewPrefab";
    string prefabFolder = "Assets/Prefabs";

    void OnEnable()
    {
        monoTexture = (MonoHealthbar)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();
        monoTexture.OutlineTexture = EditorGUILayout.ObjectField("Outline Texture", monoTexture.OutlineTexture, typeof(Texture2D), true) as Texture2D;
        monoTexture.FillTexture = EditorGUILayout.ObjectField("Fill Texture", monoTexture.FillTexture, typeof(Texture2D), true) as Texture2D;

        EditorGUILayout.Space();
        monoTexture.Width = EditorGUILayout.IntField(new GUIContent("Width", "Set the width of the texture."), monoTexture.Width);
        monoTexture.Height = EditorGUILayout.IntField(new GUIContent("Height", "Set the height of the texture."), monoTexture.Height);
        
        EditorGUILayout.Space();
        monoTexture.Zindex = EditorGUILayout.IntField(new GUIContent("Z-Index", "Set which component is at top of others."), monoTexture.Zindex);
        monoTexture.KeepAspectRatio = EditorGUILayout.Toggle(new GUIContent("Keep Aspect Ratio", "Keep the aspect ratio-proportions of the texture."), monoTexture.KeepAspectRatio);
        monoTexture.Health = EditorGUILayout.IntField(new GUIContent("Health", "Set the health-precent of the 'healthbar'."), monoTexture.Health);

        if (Event.current.type == EventType.ValidateCommand 
               && Event.current.commandName == "UndoRedoPerformed")
        {
            // Undo/redo was performed. Repaint.
            Repaint();
        }

        EditorGUILayout.Space();
        Separator();
        EditorGUILayout.Space();
        
        GUILayout.Space(5);


        GUILayout.BeginHorizontal();

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.padding = new RectOffset(8, 8, 4, 5);
        if (GUILayout.Button("Create New Prefab", buttonStyle))
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

        GUIStyle style = new GUIStyle(GUI.skin.textField);
        style.fontStyle = FontStyle.Normal;
        style.alignment = TextAnchor.MiddleLeft;
        style.padding = new RectOffset(5, 3, 2, 3);
        style.fixedHeight = 22;

        newPrefabName = EditorGUILayout.TextField(newPrefabName, style);

        GUILayout.EndHorizontal();
        EditorGUILayout.Space();

    }

    void Separator()
    {
        GUILayout.Box("", GUILayout.Width(Screen.width - 10) , GUILayout.Height(3));
    }

    void OnGUI()
    {
        if (Event.current.type == EventType.DragUpdated)
        {
            // Something dragged from outside your panel
            // is currently OVER your panel.
            // Here you should check if the dragged types are valid,
            // and then show the correct draggin icon with something like:
            DragAndDrop.visualMode = DragAndDropVisualMode.Link;
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            // This event is fired if the dragged objects are released over your panel,
            // AND you didn't set DragAndDropVisualMode to Rejected.
            DragAndDrop.AcceptDrag(); // Accept the drag
            // Now you can parse the dragged objects
            // and import them using the DragAndDrop class.
        }

        OnGuiChange();
    }

    GUIContent buttonText = new GUIContent("some button");
    GUIStyle buttonStyle = GUIStyle.none; 
    private void OnGuiChange()
    {
        Rect rt  = GUILayoutUtility.GetRect(buttonText, buttonStyle); 
        if (rt.Contains(Event.current.mousePosition)) { 
            GUI.Label(new Rect(0,20,200,70), "PosX: " + rt.x + "\nPosY: " + rt.y + 
                  "\nWidth: " + rt.width + "\nHeight: " + rt.height);
        }
        GUI.Button(rt, buttonText, buttonStyle); 
    }

}
