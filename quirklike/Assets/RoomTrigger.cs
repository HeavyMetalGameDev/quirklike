using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorTriggerType { ENTERANCE,EXIT}
public class RoomTrigger : MonoBehaviour
{
    DoorTriggerType _doorType;
    Vector3 playerPositionOnEntry;
    Vector3 playerPositionOnExit;
    float colliderThickness = 0.0f;
    const float thicknessAllowance = 0.6f;

    private void Start()
    {
        colliderThickness = GetComponent<BoxCollider>().size.x;
    }

    public void SetDoorTriggerType(DoorTriggerType type)
    {
        _doorType = type;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return; //we only care about the player here

        playerPositionOnEntry = other.transform.position;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerPositionOnExit = other.transform.position;

        Vector3 playerTravelDistance = playerPositionOnExit - playerPositionOnEntry;
        float distanceDot = Vector3.Dot(playerTravelDistance, transform.right);

        bool intoRoom = false;
        if (distanceDot > 0) //player is on the entry side
        {
            intoRoom = true;
        }

        bool hasGoneThrough = false;
        if (Mathf.Abs(distanceDot) > colliderThickness * thicknessAllowance)
        {
            hasGoneThrough = true;
        }

        if (!hasGoneThrough || !intoRoom) return;

        switch (_doorType)
        {
            case DoorTriggerType.ENTERANCE:
            {
                    Debug.Log("PLAYER HAS ENTERED NEW ROOM!");
                    break;
            }
            case DoorTriggerType.EXIT:
                {
                    Debug.Log("PLAYER HAS EXITED THE ROOM!");

                    break;
                }
        }

    }
}
