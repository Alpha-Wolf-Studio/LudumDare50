using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<float> cameraSizeByLevel;
    [SerializeField] private Camera cameraMain;

    private IEnumerator IEnumeratorChangeSize;

    public void SetNewLevel(int level) 
    {
        if (IEnumeratorChangeSize != null) StopCoroutine(IEnumeratorChangeSize);
        IEnumeratorChangeSize = CoroutineChangeSize(level);
        StartCoroutine(IEnumeratorChangeSize);
    }

    private IEnumerator CoroutineChangeSize(int level) 
    {
        float t = 0;
        float startingSize = cameraMain.orthographicSize;
        while (t < 1) 
        {
            cameraMain.orthographicSize = Mathf.Lerp(startingSize, cameraSizeByLevel[level], t);
            t += Time.deltaTime;
            yield return null;
        }
    }

}
