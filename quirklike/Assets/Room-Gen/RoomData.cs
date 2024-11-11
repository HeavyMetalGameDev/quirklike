
using System;
using System.Collections.Generic;
using UnityEngine;


public class RoomData : MonoBehaviour
{
    // Start is called before the first frame update
    public enum RoomType { MainRoom, TransitionRoom };

    [SerializeField]
    private int roomID; //id is in the context of the level i.e first room is ID 0 , second room is 1 etc
    public void SetRoomID(int id) { roomID = id; }
    public int GetRoomID() { return roomID; }

    [SerializeField]
    private Transform playerSpawn;
    public Transform GetPlayerSpawn() { return playerSpawn; }

    [SerializeField] 
    private RoomType roomType;

    public RoomType GetRoomType() { return roomType; }  


    [SerializeField]
    private List<GameObject> linkPoints;
    public ref List<GameObject> GetLinkPoints() { return ref linkPoints; }

     

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
        foreach (GameObject entry in linkPoints)
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
        linkPoints.RemoveAt(index);
    }
    public void OnDrawGizmos()
    {
        if (linkPoints.Count > 0) 
        {
            foreach (GameObject entry in linkPoints)
            {
                Gizmos.color = Color.red;
                LinkPoint ep = entry.GetComponent<LinkPoint>();   
                //Debug.Log(entry.transform.eulerAngles.y);
                float angle = -ep.GetAngle() * Mathf.Deg2Rad;
                Gizmos.DrawLine(entry.transform.position, entry.transform.position + 5 * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));


                Gizmos.color = Color.green;
                angle = -(ep.GetAngle() + 180f) * Mathf.Deg2Rad;
                Gizmos.DrawLine(entry.transform.position, entry.transform.position + 1 * new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)));
            }
        }
        if(playerSpawn)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(playerSpawn.position, new Vector3(1, 2, 1));
        }
    }
}
