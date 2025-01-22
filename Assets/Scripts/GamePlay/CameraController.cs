using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 lastMousePosition; // 上次鼠标的位置
    private Camera mainCamera; // 主相机

    public float baseDragSpeed = 0.1f; // 基础拖拽速度
    public float zoomSpeed = 4f; // 缩放速度
    public float minZoom = 1f; // 最小缩放限制
    public float maxZoom = 7f; // 最大缩放限制

    void Start()
    {
        // 获取主相机
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 计算相机与 Z 轴 0 点的距离
        float distanceToZ = Mathf.Abs(mainCamera.transform.position.z);

        // 根据 Z 轴的距离动态调整拖拽速度
        float dynamicDragSpeed = baseDragSpeed / distanceToZ; // 距离越近，拖拽速度越小

        // 鼠标中键拖拽相机
        if (Input.GetMouseButton(2)) // 2 是鼠标中键（滚轮键）
        {
            // 获取鼠标当前位置
            Vector3 mousePosition = Input.mousePosition;

            // 计算鼠标移动量
            Vector3 delta = mousePosition - lastMousePosition;

            // 通过平移相机来实现拖拽效果，使用动态拖拽速度
            Vector3 movement = new Vector3(delta.x, delta.y, 0) * dynamicDragSpeed;

            // 更新相机位置
            mainCamera.transform.position -= movement;

            // 更新 lastMousePosition
            lastMousePosition = mousePosition;
        }
        else
        {
            // 鼠标中键没有按下时，记录当前鼠标位置
            lastMousePosition = Input.mousePosition;
        }

        // 鼠标滚轮缩放相机
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            // 对于平行相机，调整 orthographicSize 来实现缩放
            mainCamera.orthographicSize -= scrollInput * zoomSpeed;

            // 限制相机的 orthographicSize
            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize, minZoom, maxZoom);
        }
    }
}