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
    private GameObject currentRoom;
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
            Debug.Log("Generating Room " + i);
            roomsToGenerate.Add(WeightedRoomSelect(roomPool, roomPoolWeights));
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
        return availablePoints[Random.Range(0, availablePoints.Count)];
    }
    private void SpawnRooms()
    {
        currentRoom = roomsToGenerate[0];

        startingRoom = Instantiate(currentRoom, Vector3.zero, new Quaternion(), this.transform);
        currentLinkPoint = GetRandomLinkPoints(currentRoom.GetComponent<RoomData>(), LinkType.ExitOnly);

        for (int i = 1; i < roomsToGenerate.Count; i++)
        {

            currentRoom = LinkRoom(WeightedRoomSelect(transitionPool, transitionPoolWeights));
            currentLinkPoint = GetRandomLinkPoints(currentRoom.GetComponent<RoomData>(), LinkType.ExitOnly);

            //currentRoom = LinkRoom(transitionPool[0]);
            //currentLinkPoint = GetRandomLinkPoints(currentRoom.GetComponent<RoomData>(), LinkType.ExitOnly);

            currentRoom = LinkRoom(roomsToGenerate[i]);
            currentLinkPoint = GetRandomLinkPoints(currentRoom.GetComponent<RoomData>(), LinkType.ExitOnly);
        }
        endingRoom = currentRoom;
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
        int randomEntryLink = GetRandomLinkPointIndex(roomToAdd.GetComponent<RoomData>(), LinkType.EntryOnly);
        GameObject currentEntryPoint = roomToAdd.GetComponent<RoomData>().GetLinkPoints()[randomEntryLink];
        float toRotate = 0f;
        if (true)
        {
            float adjustAngle = currentEntryPoint.transform.eulerAngles.y;
            toRotate = (currentLinkPoint.transform.eulerAngles.y + 180f) - adjustAngle;
            roomOffset = currentEntryPoint.transform.localPosition;

        }

        GameObject newTransition = Instantiate(roomToAdd,
            currentLinkPoint.transform.position - roomOffset, new Quaternion(), this.transform);

        newTransition.transform.RotateAround(currentLinkPoint.transform.position, new Vector3(0, 1, 0), toRotate);
        newTransition.GetComponent<RoomData>().RemoveEntryLinkPoint(randomEntryLink);
        generatedRooms.Add(newTransition);
        return newTransition;
    }
}
