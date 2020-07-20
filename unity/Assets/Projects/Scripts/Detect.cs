using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TensorFlowLite;
using UnityEngine.Video;

public class Detect : MonoBehaviour
{
    [SerializeField, FilePopup("*.tflite")]
    string fileName = "detect.tflite";
    [SerializeField]
    RawImage cameraView = null;
    [SerializeField]
    Text framePrefab = null;
    [SerializeField, Range(0f, 1f)]
    float scoreThreshold = 0.5f;
    [SerializeField]
    TextAsset labelMap = null;
    //WebCamTexture webcamTexture;
    public RenderTexture rt;
    SSD ssd;
    public Material mat;
    Text[] frames;
    public string[] labels;

    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);
        ssd = new SSD(path);

        rt.Create();

        frames = new Text[10];
        var parent = cameraView.transform;
        for (int i = 0; i < frames.Length; i++)
        {
            frames[i] = Instantiate(framePrefab, Vector3.zero, Quaternion.identity, parent);
        }
        labels = labelMap.text.Split('\n');
    }

    void OnDestroy()
    {
        ssd?.Dispose();
    }

    void Update()
    {
        ssd.Invoke(rt);

        var results = ssd.GetResults();

        var size = ((RectTransform)cameraView.transform).rect.size;
        for (int i = 0; i < 10; i++)
        {
            SetFrame(frames[i], results[i], size);
        }
        cameraView.material = this.mat;
        this.mat = ssd.transformMat;
    }

    void SetFrame(Text frame, SSD.Result result, Vector2 size)
    {
        if (result.score < scoreThreshold)
        {
            frame.gameObject.SetActive(false);
            return;
        }
        else
        {
            frame.gameObject.SetActive(true);
        }
        frame.text = $"{GetLabelName(result.classID)} : {(int)(result.score * 100)}%";
        var rt = frame.transform as RectTransform;
        rt.anchoredPosition = result.rect.position * size - size * 0.5f;
        rt.sizeDelta = result.rect.size * size;
    }

    string GetLabelName(int id)
    {
        if (id < 0 || id >= labels.Length - 1)
        {
            return "?";
        }
        return labels[id];
    }
}