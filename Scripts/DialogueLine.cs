using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Line", menuName = "Scriptable Audio/New Dialogue Line")]
public class DialogueLine : ScriptableObject
{
    public AudioClip clip;
    public string subtitle;
}
