using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenRecord : MonoBehaviour
{
    [SerializeField]
    private CameraFrames cameraFrames;

    public static bool isRecording;

    AndroidJavaObject javaClass;

    public static ScreenRecord instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isRecording = false;
        javaClass = new AndroidJavaObject("com.example.screenrecorder.NativeScreenRecorder");

    }
    public void StartVideoRecording()
    {
        InitVideoRecording();
    }
    private string GenerateFullPathString()
    {
        string extStoragePath = javaClass.Call<string>("GetExternalStoragePath");
        Debug.Log($"extStoragePath = {extStoragePath}");
        string filePath = Path.Combine(extStoragePath, $"UnityRecording_{DateTime.Now.Millisecond.ToString()}.mp4");
        Debug.Log($"filePath = {filePath}");
        return filePath;

    }
    private void InitVideoRecording()
    {
        try
        {
            string filePath = GenerateFullPathString();
            string fullpath = javaClass.Call<string>("InitRecording", cameraFrames.frameWidth, cameraFrames.frameHeight, cameraFrames.fps, filePath);
            Debug.Log($"Full path = {fullpath}");
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
            cameraFrames.ReleaseData();
            javaClass.Call("StopRecordVideo");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

}
