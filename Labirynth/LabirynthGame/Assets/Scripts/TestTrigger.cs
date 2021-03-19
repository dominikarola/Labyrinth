using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player Enter to me!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        else
        {
            Debug.Log("Somethin Enter!!!");
            Debug.Log(other.gameObject.name);
        }
        
        
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, 5f);
    }
}
