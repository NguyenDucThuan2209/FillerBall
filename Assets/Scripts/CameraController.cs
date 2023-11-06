using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum State
    {
        ZoomIn = 5,
        ZoomOut = 15
    }
    private static CameraController m_instance;
    public static CameraController Instance => m_instance;

    [SerializeField] Camera m_camera;
    [SerializeField] Vector3 m_offset;
    [SerializeField] float m_followSpeed = 3f;
    [SerializeField] float m_zoomInRatio = 5f;
    [SerializeField] float m_zoomOutRatio = 15f;

    private State m_currentState;
    private bool m_isFollowingTarget;
    private Transform m_followingTarget;

    private void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        m_instance = this;
    }
    private void Update()
    {
        if (!m_isFollowingTarget) return;

        var currentFramePos = Vector3.Lerp(transform.position, m_followingTarget.position + m_offset, m_followSpeed * Time.deltaTime);
        transform.position = currentFramePos;

        m_camera.orthographicSize = m_zoomInRatio;
    }

    private IEnumerator IE_Zooming(State state, float duration, System.Action callback = null)
    {
        float t = 0;
        while (t < duration)
        {
            var ratio = (state == State.ZoomIn) ? 
                        Mathf.Lerp(m_zoomOutRatio, m_zoomInRatio, t / duration):
                        Mathf.Lerp(m_zoomInRatio, m_zoomOutRatio, t / duration);
            m_camera.orthographicSize = ratio;
            t += Time.deltaTime;
            yield return null;
        }
        callback?.Invoke();
    }

    public void FocusOnTarget(Vector3 position, State state)
    {
        m_isFollowingTarget = false;
        switch (state)
        {
            case State.ZoomIn:
                {

                }
                break;

            case State.ZoomOut:
                {

                }
                break;
        }
    }
    public void AssignFollowingTarget(Transform target)
    {
        m_followingTarget = target;
        m_isFollowingTarget = true;
    }
}
