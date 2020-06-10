using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCandy : MonoBehaviour
{
    public GameObject[] CandyNum = new GameObject[6];
    private int cool;
    private float posX, posY;
    void Start()
    {
        CandyNum[0] = Resources.Load("Prefab/Candy/Candy1") as GameObject;
        CandyNum[1] = Resources.Load("Prefab/Candy/Candy2") as GameObject;
        CandyNum[2] = Resources.Load("Prefab/Candy/Candy3") as GameObject;
        CandyNum[3] = Resources.Load("Prefab/Candy/Candy4") as GameObject;
        CandyNum[4] = Resources.Load("Prefab/Candy/Candy5") as GameObject;
        CandyNum[5] = Resources.Load("Prefab/Candy/Candy6") as GameObject;


        StartCoroutine("RandomCool");
        cool = 0;
        for (int j = 0; j < 5; j++)
            for (int i = 0; i < 6; i++)
            {
                posX = Random.Range(0.0f, 25.0f);
                posY = Random.Range(0.0f, -25.0f);
                Instantiate(CandyNum[i], new Vector2(posX, posY), Quaternion.identity);
            }
    }

    IEnumerator RandomCool()
    {
        while (true)
        {
            cool += 1;
            yield return new WaitForSeconds(1f);
           
            if (cool % 60 == 0)
            {
                for(int j = 0; j < 2; j++)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        posX = Random.Range(0.0f, 25.0f);
                        posY = Random.Range(0.0f, -25.0f);

                        Instantiate(CandyNum[i], new Vector2(posX, posY), Quaternion.identity);

                    }
                }
               

            }
        }
    }
}
