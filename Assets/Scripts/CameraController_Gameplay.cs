using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController_Gameplay : MonoBehaviour
{
    public enum State
    {
        ZoomIn = 5,
        ZoomOut = 15
    }
    private static CameraController_Gameplay m_instance;
    public static CameraController_Gameplay Instance => m_instance;

    [SerializeField] Camera m_camera;
    [SerializeField] Image m_fadePanel;
    [SerializeField] Vector3 m_cameraOffset;
    [SerializeField] float m_followSpeed = 5f;
    [SerializeField] float m_zoomInRatio = 5f;
    [SerializeField] float m_zoomOutRatio = 15f;

    private bool m_isFollowingTarget;
    private Transform m_followingTarget;

    public Camera Camera => m_camera;

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

        var currentFramePos = Vector3.Lerp(transform.position, m_followingTarget.position + m_cameraOffset, m_followSpeed * Time.deltaTime);
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
    private IEnumerator IE_FadingCamera(bool isFadeOut, float duration, System.Action callback = null)
    {
        m_fadePanel.gameObject.SetActive(true);

        float t = 0;
        while (t < duration)
        {
            var alphaRatio = (isFadeOut) ? Mathf.Lerp(1f, 0f, t / duration) : Mathf.Lerp(0f, 1f, t / duration);
            m_fadePanel.color = new Color(m_fadePanel.color.r, m_fadePanel.color.g, m_fadePanel.color.b, alphaRatio);
            t += Time.deltaTime;
            yield return null;
        }

        callback?.Invoke();
        m_fadePanel.gameObject.SetActive(false);
    }

    public void FocusOnTarget(Vector3 target, State state, float duration = 1f, System.Action callback = null)
    {
        m_isFollowingTarget = false;
        switch (state)
        {
            case State.ZoomIn:
                {
                    StartCoroutine(IE_Zooming(state, duration));
                    StartCoroutine(Utilities.IE_WorldTranslate(transform, 
                                                               transform.position, 
                                                               target + m_cameraOffset, 
                                                               duration, 
                                                               callback
                                                               ));
                }
                break;

            case State.ZoomOut:
                {
                    StartCoroutine(IE_Zooming(state, duration));
                    StartCoroutine(Utilities.IE_WorldTranslate(transform, 
                                                               transform.position, 
                                                               target + m_cameraOffset, 
                                                               duration, 
                                                               callback
                                                               ));
                }
                break;
        }
    }
    public void FadingCameraScreen(bool isFadeOut, float duration = 1f, System.Action callback = null)
    {
        StartCoroutine(IE_FadingCamera(isFadeOut, duration, callback));
    }
    public void AssignFollowingTarget(Transform target, float duration = 2f, System.Action callback = null)
    {
        m_followingTarget = target;
        FocusOnTarget(target.position, State.ZoomIn, duration, () =>
                                                               {
                                                                   m_isFollowingTarget = true;
                                                                   callback?.Invoke();
                                                               });
    }
}
