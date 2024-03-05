using System;
using UnityEditor;


#if UNITY_EDITOR
using UnityEngine;

[CustomEditor(typeof(Chunk))]
public class ChunkEditor : Editor
{
  private void OnSceneGUI()
  {
    GUIStyle style = new GUIStyle();
    style.alignment = TextAnchor.MiddleCenter;
    style.fontSize = 25;
    style.normal.textColor = Color.black;

    Chunk chunk = (Chunk)target;
    Handles.Label(chunk.transform.position, chunk.name, style);
  }
}
#endif