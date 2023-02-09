using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PortalData
{
    public int zIndex;
    public int xIndex;

    public PortalData(int a, int b)
    {
        this.zIndex = a;
        this.xIndex = b;
    }
}
