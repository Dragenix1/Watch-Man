using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Option Save files", fileName = "Option Saver")]
public class BaseOptionScriptableObject : ScriptableObject
{
    [Range(0.0001f, 1f)] public float masterSave;
    [Range(0.0001f, 1f)] public float musicSave;
    [Range(0.0001f, 1f)] public float sfxSave;
}
