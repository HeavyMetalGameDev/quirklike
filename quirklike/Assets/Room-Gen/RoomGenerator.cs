using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public int roomsToGenerate;
    [SerializeField]
    private List<GameObject> roomPool;
    [SerializeField]
    private List<GameObject> transitionPool;

    public List<GameObject> generatedRooms;

    private GameObject currentEntryPoint;
    private GameObject currentRoom;
    private GameObject currentTransition;   
    void Start()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        generatedRooms = new List<GameObject>();
        GenerateRooms();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateRooms()
    {
        InitialiseRoomList();
        SpawnRooms();
    }

    private void InitialiseRoomList()
    {
        if (generatedRooms.Count > 0)
        {
            foreach (GameObject room in generatedRooms)
            {
                Destroy(room);
            }
            generatedRooms.Clear();
        }
        for (int i = 0; i < roomsToGenerate; i++)
        {
            int selectedRoom = UnityEngine.Random.Range(0, roomPool.Count);
            generatedRooms.Add(roomPool[selectedRoom]);
        }

    }
    public GameObject GetRandomEntryPoint(RoomData roomData)
    {
        int randomEntry = UnityEngine.Random.Range(0, roomData.GetEntryLinkPoints().Count);
        return roomData.GetEntryLinkPoints()[randomEntry];
    }
    private void SpawnRooms()
    {
        currentRoom = generatedRooms[0];
 
        Instantiate(currentRoom, Vector3.zero, new Quaternion(), this.transform);
        currentEntryPoint = GetRandomEntryPoint(currentRoom.GetComponent<RoomData>());
        Debug.Log(currentEntryPoint.name);
        Debug.Log(currentEntryPoint.transform.position);

        currentTransition = transitionPool[0];
        RoomData transitionRoomData = currentTransition.GetComponent<RoomData>();
        bool requiresRotation = true;

        Vector3 roomOffset = Vector3.zero;
        float adjustAngle = 0f;
        Vector3 entryOriginRelitive = Vector3.zero;
        foreach (GameObject transEntryPoint in transitionRoomData.GetEntryLinkPoints())
        {
            bool foundLink = transEntryPoint.transform.localEulerAngles.y == currentEntryPoint.transform.localEulerAngles.y + 180f;
    
            if (foundLink == true)
            {

                roomOffset = transEntryPoint.transform.localPosition;
                requiresRotation = false;
                break;
            }
        }
        Debug.Log(currentEntryPoint.transform.localPosition);
        Debug.Log(roomOffset);
        Instantiate(currentTransition,
            currentEntryPoint.transform.localPosition - roomOffset, new Quaternion(), this.transform);
    }
}
