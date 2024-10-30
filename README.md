# Screen Recording for Unity

This sample project is developed for  **Unity Engine** and supports only **Android** platform. It provides support for recording Unity's Main Camera view and save it as video(.mp4) format. This project contains Unity C# code to get Main Camera frames and **.aar** file to save those frames as video in .mp4 format on Android device.

# Requirements
-  Unity Editor 2022.3.40f1
-  Android OS 8.0 and up


# How to use

Go to Scenes folder. **ScreenRecordSample** scene has sample code and basic setup.

## Start Recording

	 javaClass.Call<string>("InitRecording", Width, Height, fps, filePath);

## Add Frames to video

	 javaClass.Call("AddVideoFrames", sdata);

## Stop Recoding

	javaClass.Call("StopRecordVideo");

## References
- [GitHub - duzexu/Record_Screen_In_Unity](https://github.com/duzexu/Record_Screen_In_Unity) : This repository provides the code to record screen in unity by making Unity project as Android Library.
- [RGB2YUV](https://github.com/alzybaad/RGB2YUV/blob/master/app/src/main/java/team/birdhead/rgb2yuv/converter/JavaConverter.java) : This code supports conversion of RGB frames to YUV.
- [NV21Utils](https://github.com/pedroSG94/RootEncoder/blob/master/encoder/src/main/java/com/pedro/encoder/utils/yuv/NV21Utils.java) : This code supports conversion from NV21 to other different formats.

## Output

https://github.com/user-attachments/assets/24fb2664-1bce-42d9-a67b-322772ab670c



