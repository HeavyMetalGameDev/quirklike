using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorTriggerType { ENTERANCE,EXIT}
public class RoomTrigger : MonoBehaviour
{
    public DoorTriggerType _doorType = DoorTriggerType.ENTERANCE;
    Vector3 playerPositionOnEntry;
    Vector3 playerPositionOnExit;
    float colliderThickness = 0.0f;
    const float thicknessAllowance = 0.6f;

    private void Awake()
    {
        _doorType = DoorTriggerType.ENTERANCE; //we need to do this for some reason???
    }
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

        if (!hasGoneThrough) return;

        switch (_doorType)
        {
            case DoorTriggerType.ENTERANCE:
            {
                    if (!intoRoom) return;
                    Callbacks.CallEvent(CallbackEvent.RoomEntered);
                    break;
            }
            case DoorTriggerType.EXIT:
            {
                    if (intoRoom) return;
                    Callbacks.CallEvent(CallbackEvent.RoomExited);
                    break;
            }
        }

    }
}
