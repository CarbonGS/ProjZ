using UnityEngine;
using UnityEditor;

public class RotationEditorWindow : EditorWindow
{
    [MenuItem("Tools/Rotation Editor")]
    public static void ShowWindow()
    {
        GetWindow<RotationEditorWindow>("Rotation Editor");
    }
    private void OnGUI()
    {
        var axis = new Vector3(0,0,0);

        GUILayout.Label("Rotate Selected Objects", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Rotate +90 Degrees Around Y-Axis"))
        {
            axis = new Vector3(0, 90f, 0);
            RotateSelectedObjects(axis);
        }
        if (GUILayout.Button("Rotate -90 Degrees Around Y-Axis"))
        {
            axis = new Vector3(0, -90f, 0);
            RotateSelectedObjects(axis);
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Rotate +90 Degrees Around X-Axis"))
        {
            axis = new Vector3(90f, 0, 0);
            RotateSelectedObjects(axis);
        }
        if (GUILayout.Button("Rotate -90 Degrees Around X-Axis"))
        {
            axis = new Vector3(-90f, 0, 0);
            RotateSelectedObjects(axis);
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Rotate +90 Degrees Around Z-Axis"))
        {
            axis = new Vector3(0, 0, 90f);
            RotateSelectedObjects(axis);
        }
        if (GUILayout.Button("Rotate -90 Degrees Around Z-Axis"))
        {
            axis = new Vector3(0, 0, -90f);
            RotateSelectedObjects(axis);
        }
    }

    private void RotateSelectedObjects(Vector3 rotation)
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            Undo.RecordObject(obj.transform, "Rotate Object");
            obj.transform.Rotate(rotation, Space.Self);
        }
    }

}
