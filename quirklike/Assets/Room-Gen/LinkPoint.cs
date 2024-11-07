using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkPoint : MonoBehaviour
{
    public enum LinkType { NoPriority, EntryOnly, ExitOnly };

    [SerializeField]
    public int linkPointID;

    [SerializeField]
    private LinkType linkType;
    public LinkType GetLinkType() { return linkType; }
    public float GetAngle()
    {
        return this.gameObject.transform.eulerAngles.y;
    }
}
