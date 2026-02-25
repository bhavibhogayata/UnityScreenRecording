# Screen Recording for Unity

This sample project is built with Unity Engine and supports the Android platform only. It demonstrates how to capture frames from Unity’s Main Camera and save them as an .mp4 video file on an Android device.

## Features

-  Capture Unity Main Camera frames

-  Record and save video in .mp4 format

-  Unity C# implementation for frame capture

-  Native Android .aar plugin for video encoding and saving

## Requirements
-  Unity Editor 6000.3.8f1 ( might work with other Unity Versions)
-  Android OS 8.0 and up

## Related Samples
If you are looking for Passthrough camera frame recording on Meta Quest 3, please refer to the [Meta Quest Passthrough Recorder](https://github.com/bhavibhogayata/MetaQuestPassthroughRecording) sample project.


## How to use

Go to Scenes folder. **ScreenRecordSample** scene has sample code and basic setup.

### Start Recording

     javaClass.Call<string>("InitRecording", Width, Height, fps, filePath, isRGBA);

### Add Frames to video

     javaClass.Call("AddVideoFrames", sdata);

### Stop Recoding

    javaClass.Call("StopRecordVideo");

## References
- [GitHub - duzexu/Record_Screen_In_Unity](https://github.com/duzexu/Record_Screen_In_Unity) : This repository provides the code to record screen in unity by making Unity project as Android Library.
- [RGB2YUV](https://github.com/alzybaad/RGB2YUV/blob/master/app/src/main/java/team/birdhead/rgb2yuv/converter/JavaConverter.java) : This code supports conversion of RGB frames to YUV.
- [NV21Utils](https://github.com/pedroSG94/RootEncoder/blob/master/encoder/src/main/java/com/pedro/encoder/utils/yuv/NV21Utils.java) : This code supports conversion from NV21 to other different formats.

## Output

https://github.com/user-attachments/assets/24fb2664-1bce-42d9-a67b-322772ab670c