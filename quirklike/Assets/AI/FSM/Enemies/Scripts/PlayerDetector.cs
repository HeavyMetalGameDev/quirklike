using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour 
{
    public bool PlayerInRange => DetectedPlayer != null;

    private PlayerController DetectedPlayer;
    public bool PlayerInMRange = false;
    private float MeleeRange = 4.0f;

    private void OnTriggerEnter(Collider other)
    {

        if(other.GetComponent<PlayerController>()){
            DetectedPlayer = other.GetComponent<PlayerController>();
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent <PlayerController>()){
            PlayerInMRange = false;
            StartCoroutine(ClearDetectionAfterDelay());
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.GetComponent<PlayerController>()){
            if(DetectedPlayer != null)
            {
                Vector3 dist = this.transform.position - GetPlayerPosition();
                if(dist.magnitude < 4)
                {
                    PlayerInMRange = true;
                } 
            }
        }
    }

    private IEnumerator ClearDetectionAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        DetectedPlayer = null;
    }

    public Vector3 GetPlayerPosition()
    {
        return DetectedPlayer?.transform.position ?? Vector3.zero;
    }

}
