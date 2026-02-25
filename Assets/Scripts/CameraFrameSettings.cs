using UnityEngine;

[CreateAssetMenu(menuName = "Create Frame Setting", fileName = "Default Camera Setting")]
public class CameraFrameSettings : ScriptableObject
{
    public int fps = 15;

    //Width the output is suppose to have
    public int frameWidth;

    //Height the output is suppose to have
    public int frameHeight;

    // Pass this to true for android mobile devices and false for meta quest devices
    public bool isRGBA = false;

}
