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
            int selectedRoom = Random.Range(0, roomPool.Count);
            generatedRooms.Add(roomPool[selectedRoom]);
        }

    }
    public GameObject GetRandomEntryPoint(RoomData roomData)
    {
        int randomEntry = Random.Range(0, roomData.GetEntryLinkPoints().Count);
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

        Vector3 entryOriginRelitive = Vector3.zero;
        int randomEntryLink = Random.Range(0, transitionRoomData.GetEntryLinkPoints().Count);
        randomEntryLink = 1;
        GameObject currentExitPoint = transitionRoomData.GetEntryLinkPoints()[randomEntryLink];
        float toRotate = 0f;
        if (true)
        {
            float adjustAngle = currentExitPoint.transform.localEulerAngles.y;
            toRotate = (currentEntryPoint.transform.localEulerAngles.y + 180f) - adjustAngle;
            
            roomOffset = currentExitPoint.transform.localPosition;
            requiresRotation = false;
               
            
        }
        Debug.Log(currentEntryPoint.transform.localPosition);
        Debug.Log(roomOffset);
        GameObject newTransition = Instantiate(currentTransition,
            currentEntryPoint.transform.localPosition - roomOffset, new Quaternion(), this.transform);
        newTransition.transform.RotateAround(currentEntryPoint.transform.localPosition, new Vector3(0, 1, 0), toRotate);
       // newTransition.transform.Rotate(newTransition.transform.position, toRotate);
    }
}
