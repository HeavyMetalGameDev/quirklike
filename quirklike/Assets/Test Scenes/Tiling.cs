using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Tiling : MonoBehaviour
{
    public enum GeometryType
    {
        Plane,
        Cube
    }
    // Start is called before the first frame update
    [SerializeField] private GeometryType geomType = GeometryType.Plane;
    private float texScale = 0;
    const float PLANE_SCALE = 4.0f;
    const float CUBE_SCALE = 0.4f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (geomType)
        {
            case GeometryType.Plane:
                texScale = PLANE_SCALE;
                break;
            case GeometryType.Cube:
                texScale = CUBE_SCALE;
                break;
        }
        GetComponent<Renderer>().sharedMaterial.
            SetTextureScale("_MainTex", new Vector2(transform.localScale.x * texScale, transform.localScale.z * texScale));
    }
}
