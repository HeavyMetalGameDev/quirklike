using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public int roomsToGenerateCount;

    [SerializeField]
    private GameObject startingRoom;
    public GameObject GetStartingRoom() { return startingRoom; }


    [SerializeField]
    private GameObject endingRoom;
    public GameObject GetEndingRoom() { return endingRoom; }

    [SerializeField]
    private List<GameObject> roomPool;
    [SerializeField]
    private List<GameObject> transitionPool;

    private List<GameObject> roomsToGenerate;
    public List<GameObject> generatedRooms;

    private GameObject currentEntryPoint;
    private GameObject currentRoom;
    private GameObject currentTransition;

    void Start()
    {
        
        roomsToGenerate = new List<GameObject>();
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
        if (roomsToGenerate.Count > 0)
        {
            foreach (GameObject room in generatedRooms)
            {
                Destroy(room);
            }
            generatedRooms.Clear();
            foreach (GameObject room in roomsToGenerate)
            {
                Destroy(room);
            }
            roomsToGenerate.Clear();
        }
        for (int i = 0; i < roomsToGenerateCount; i++)
        {
            int selectedRoom = Random.Range(0, roomPool.Count);
            roomsToGenerate.Add(roomPool[selectedRoom]);
        }

    }
    public GameObject GetRandomEntryPoint(RoomData roomData)
    {
        int randomEntry = Random.Range(0, roomData.GetEntryLinkPoints().Count);
        return roomData.GetEntryLinkPoints()[randomEntry];
    }
    private void SpawnRooms()
    {
        currentRoom = roomsToGenerate[0];
 
        Instantiate(currentRoom, Vector3.zero, new Quaternion(), this.transform);
        currentEntryPoint = GetRandomEntryPoint(currentRoom.GetComponent<RoomData>());

        for (int i = 1; i < roomsToGenerate.Count; i++)
        {

            currentRoom = LinkRoom(transitionPool[Random.Range(1,3)]);
            currentEntryPoint = GetRandomEntryPoint(currentRoom.GetComponent<RoomData>());

            currentRoom = LinkRoom(transitionPool[0]);
            currentEntryPoint = GetRandomEntryPoint(currentRoom.GetComponent<RoomData>());

            currentRoom = LinkRoom(roomsToGenerate[i]);
            currentEntryPoint = GetRandomEntryPoint(currentRoom.GetComponent<RoomData>());
        }

        // newTransition.transform.Rotate(newTransition.transform.position, toRotate);
    }

    private GameObject LinkRoom(GameObject roomToAdd)
    {
        if (roomToAdd.GetComponent<RoomData>() == null)
        {
            Debug.Log("GameObject does not have Room Data!");
            return null;
        }
        Vector3 roomOffset = Vector3.zero;
        Vector3 entryOriginRelitive = Vector3.zero;
        int randomEntryLink = Random.Range(0, roomToAdd.GetComponent<RoomData>().GetEntryLinkPoints().Count);
        randomEntryLink = 0;
        GameObject currentExitPoint = roomToAdd.GetComponent<RoomData>().GetEntryLinkPoints()[randomEntryLink];
        float toRotate = 0f;
        if (true)
        {
            float adjustAngle = currentExitPoint.transform.eulerAngles.y;
            toRotate = (currentEntryPoint.transform.eulerAngles.y + 180f) - adjustAngle;
            roomOffset = currentExitPoint.transform.localPosition;

        }

        GameObject newTransition = Instantiate(roomToAdd,
            currentEntryPoint.transform.position - roomOffset, new Quaternion(), this.transform);

        newTransition.transform.RotateAround(currentEntryPoint.transform.position, new Vector3(0, 1, 0), toRotate);
        newTransition.GetComponent<RoomData>().RemoveEntryLinkPoint(randomEntryLink);
        generatedRooms.Add(newTransition);
        return newTransition;
    }
}
