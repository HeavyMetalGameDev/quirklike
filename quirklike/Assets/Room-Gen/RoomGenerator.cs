using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UIElements;
using static LinkPoint;
using Random = UnityEngine.Random;

public class RoomGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Data")]
    [SerializeField]
    public int roomsToGenerateCount;
    public List<GameObject> generatedRooms;
    public List<GameObject> generatedTransitions;

    [Header("Important Rooms")]
    [SerializeField]
    private GameObject startingRoom;
    public GameObject GetStartingRoom() { return startingRoom; }

    [SerializeField]
    private GameObject endingRoom;
    public GameObject GetEndingRoom() { return endingRoom; }

    [Header("Rooms")]
    [SerializeField]
    private List<GameObject> roomPool;
    [SerializeField]
    private List<float> roomPoolWeights;
    [Header("Transitions")]
    [SerializeField]
    private List<GameObject> transitionPool; 
    [SerializeField]
    private List<float> transitionPoolWeights;

    [Header("Generated Rooms")]
    private List<GameObject> roomsToGenerate;

    private GameObject currentLinkPoint;
    private int currentLinkPointIndex;
    private GameObject currentRoom;
    private int currentGeneratedRoomId = 0;
    private GameObject currentTransition;

    private bool haveRoomsGenerated = false;
    public bool HasRoomsGenerated() {  return haveRoomsGenerated; }

    void Start()
    {
        roomsToGenerate = new List<GameObject>();
        if (roomPool.Count != roomPoolWeights.Count)
        {
            Debug.Log("WARNING: Room Pool and Room Pool Weight Mismatch");
            return;
        }
        if (transitionPool.Count != transitionPoolWeights.Count)
        {
            Debug.Log("WARNING: Transition Pool and Transition Pool Weight Mismatch");
            return;
        }
        GenerateRooms();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void GenerateRooms()
    {
        haveRoomsGenerated = false;
        InitialiseRoomList();
        SpawnRooms();
        haveRoomsGenerated = true;
        Callbacks.CallEvent(CallbackEvent.RoomGenerationComplete);
    }

    private void InitialiseRoomList()
    {
        ClearGameObjectList(generatedRooms);
        ClearGameObjectList(generatedTransitions);
        ClearGameObjectList(roomsToGenerate);
        for (int i = 0; i < roomsToGenerateCount; i++)
        {
            Debug.Log("Generating Room " + i);
            roomsToGenerate.Add(WeightedRoomSelect(roomPool, roomPoolWeights));
        }

    }
    private void ClearGameObjectList(List<GameObject> l)
    {
        if (l.Count > 0)
        {
            foreach (GameObject r in l)
            {
                Destroy(r);
            }
            l.Clear();
        }
    }
    private GameObject WeightedRoomSelect(List<GameObject> r, List<float> w)
    {
        List<GameObject> rooms = new List<GameObject>(r);
        List<float> roomWeights = new List<float>(w);
        // Gotten from here: https://discussions.unity.com/t/random-numbers-with-a-weighted-chance/646590 
        float totalWeight = 0f;
        float workingWeight = 0f;
        for(int i = 0; i < w.Count; i++)
        {
            totalWeight += roomWeights[i];
            if (i < w.Count - 1) workingWeight += roomWeights[i];
        }
        int index = roomWeights.Count - 1;
        while (index >= 0)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (Random.Range(0, totalWeight) > workingWeight)
            {
                Debug.Log(index);
                return rooms[index];
            }

            // Remove the last item from the sum of total untested weights and try again.
            totalWeight -= roomWeights[index];
            index--;
            workingWeight -= roomWeights[index];
        }
        Debug.Log("Something Fucked Up");
        return null;
    }
    public GameObject GetRandomLinkPoints(RoomData roomData)
    {
        int randomEntry = Random.Range(0, roomData.GetLinkPoints().Count);
        return roomData.GetLinkPoints()[randomEntry];
    }

    public GameObject GetRandomLinkPoints(RoomData roomData, LinkType linkType)
    {
        List<int> availablePoints = new List<int>();
        for (int i = 0; i < roomData.GetLinkPoints().Count; i++)
        {
            LinkType lt = roomData.GetLinkPoints()[i].GetComponent<LinkPoint>().GetLinkType();
            if (lt == linkType || lt == LinkType.NoPriority)
            {
                availablePoints.Add(i);
            }
        }
        return roomData.GetLinkPoints()[availablePoints[Random.Range(0, availablePoints.Count)]];
    }

    public int GetRandomLinkPointIndex(RoomData roomData, LinkType linkType)
    {
        List<int> availablePoints = new List<int>();
        for (int i = 0; i < roomData.GetLinkPoints().Count; i++)
        {
            LinkType lt = roomData.GetLinkPoints()[i].GetComponent<LinkPoint>().GetLinkType();
            if (lt == linkType || lt == LinkType.NoPriority)
            {
                availablePoints.Add(i);
            }
        }
        Debug.Log(availablePoints.Count);
        return availablePoints[Random.Range(0, availablePoints.Count)];
    }
    private void SpawnRooms()
    {
        currentRoom = roomsToGenerate[0]; //this is a prefab, not clear
        startingRoom = Instantiate(currentRoom, Vector3.zero, new Quaternion(), this.transform);
        currentRoom = startingRoom;
        generatedRooms.Add(currentRoom);

        currentLinkPointIndex = GetRandomLinkPointIndex(currentRoom.GetComponent<RoomData>(), LinkType.ExitOnly); //gets an exit
        currentLinkPoint = currentRoom.GetComponent<RoomData>().GetLinkPoints()[currentLinkPointIndex];

        for (int i = 1; i < roomsToGenerate.Count; i++)
        {

            currentRoom = LinkRoom(WeightedRoomSelect(transitionPool, transitionPoolWeights));
            currentLinkPointIndex = GetRandomLinkPointIndex(currentRoom.GetComponent<RoomData>(), LinkType.ExitOnly); //gets an exit
            currentLinkPoint = currentRoom.GetComponent<RoomData>().GetLinkPoints()[currentLinkPointIndex];


            //currentRoom = LinkRoom(transitionPool[0]);
            //currentLinkPoint = GetRandomLinkPoints(currentRoom.GetComponent<RoomData>(), LinkType.ExitOnly);

            currentRoom = LinkRoom(roomsToGenerate[i]);
            currentLinkPointIndex = GetRandomLinkPointIndex(currentRoom.GetComponent<RoomData>(), LinkType.ExitOnly); //gets an exit
            currentLinkPoint = currentRoom.GetComponent<RoomData>().GetLinkPoints()[currentLinkPointIndex];
        }
        endingRoom = currentRoom;
        endingRoom.GetComponent<RoomData>().SetRoomID(currentGeneratedRoomId);
        DisableAllObjects(ref endingRoom.GetComponent<RoomData>().GetLinkPoints()); //keep this for now. Later, we will need an exit
        // newTransition.transform.Rotate(newTransition.transform.position, toRotate);
    }

    private GameObject LinkRoom(GameObject roomToAdd)
    {
        Debug.Log(roomToAdd.activeInHierarchy);
        if (roomToAdd.GetComponent<RoomData>() == null)
        {
            Debug.Log("GameObject does not have Room Data!");
            return null;
        }
        Vector3 roomOffset = Vector3.zero;
        Vector3 entryOriginRelitive = Vector3.zero;
        int randomEntryLink = GetRandomLinkPointIndex(roomToAdd.GetComponent<RoomData>(), LinkType.EntryOnly);
        GameObject currentEntryPoint = roomToAdd.GetComponent<RoomData>().GetLinkPoints()[randomEntryLink];

        float toRotate = 0f;
        if (true)
        {
            float adjustAngle = currentEntryPoint.transform.eulerAngles.y;
            toRotate = (currentLinkPoint.transform.eulerAngles.y + 180f) - adjustAngle;
            roomOffset = currentEntryPoint.transform.localPosition;

        }

        GameObject newCreatedRoom = Instantiate(roomToAdd,
            currentLinkPoint.transform.position - roomOffset, new Quaternion(), this.transform);

        currentRoom.GetComponent<RoomData>().SetRoomID(currentGeneratedRoomId);
        AssignLinkAsExit(currentLinkPoint);
        AssignLinkTriggerID(currentLinkPoint);
        AssignLinkTriggerToRoomManager(currentLinkPoint, currentRoom);

        currentGeneratedRoomId++;

        GameObject newLinkPoint = newCreatedRoom.GetComponent<RoomData>().GetLinkPoints()[randomEntryLink];
        AssignLinkAsEntry(newLinkPoint);
        AssignLinkTriggerID(newLinkPoint);
        AssignLinkTriggerToRoomManager(newLinkPoint, newCreatedRoom);


        currentRoom.GetComponent<RoomData>().RemoveEntryLinkPoint(currentLinkPointIndex);
        DisableAllObjects(ref currentRoom.GetComponent<RoomData>().GetLinkPoints());

        newCreatedRoom.transform.RotateAround(currentLinkPoint.transform.position, new Vector3(0, 1, 0), toRotate);
        newCreatedRoom.GetComponent<RoomData>().RemoveEntryLinkPoint(randomEntryLink);
        if(newCreatedRoom.GetComponent<RoomData>().GetRoomType() == RoomData.RoomType.MainRoom) generatedRooms.Add(newCreatedRoom);
        else generatedTransitions.Add(newCreatedRoom);
        return newCreatedRoom;
    }

    void AssignLinkTriggerToRoomManager(GameObject link, GameObject room )
    {
        var roomManager = room.GetComponent<RoomManager>();
        var linkTrigger = link.GetComponentInChildren<RoomTrigger>();

        roomManager.AssignTrigger(linkTrigger);
    }

    void AssignLinkAsEntry(GameObject link)
    {
        RoomTrigger trigger = link.GetComponentInChildren<RoomTrigger>();
        if (trigger == null)
        {
            Debug.LogError(link);
            Debug.LogError("DOOR IS MISSING TRIGGER");
            return;
        }
        trigger.SetDoorTriggerType(DoorTriggerType.ENTERANCE);
    }
    void AssignLinkAsExit(GameObject link)    {
        RoomTrigger trigger = link.GetComponentInChildren<RoomTrigger>();
        if (trigger == null)
        {
            Debug.LogError(link);

            Debug.LogError("DOOR IS MISSING TRIGGER");
            return;
        }
        Debug.Log(link.name + " IS EXIT ASSIGNED");
        trigger.SetDoorTriggerType(DoorTriggerType.EXIT);
    }

    void AssignLinkTriggerID(GameObject link)
    {
        RoomTrigger trigger = link.GetComponentInChildren<RoomTrigger>();
        if (trigger == null)
        {
            Debug.LogError(link);

            Debug.LogError("DOOR IS MISSING TRIGGER");
            return;
        }
        trigger.SetTriggerRoomID(currentGeneratedRoomId);
    }

    void DisableAllObjects(ref List<GameObject> objs)
    {
        foreach (GameObject obj in objs)
        {
            obj.SetActive(false);
        }
    }
}
