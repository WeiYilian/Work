using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class CameraController : MonoBehaviour
{
    public float Pitch { get; private set; }//绕y轴摄像机旋转数值
    public float Yaw { get; private set; }//绕x轴摄像机旋转数值

    public float mouseSensitivity = 5;

    public float cameraYSpeed = 5;

    public float limit = 30f;
    
    private Transform _target;
    private Transform _camera;

    [SerializeField] private AnimationCurve armlengthCurve;

    private void Awake()
    {
        _camera = transform.GetChild(0);//0是第一个子物体
    }

    public void InitCamera(Transform target)
    {
        _target = target;
        transform.position = target.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateRotation();
        UpdatePosition();
        UpdateArmLength();
    }

    /// <summary>
    /// 摄像机围绕物体旋转
    /// </summary>
    private void UpdateRotation()
    {
        Yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        Pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        Pitch = Mathf.Clamp(Pitch, -limit, limit);

        transform.rotation = Quaternion.Euler(Pitch, Yaw, 0);
    }

    /// <summary>
    /// 摄像机跟随，水平方向严格跟随，竖直方向缓缓跟随(插值)
    /// </summary>
    private void UpdatePosition()
    {
        Vector3 position = _target.position;
        float newY = Mathf.Lerp(transform.position.y, position.y, Time.deltaTime * cameraYSpeed);
        transform.position = new Vector3(position.x, newY, position.z);
    }

    /// <summary>
    /// 更改摄像机在不同视角时的距离
    /// </summary>
    private void UpdateArmLength()
    {
        _camera.localPosition = new Vector3(0, 0, armlengthCurve.Evaluate(Pitch) * -1);
    }
}
