using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 따라갈 대상
    public Vector3 offset; // 카메라의 오프셋

    // X, Z 축 이동 제한 범위
    public Vector2 xBounds; // X축 최소, 최대 값
    public Vector2 zBounds; // Z축 최소, 최대 값

    public float fixedY; // 고정된 Y 위치

    void Update()
    {
        // 타겟의 위치와 오프셋을 적용
        Vector3 desiredPosition = target.position + offset;

        // X, Z 위치를 제한
        float clampedX = Mathf.Clamp(desiredPosition.x, xBounds.x, xBounds.y);
        float clampedZ = Mathf.Clamp(desiredPosition.z, zBounds.x, zBounds.y);

        // 제한된 X, Z와 고정된 Y 위치를 적용
        transform.position = new Vector3(clampedX, fixedY, clampedZ);
    }
}