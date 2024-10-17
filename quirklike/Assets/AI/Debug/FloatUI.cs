using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatUI : MonoBehaviour
{
    public Transform LookAt;
    public Vector3 Offset;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(LookAt.position + Offset);

        if(transform.position != pos)
        {
            transform.position = pos;
        }
    }
}
