using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : PickUp
{
    public int points = 5;
    public override void Picked()
    {
        GameManager.gameManager.AddPoints(points);
        GameManager.gameManager.PlayClip(pickClip);
        Destroy(this.gameObject);
    }

    void Update()
    {
        Rotation();
    }
}
