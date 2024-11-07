using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemSlot : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {   
        if(transform.childCount != 0){
            return;
        }
        
        GameObject dropped = eventData.pointerDrag;
        var draggableItem = dropped.GetComponent<UIGunIconDragable>();

        if (draggableItem != null){
            draggableItem.parentAfterDrag = transform;
        }
    }
}
