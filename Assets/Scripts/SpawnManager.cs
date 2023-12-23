using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject item;

    public int initialEnemiesCount = 8;
    public int initialItemCount = 15;

    private float x_rangeL = -47f;
    private float x_rangeR = 47f;

    private float z_rangeU = -47f;
    private float z_rangeD = +44f;

    private float y_pos = 1f;

    void Start()
    {
        SpawnEnemies();
        SpawnItem();

    }

    private void SpawnEnemies()
    {
        for(int i=0; i< initialEnemiesCount;  i++)
        {
            Vector3 randomPos = RandomPos();
            int randomIndex = Random.Range(0, enemies.Length);

            Instantiate(enemies[randomIndex], randomPos, enemies[randomIndex].transform.rotation);

        }
    }

    private void SpawnItem()
    {
        for(int i=0; i< initialItemCount; i++)
        {
            Vector3 randomPos = RandomPos();

            Instantiate(item, randomPos, item.transform.rotation);
        }
    }


    private Vector3 RandomPos()
    {
        float pos_X = Random.Range(x_rangeL, x_rangeR);
        float pos_Z = Random.Range(z_rangeU, z_rangeD);

        return new Vector3(pos_X, y_pos, pos_Z);
    }

}
