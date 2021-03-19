using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlace : MonoBehaviour
{
    float alfa = 0;
    void FixedUpdate()
    {
        float scale = Resizer();
        transform.localScale = new Vector3(scale, 4.5f, scale);
    }

    public float Resizer()
    {
        
        float value = Mathf.Sin(alfa);
        alfa += (1.5f * Time.deltaTime);
        return value + 2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.gameManager.WinGame();
        }
    }
}
