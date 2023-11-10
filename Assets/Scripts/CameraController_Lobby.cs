using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController_Lobby : MonoBehaviour
{
    [SerializeField] float m_followSpeed;
    [SerializeField] Vector2 m_zoomRange;
    [SerializeField] Vector3 m_cameraOffset;
    [SerializeField] Vector2 m_horizontalRange;
    [Space]
    [SerializeField] Camera m_camera;
    [SerializeField] Image m_cloudImage;
    [SerializeField] Image[] m_planeImage;
    [SerializeField] Transform m_followingTarget;

    private bool m_isTransiting = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (m_isTransiting) return;

        var currentFramePos = Vector3.Lerp(transform.position, m_followingTarget.position + m_cameraOffset, m_followSpeed * Time.deltaTime);
        currentFramePos.x = Mathf.Clamp(currentFramePos.x, m_horizontalRange.x + m_camera.orthographicSize / 2f, m_horizontalRange.y - m_camera.orthographicSize / 2f);

        transform.position = currentFramePos;
    }

    private IEnumerator IE_TransitionToMap(Map map, 
                                           System.Action onPlaneShowUp, 
                                           System.Action onPlaneHideout, 
                                           float cloudDuration = 1f, 
                                           float planeDuration = 1f)
    {
        float t = 0;
        m_cloudImage.gameObject.SetActive(true);
        while (t < cloudDuration)
        {
            m_cloudImage.rectTransform.anchoredPosition = Vector2.Lerp(new Vector2(0, -3500f), Vector2.zero, t / cloudDuration);
            t += Time.deltaTime;
            yield return null;
        }

        t = 0f;
        byte index = (byte)map;
        m_planeImage[index].gameObject.SetActive(true);
        while (t < planeDuration)
        {
            m_planeImage[index].rectTransform.anchoredPosition = Vector2.Lerp(new Vector2(0, -3500f), Vector2.zero, t / cloudDuration);
            t += Time.deltaTime;
            yield return null;
        }

        onPlaneShowUp?.Invoke();

        t = 0f;
        while (t < planeDuration)
        {
            m_planeImage[index].rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, new Vector2(0, 3500f), t / cloudDuration);
            m_cloudImage.rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, new Vector2(0, 3500f), t / cloudDuration);
            t += Time.deltaTime;
            yield return null;
        }

        onPlaneHideout?.Invoke();

        //t = 0f;
        //while (t < cloudDuration / 2f)
        //{
        //    m_cloudImage.rectTransform.anchoredPosition = Vector2.Lerp(Vector2.zero, new Vector2(0, 3500f), t / (cloudDuration / 2f));
        //    t += Time.deltaTime;
        //    yield return null;
        //}
    }
    private IEnumerator IE_ZoomIntoMap(float duration, System.Action callback = null)
    {
        float t = 0;
        while (t < duration)
        {
            var ratio = Mathf.Lerp(m_zoomRange.x, m_zoomRange.y, t / duration);

            m_camera.orthographicSize = ratio;
            t += Time.deltaTime;
            yield return null;
        }
        callback?.Invoke();
    }

    public void FocusOnPoint(Vector3 point)
    {
        transform.position = point + m_cameraOffset;
    }
    public void ZoomIntoMap(float duration = 1f)
    {
        m_isTransiting = true;
        StartCoroutine(IE_ZoomIntoMap(duration, () => m_isTransiting = false));
    }
    public void SetupCameraForMap(Vector2 horizontalRange)
    {
        m_horizontalRange = horizontalRange;
    }
    public void TransitionToMap(Map map, System.Action onPlaneShowUp, float cloudDuration = 1f, float planeDuration = 1f)
    {
        StartCoroutine(IE_TransitionToMap(map, onPlaneShowUp, null, cloudDuration, planeDuration));
    }
}
