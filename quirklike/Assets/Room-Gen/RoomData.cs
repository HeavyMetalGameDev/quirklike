
using System;
using System.Collections.Generic;
using UnityEngine;


public class RoomData : MonoBehaviour
{
    // Start is called before the first frame update
    public enum RoomType { MainRoom, TransitionRoom };

    [SerializeField]
    private string roomID;
    public string GetRoomID() { return roomID; }

    [SerializeField]
    private Transform playerSpawn;
    public Transform GetPlayerSpawn() { return playerSpawn; }

    [SerializeField] 
    private RoomType roomType;

    public RoomType GetRoomType() { return roomType; }  


    [SerializeField]
    private List<GameObject> entryLinkPoints;
    public List<GameObject> GetEntryLinkPoints() { return entryLinkPoints; }

     

    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public GameObject GetEntryLinkPoint(float angle)
    {
        foreach (GameObject entry in entryLinkPoints)
        {
            if(-entry.transform.eulerAngles.y == angle)
            {
                return entry;
            }
        }
        return null;
    }

    public void RemoveEntryLinkPoint(int index)
    {
        entryLinkPoints.RemoveAt(index);
    }
    public void OnDrawGizmos()
    {
        if (entryLinkPoints.Count > 0) 
        {
            foreach (GameObject entry in entryLinkPoints)
            {
                Gizmos.color = Color.red;
                EntryPoint ep = entry.GetComponent<EntryPoint>();   
                //Debug.Log(entry.transform.eulerAngles.y);
                float angle = -ep.GetAngle() * Mathf.Deg2Rad;
                Gizmos.DrawLine(entry.transform.position, entry.transform.position + 5 * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));


                Gizmos.color = Color.green;
                angle = -(ep.GetAngle() + 180f) * Mathf.Deg2Rad;
                Gizmos.DrawLine(entry.transform.position, entry.transform.position + 1 * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
            }
        }
    }
}
