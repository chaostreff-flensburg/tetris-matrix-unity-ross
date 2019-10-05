using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PixelGrid : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject prefab;
    public BrushInfo brushInfo;

    public GameObject[] pixels = new GameObject[384];

    void Start()
    {
        var down = false;
        var count = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (down)
                {
                    pixels[count] = Instantiate(prefab, new Vector3(i - width / 2, j - height / 2, 0),
                        Quaternion.identity);
                }
                else
                {
                    pixels[count] = Instantiate(prefab, new Vector3(i - width / 2, height - 1 - j - height / 2, 0),
                        Quaternion.identity);
                }

                pixels[count].GetComponent<PixelInfo>().brushInfo = brushInfo;
                pixels[count].transform.parent = gameObject.transform;
                count++;
            }

            down = !down;
        }

        transform.localScale = new Vector3(0.3f, 0.3f, 1);
    }
}