using Krearthur.GOP;
using NaughtyAttributes;
using UnityEngine;

public class GOSetSwitcherIds : MonoBehaviour
{
    [Button]
    private void Set()
    {
        var painter = GetComponent<GOPainter>();
        var switcher = GetComponent<GOSwitcher>();

        var ids = new int[painter.paintObjects.Length];
        for (var i = 0; i < painter.paintObjects.Length; i++)
            ids[i] = i;

        switcher.paintObjectIds = ids;
    }
}
