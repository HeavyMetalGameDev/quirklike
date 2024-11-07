using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWorldHover : MonoBehaviour
{
    [SerializeField] float hoverTimeVertical = 2.0f;
    [SerializeField] float hoverVerticalDistance = 2.0f;
    private float hoverPeriodVertical;

    [SerializeField] float hoverTimeRotation = 2.0f;
    private float hoverPeriodRotation;

    [SerializeField] GameObject objectToHover;
    private float hoverTimerVertical = 0.0f;
    bool isHovering = true;

    void Update()
    {
        if(isHovering)Hover();
    }

    private void OnEnable()
    {
        BeginHovering();
    }

    private void OnDisable()
    {
        StopHovering();
    }

    void BeginHovering()
    {
        hoverTimerVertical = 0.0f;
        hoverPeriodVertical = 1 / hoverTimeVertical;
        hoverPeriodRotation = 1 / hoverTimeRotation;
    }

    void StopHovering()
    {
        //might be needed laters
    }

    void Hover()
    {
        hoverTimerVertical += Time.deltaTime;
        objectToHover.transform.Rotate(Vector3.up * Time.deltaTime * 360/hoverTimeRotation);
        objectToHover.transform.localPosition = Vector3.up * Mathf.Sin(Mathf.PI * 2 * hoverTimerVertical * hoverPeriodVertical) * hoverVerticalDistance;
    }
}
