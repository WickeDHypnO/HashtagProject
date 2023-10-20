using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipUi : MonoBehaviour
{
    TextMeshProUGUI _textMesh;
    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.text = "";
    }

    public void SetTooltipText(string text)
    {
        _textMesh.text = text;
    }
}
