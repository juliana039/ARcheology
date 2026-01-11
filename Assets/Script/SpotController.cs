using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotController : MonoBehaviour
{
    public bool IsOccupied ()
    {
        //retorna se o spot está ocupado ou não
        return transform.childCount > 0;
    }
}
