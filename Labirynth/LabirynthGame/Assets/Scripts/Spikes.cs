using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{

    public float force;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            Vector3 recoil = transform.parent.InverseTransformDirection(transform.right) * Random.Range(-0.1f,0.1f) + transform.parent.InverseTransformDirection(transform.forward) * -1f;
            other.GetComponent<CharacterController>().Move(recoil * Time.deltaTime * force);
        }
    }
}
