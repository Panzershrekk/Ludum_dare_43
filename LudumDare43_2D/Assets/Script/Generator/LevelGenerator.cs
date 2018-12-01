using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public Texture2D map;
    public ColorToPrefab[] colorMapping;
    //public List<DataTile> mapInfo = new List<DataTile>();

    // Use this for initialization
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        int i = 0;

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y, i);
                i++;
            }
        }
    }

    void GenerateTile(int x, int y, int i)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            //Transparency
            return;
        }
        foreach (ColorToPrefab color in colorMapping)
        {
            if (color.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x, y);
                GameObject obj;
                if (color.color == Color.red)
                    obj = Instantiate(color.prefab, position + new Vector3(0, 0, -0.1f), Quaternion.identity, transform);
                else if (color.color == Color.blue)
                {
                    obj = Instantiate(color.prefab, position, Quaternion.identity, transform);
                    GameObject.FindGameObjectWithTag("Player").transform.position = obj.transform.position + new Vector3(0, 0, -0.1f);
                }
                else
                    obj = Instantiate(color.prefab, position + new Vector3(0, 0, 0.1f), Quaternion.identity, transform);
                //mapInfo.Add(new DataTile(x, y, i, obj, obj));
            }
        }
    }
}