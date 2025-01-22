using System;
using UnityEngine;

public class MiniBaseAreaController : MonoBehaviour
{
    public int BaseCount = 3;
    public int CurrentBaseIndex = 0;
    private int ScreenWidth = 1960;
    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.3f;
    public float maxSpeed = 10.0f;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        MoveUpdate();
    }

    public void MoveLeft()
    {
        CurrentBaseIndex--;
        if (CurrentBaseIndex < 0)
        {
            CurrentBaseIndex = 0;
        }
    }
    public void MoveRight()
    {
        CurrentBaseIndex++;
        if (CurrentBaseIndex > BaseCount - 1)
        {
            CurrentBaseIndex = BaseCount - 1;
        }
    }
    
    public void MoveUpdate()
    {
        int countTemp = -CurrentBaseIndex; //因为滑动需要反向移动，所以变为负值
        Vector3 target = new Vector3(countTemp * ScreenWidth, rectTransform.anchoredPosition.y, 0);
        // 使用 SmoothDamp 实现平滑的非线性相机移动
        rectTransform.anchoredPosition = Vector3.SmoothDamp(rectTransform.anchoredPosition, target, ref velocity, smoothTime, maxSpeed);
    }
}
