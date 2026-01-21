using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectInfo", menuName = "ScriptableObjects/ObjectInfo")]
public class SOObjectInfo : ScriptableObject
{
    public string objectName;
    [TextArea(3, 10)]
    public string description;
    
    public CabinetType correctCabinet; // NOVO CAMPO
}
