using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Lobby : MonoBehaviour
{
    [SerializeField] float m_followSpeed;
    [SerializeField] Vector3 m_cameraOffset;
    [SerializeField] Vector2 m_horizontalRange;
    [Space]
    [SerializeField] Camera m_camera;
    [SerializeField] Transform m_followingTarget;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void Update()
    {
        var currentFramePos = Vector3.Lerp(transform.position, m_followingTarget.position + m_cameraOffset, m_followSpeed * Time.deltaTime);
        currentFramePos.x = Mathf.Clamp(currentFramePos.x, m_horizontalRange.x + m_camera.orthographicSize / 2f, m_horizontalRange.y - m_camera.orthographicSize / 2f);

        transform.position = currentFramePos;
    }
}
