using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<CameraConfiguration> camerasConfigurations;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private float startSize = 0f;

    [System.Serializable]
    class CameraConfiguration 
    {
        [SerializeField][HideInInspector] private string name;
        public void SetName(string name) => this.name = name;

        public float sizeByLevel;
    }

    private IEnumerator IEnumeratorChangeSize;

    private void Start()
    {
        cameraMain.orthographicSize = startSize;
    }

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
            cameraMain.orthographicSize = Mathf.Lerp(startingSize, camerasConfigurations[level].sizeByLevel, t);
            t += Time.deltaTime;
            yield return null;
        }
    }


    private void OnValidate()
    {
        for (int i = 0; i < camerasConfigurations.Count; i++)
        {
            camerasConfigurations[i].SetName("Level " + (i + 1).ToString());
        }
    }

}
