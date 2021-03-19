using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform closePosition;
    public Transform openPosition;
    public Transform door;
    public bool open = false;
    int speed = 5;


    void Start()
    {
        door.position = closePosition.position; 
    }

    public void OpenClose()
    {
        open = !open;   
    }

    private void Update()
    {
        if(open && Vector3.Distance(door.position, openPosition.position) > 0.001f)
        {
            door.position = Vector3.MoveTowards(door.position, openPosition.position, Time.deltaTime * speed);
        }

        if(!open && Vector3.Distance(door.position, closePosition.position) > 0.001f)
        {
            door.position = Vector3.MoveTowards(door.position, closePosition.position, Time.deltaTime * speed);
        }
    }
}
