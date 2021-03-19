using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefab[] colorMappings;
    public float offset = 5f;

    public Material material01;
    public Material material02;

 

    public void GenerateLabirynth()
    {
        Debug.Log("Liczba pixeli: szerokość " + map.width + " i wysokość " + map.height + ". Łącznie: " + (map.width * map.height) + " pixeli");

        for(int x = 0; x < map.width; x++)
        {
            for (int z = 0; z < map.height; z++)
            {
                //Debug.Log("Generuje blok o wspórzędnych: x " + x + " i z " + z);
                //Debug.Log("Numer bloku: " + (x * map.width + z + 1) +" z : " + (map.width * map.height));
                GenerateTile(x,z);
            }
        }


        ColorTheChildren();


    }

    void GenerateTile(int x, int z)
    {
        Color pixelColor = map.GetPixel(x, z);

        /*if(pixelColor.a == 0)
        {
            return;
        }*/
        bool pyklo = false;
        foreach(ColorToPrefab colorMapping in colorMappings)
        {
            if(colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x, 0, z) * offset;
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                pyklo = true;
                //Debug.Log("Pykło: x " + x + " z " + z + ". Kolor pixela: R " + pixelColor.r + " G " + pixelColor.g + " B " + pixelColor.b + " A " + pixelColor.a);
                Debug.Log("Pykło: x " + x + " z " + z + ". Kolor pixela: " +  pixelColor.ToString());

            }
        }

        if(!pyklo)
        {
            //Debug.LogWarning("Coś nie pykło: x " + x + " z " + z + ". Kolor pixela: R " + pixelColor.r + " G " + pixelColor.g + " B " + pixelColor.b + " A " + pixelColor.a);
            Debug.LogWarning("Coś nie pykło: x " + x + " z " + z + ". Kolor pixela: " + pixelColor.ToString());
        }
    }

    public void ColorTheChildren()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "Wall")
            {
                if(Random.Range(1,100) % 3 == 0)
                {
                    child.gameObject.GetComponent<Renderer>().material = material02;
                }
                else
                {
                    child.gameObject.GetComponent<Renderer>().material = material01;
                }
                
            }

            //W tym miejscu można dodać kawałek kodu, 
            //który będzie sprawdzał czy ściana ma jeszcze jakieś dzieci i by im też nadała materiały w ten sposób
            //opis poniżej

            if (child.childCount > 0)
            {
                foreach (Transform grandchild in child.transform)
                {
                    if (grandchild.tag == "Wall")
                    {
                        if (Random.Range(1, 100) % 3 == 0)
                        {
                            grandchild.gameObject.GetComponent<Renderer>().material = material02;
                        }
                        else
                        {
                            grandchild.gameObject.GetComponent<Renderer>().material = material01;
                        }
                    }
                }
            }

        }
    }


}
