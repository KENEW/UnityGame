using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManage : MonoBehaviour
{
    private ResourceManage resource;

    private GameObject mapParent;

    public GameObject[ , ] map;
    public float[] tileSize;

    private void Awake()
    {
        resource = ResourceManage.instanceRes;

        mapParent = GameObject.Find("MapTiles");
    }
    public void MapSummon(float row, float line, GameObject obj, GameObject tile)
    {
        obj = Instantiate(tile, new Vector3(row, line, 0), Quaternion.identity);
    }

    public void MapLoad(int row, int line, int num)
    {
        map = new GameObject[row, line];
        tileSize = new float[num];

        tileSize[0] = resource.MapTilePre[0].GetComponent<SpriteRenderer>().size.x;//resource.MapTilePre[0].GetComponent<RectTransform>().rect.height;
        tileSize[1] = resource.MapTilePre[0].GetComponent<SpriteRenderer>().size.y;//resource.MapTilePre[1].GetComponent<RectTransform>().rect.width;

        for (int a = 0; a < row; a++)
        {
            for(int b = 0; b < line; b++)
            {
                if ((a + b) % 2 == 1)
                    map[a, b] = Instantiate(resource.MapTilePre[0], new Vector3(b * tileSize[0], a * tileSize[1], 0), Quaternion.identity);
                //MapSummon(b * tileSize[0], a * tileSize[1], map[a, b], resource.MapTilePre[0]);

                if ((a + b) % 2 == 0)
                    map[a, b] = Instantiate(resource.MapTilePre[1], new Vector3(b * tileSize[0], a * tileSize[1], 0), Quaternion.identity);

                map[a, b].transform.parent = mapParent.transform;
            }
        }
        
    }
    private void Start()
    {
        MapLoad(18, 18, 2);
    }
}
