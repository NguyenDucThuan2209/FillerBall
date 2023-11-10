using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController_Lobby : MonoBehaviour
{
    [SerializeField] float m_followSpeed;
    [SerializeField] Vector3 m_cameraOffset;
    [SerializeField] Vector2 m_horizontalRange;
    [SerializeField] Vector3 m_startPosition;
    [SerializeField] Vector2 m_zoomRange;
    [Space]
    [SerializeField] Camera m_camera;
    [SerializeField] Transform m_followingTarget;

    private bool m_isTransiting;

    private void Update()
    {
        if (m_isTransiting) return;

        var currentFramePos = Vector3.Lerp(transform.position, m_followingTarget.position + m_cameraOffset, m_followSpeed * Time.deltaTime);
        currentFramePos.x = Mathf.Clamp(currentFramePos.x, m_horizontalRange.x + m_camera.orthographicSize / 2f, m_horizontalRange.y - m_camera.orthographicSize / 2f);

        transform.position = currentFramePos;
    }
    private IEnumerator IE_ZoomIntoMap(Vector3 focusPoint, float duration, System.Action callback = null)
    {
        float t = 0;
        while (t < duration)
        {
            var ratio = Mathf.Lerp(m_zoomRange.x, m_zoomRange.y, t / duration);
            var position = Vector3.Lerp(m_startPosition, focusPoint + m_cameraOffset, t / duration);

            m_camera.orthographicSize = ratio;
            transform.position = position;
            t += Time.deltaTime;
            yield return null;
        }
        callback?.Invoke();
    }

    public void ZoomIntoMap(Vector3 focusPoint, float duration = 2f)
    {
        StartCoroutine(IE_ZoomIntoMap(focusPoint, duration));
    }
    public void SetupCameraForMap(Vector2 horizontalRange)
    {
        m_horizontalRange = horizontalRange;
    }
}
