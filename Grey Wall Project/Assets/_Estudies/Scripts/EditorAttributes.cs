using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorAttributes : MonoBehaviour
{
    [Header("Strings")]
    [Multiline] public string _mulitLine = "[Multiline]";
    [TextArea] public string _textArea = "[TextArea]";

    [Header("Floats")]
    [Tooltip("Idade do Usuario")] public int _idadeToolTip = 0;
}
