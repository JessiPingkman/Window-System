using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WindowController))]
public class WindowControllerEditor : Editor
{
    private WindowController _windowController;

    public override void OnInspectorGUI()
    {
        _windowController = (WindowController)target;
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Opened Windows", EditorStyles.boldLabel);

        if (Application.isPlaying)
        {
            GUIStyle style = new GUIStyle
            {
                richText = true,
            };
            if (_windowController.GetWindows() != null)
            {
                foreach (Window window in _windowController.GetWindows())
                {
                    if (window.Opened)
                    {
                        if (GUILayout.Button($"<color=green>{window.name}</color>", style))
                        {
                            EditorGUIUtility.PingObject(window.gameObject);
                        }
                    }
                    else
                    {
                        if (GUILayout.Button($"<color=red>{window.name}</color>", style))
                        {
                            EditorGUIUtility.PingObject(window.gameObject);
                        }
                    }
                }
            }
        }
    }
}