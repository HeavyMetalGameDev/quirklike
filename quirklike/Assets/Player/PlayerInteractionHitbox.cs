using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InteractedObject
{
    public GameObject objectToInteractWith;
    public InteractableTypeEnum objectToInteractWithType;
    public float interactionDistance; //how far the object is from the interaction hitbox
}

public class PlayerInteractionHitbox : MonoBehaviour

    //needs code to align with the camera's movement
{
    [SerializeField] PlayerWeaponController _weaponController;
    private PlayerInputManager _playerInputManager;
    InteractedObject _interactedObject;

    bool _isInteractingThisFrame = false;

    private void Start()
    {
        _interactedObject.interactionDistance = Mathf.Infinity;
        _playerInputManager = PlayerInputManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        float interactionDistance = (transform.position - other.transform.position).magnitude;
        if (interactionDistance > _interactedObject.interactionDistance) //we do this so we can only interact with the closest object per frame
        {
            return;
        }
        _interactedObject.objectToInteractWith = other.gameObject;
        _interactedObject.interactionDistance = interactionDistance; // this will work better with small objects

        if (other.gameObject.CompareTag("Weapon"))
        {
            _interactedObject.objectToInteractWithType = InteractableTypeEnum.WEAPON;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == _interactedObject.objectToInteractWith)
        {
            ResetInteractedObject();
        }
    }

    void ResetInteractedObject()
    {
        _interactedObject.objectToInteractWith = null;
        _interactedObject.interactionDistance = Mathf.Infinity;
        _interactedObject.objectToInteractWithType = InteractableTypeEnum.NONE;
    }

    private void Update()
    {
        _isInteractingThisFrame = _playerInputManager.PlayerInteractionPress();

        if (_isInteractingThisFrame && _interactedObject.objectToInteractWith!=null)
        {
            _isInteractingThisFrame = false;

            switch (_interactedObject.objectToInteractWithType)
            {
                case InteractableTypeEnum.WEAPON:
                    {
                        WeaponBase weaponToPickUp = _interactedObject.objectToInteractWith.GetComponentInParent<WeaponBase>(); //since the hitbox is a child of the weapon
                        if(weaponToPickUp)_weaponController.AttachWeapon(weaponToPickUp);
                        else
                        {
                            Debug.LogError(_interactedObject.objectToInteractWith + " IS NOT A VALID WEAPON!");
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            ResetInteractedObject(); //important!
        }
    }
}
