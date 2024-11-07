using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[System.Serializable]
public class EntryPoint : MonoBehaviour
{
    [SerializeField]
    public int entryPointID;
    
    public float GetAngle()
    {
        return this.gameObject.transform.eulerAngles.y;
    }
}
