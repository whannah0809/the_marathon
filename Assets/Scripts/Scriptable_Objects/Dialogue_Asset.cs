using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "IYWTDTLDIT/Dialogue", order = 1)]
[System.Serializable]

public class Dialogue_Asset : ScriptableObject
{
    [System.Serializable]
    public class Dialogue_Line{
        [SerializeField] public int font_size = 50;
        [SerializeField] public string line;
    }

    [SerializeField]
    public Dialogue_Line[] lines;
}
