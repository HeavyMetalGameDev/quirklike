using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkPoint : MonoBehaviour
{
    [SerializeField]
    public int linkPointID;

    public float GetAngle()
    {
        return this.gameObject.transform.eulerAngles.y;
    }
}
