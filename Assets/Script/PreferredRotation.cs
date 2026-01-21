using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreferredRotation : MonoBehaviour
{
    public Vector3 scannerRotation = new Vector3(0, 0, 0);
    public Vector3 cabinetRotation = new Vector3(0, 0, 0);
    public Vector3 scannerPositionOffset = new Vector3(0, 0, 0);
    public Vector3 cabinetPositionOffset = new Vector3(0, 0, 0);
    
    public Vector3 GetScannerRotation()
    {
        return scannerRotation;
    }
    
    public Vector3 GetCabinetRotation()
    {
        return cabinetRotation;
    }
    
    public Vector3 GetScannerOffset()
    {
        return scannerPositionOffset;
    }
    
    public Vector3 GetCabinetOffset()
    {
        return cabinetPositionOffset;
    }
}
