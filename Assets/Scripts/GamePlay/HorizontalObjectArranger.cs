using UnityEngine;

[ExecuteAlways]
public class HorizontalObjectArranger : MonoBehaviour
{
    public GameObject[] objectsToArrange; // 需要排列的物体数组
    public float spacing = 2f; // 物体之间的间距

    void Start()
    {
        ArrangeObjects();
        objectsToArrange = new GameObject[transform.childCount];
    }

    void GetChildObject()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            objectsToArrange[i] = transform.GetChild(i).gameObject;
        }
    }
    void ArrangeObjects()
    {
        float startX = -((objectsToArrange.Length - 1) * spacing) / 2f; // 计算起始X位置
        for (int i = 0; i < objectsToArrange.Length; i++)
        {
            objectsToArrange[i].transform.position = new Vector3(startX + i * spacing,
                objectsToArrange[i].transform.position.y, objectsToArrange[i].transform.position.z);
        }
    }
}