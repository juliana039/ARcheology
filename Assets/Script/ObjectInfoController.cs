using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectInfoController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject panel;


    public void SetVisible(bool isVisible = true)
    {
        panel.SetActive(isVisible);
    }

    public void SetObjectInfo(SOObjectInfo info)
    {
        titleText.text = info.objectName;
        descriptionText.text = info.description;
    }
}
