using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public static MiniMap instance;

    public GameObject mapPanel;
    public GameObject mapPoint;
    public GameObject pointConfirm;

    RectTransform mapRectTransform;
    RectTransform pointRectTransform;

    GameObject pointinstance;

    public Vector3 mapSpawnPoint;

    ObjectPool pool;
    GameObject[] pointArray = new GameObject[10];
    int arrayCount;

    void Start()
    {
        instance= this;

        mapRectTransform = mapPanel.GetComponent<RectTransform>();
        pointRectTransform = mapPoint.GetComponent<RectTransform>();

        MapPointCreator();

        pointConfirm.transform.position = pointRectTransform.rect.position;
        float spawnPointX = pointRectTransform.localPosition.x + 3.4f;
        float spawnPointY = pointRectTransform.localPosition.y + 3.4f;
        mapSpawnPoint = new Vector3(spawnPointX, spawnPointY, 0);

    }

    public void MapPointUploader(Collider[] colliderArray, Transform playerTransform)
    {
        if(arrayCount != colliderArray.Length)   // mapPanel içerisinde halihazýrda bir tane child var. Bu child hesaba katýlmasýn diye -1 koyuyoruz.
        {
            for (int k = 0; k < 10; k++)
            {
                if (pointArray[k] != null)
                {
                    //Destroy(pointArray[k].gameObject);
                    ObjectPool.ýnstance.AddQueue("mappointpool", pointArray[k]);
                    pointArray[k] = null;
                }
            }
            
            arrayCount = colliderArray.Length;
            //pointArray = new GameObject[colliderArray.Length];
            for (int j = 0; j < colliderArray.Length; j++)
            {
                if (pointArray[j] == null)
                {
                    //pointArray[j] = Instantiate(mapPoint, mapPanel.transform);
                    pointArray[j] = ObjectPool.ýnstance.RemoveQueue("mappointpool");
                    pointArray[j].transform.localScale = Vector3.one/2;
                    pointArray[j].GetComponent<Image>().color = Color.red;
                }
            }
        }


        for (int i = 0; i < colliderArray.Length; i++)
        {
            pointArray[i].transform.localPosition = (colliderArray[i].transform.position - playerTransform.position) * 10f;
        }
    }

    void MapPointCreator()
    {
        for (int i = 0; i < 10; i++)
        {
            ObjectPool.ýnstance.AddQueue("mappointpool", Instantiate(mapPoint, mapPanel.transform));
        }
    }
}
