using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenRecord : MonoBehaviour
{
    [SerializeField]
    // private CameraFrames cameraFrames;
    private CameraFrameSettings cameraFrameSettings;

    public static bool isRecording;

    AndroidJavaObject javaClass;

    string finalPath;
    public static ScreenRecord instance;

    public Action<string> OnRecordingStopped;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isRecording = false;
        javaClass = new AndroidJavaObject("com.bhavisha.screenrecord.ScreenRecorder");
    }

    public void StartVideoRecording()
    {
        InitVideoRecording();
    }
    private string GenerateFullPathString()
    {
        string tempPath = Application.persistentDataPath;
        string filePath = Path.Combine(tempPath, $"UnityRecording_{DateTime.Now:ddMMyyyyHHmmss}.mp4");
        Debug.Log($"-------- filePath = {filePath}");
        return filePath;
    }
    private void InitVideoRecording()
    {
        try
        {
            string filePath = GenerateFullPathString();
            javaClass.Call("InitRecording", cameraFrameSettings.frameWidth, cameraFrameSettings.frameHeight, cameraFrameSettings.fps, filePath, cameraFrameSettings.isRGBA);
            Debug.Log($"-------- Full path = {filePath}");
            finalPath = filePath;
            isRecording = true;
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void AddVideoFrames(byte[] data, int dataLength)
    {
        AddVideoData(data, dataLength);
    }
    public void StopVideoRecording()
    {
        StopRecordVideo();
    }
    sbyte[] sdata;
    private void AddVideoData(byte[] data, int dataLenth)
    {
        try
        {
            //Convert byte[] to sbyte[] because android java does not accept byte[] as argument.
            sdata = new sbyte[data.Length];
            Buffer.BlockCopy(data, 0, sdata, 0, data.Length);
            javaClass.Call("AddVideoFrames", sdata);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    private void StopRecordVideo()
    {
        try
        {
            isRecording = false;
            javaClass.Call("StopRecordVideo");

            OnRecordingStopped.Invoke(finalPath);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

}
