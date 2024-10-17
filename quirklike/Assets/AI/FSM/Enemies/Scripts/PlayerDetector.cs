using System.Collections;
using UnityEngine;

public class PlayerDetector : MonoBehaviour 
{
    public bool PlayerInRange => DetectedPlayer != null;

    private PlayerController DetectedPlayer;

    private void OnTriggerEnter(Collider other){

        if(other.GetComponent<PlayerController>()){
            DetectedPlayer = other.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.GetComponent <PlayerController>()){
            StartCoroutine(ClearDetectionAfterDelay());
        }
    }

    private IEnumerator ClearDetectionAfterDelay(){
        yield return new WaitForSeconds(10f);
        DetectedPlayer = null;
    }

    public Vector3 GetPlayerPosition(){
        return DetectedPlayer?.transform.position ?? Vector3.zero;
    }
}
