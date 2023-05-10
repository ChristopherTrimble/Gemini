// Author: Christopher Trimble
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Menu_Button : Button, IPointerEnterHandler, IPointerExitHandler
{
    public UtilitySO colors;
    public TextMeshProUGUI text;
    protected override void Awake()
    {
        colors = Resources.Load<UtilitySO>("UtilitySO");
        text = transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
    }

    public new void OnPointerEnter(PointerEventData eventData)
    {
        text.color = colors.colors[1];
    }

    public new void OnPointerExit(PointerEventData eventData)
    {
        text.color = colors.colors[0];
    }
}
