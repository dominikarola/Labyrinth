using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public Doors[] doors;
    public KeyColor myColor;
    bool iCanOpen = false;
    bool locked = false;
    Animator key;

    AudioClip openClip;
    AudioClip lockClip;

    public Material red;
    public Material green;
    public Material gold;

    public Renderer myLock;

    private void Start()
    {
        key = GetComponent<Animator>();
        SetMyColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            iCanOpen = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = false;
            GameManager.gameManager.SetUseInfo(""); //<-----
        }
    }

    private void Update()
    {
        if(iCanOpen && !locked)
        {
            GameManager.gameManager.SetUseInfo("Press E to open lock"); //<-----
        }

        if (Input.GetKeyDown(KeyCode.E) && iCanOpen && !locked)
        {
            key.SetBool("useKey", CheckTheKey());
            
        }
    }

    public void UseKey()
    {
        foreach(Doors door in doors)
        {
            door.OpenClose();
        }
    }

    public bool CheckTheKey()
    {
        if(GameManager.gameManager.redKey > 0 && myColor == KeyColor.Red)
        {
            GameManager.gameManager.PlayClip(openClip);
            GameManager.gameManager.redKey--;
            GameManager.gameManager.redKeyText.text = GameManager.gameManager.redKey.ToString(); //<-----
            locked = true;
            return true;
        }
        else if (GameManager.gameManager.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.gameManager.PlayClip(openClip);
            GameManager.gameManager.greenKey--;
            GameManager.gameManager.greenKeyText.text = GameManager.gameManager.greenKey.ToString(); //<-----
            locked = true;
            return true;
        }
        else if (GameManager.gameManager.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.gameManager.PlayClip(openClip);
            GameManager.gameManager.goldKey--;
            GameManager.gameManager.goldKeyText.text = GameManager.gameManager.goldKey.ToString(); //<-----
            locked = true;
            return true;
        } else
        {
            GameManager.gameManager.PlayClip(lockClip);
            return false;
        }
    }

    void SetMyColor()
    {
        switch (myColor)
        {
            case KeyColor.Red:
                GetComponent<Renderer>().material = red;
                myLock.material = red; //Tu oczywiście odpowiedni kolor
                break;
            case KeyColor.Green:
                GetComponent<Renderer>().material = green;
                myLock.material = green;
                break;
            case KeyColor.Gold:
                GetComponent<Renderer>().material = gold;
                myLock.material = gold;
                break;
        }
    }

}
