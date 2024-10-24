using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StateUI : MonoBehaviour
{
    public TMP_Text curState; 
    private Wanderer wand;
    
    void Start()
    {
        wand = this.GetComponentInParent<Wanderer>();
        
    }

    private void Update()
    {
       curState.text = wand.getStateName();
    }
    

}
