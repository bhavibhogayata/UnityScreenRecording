using UnityEngine;
using UnityEngine.UI;

public class CameraFrames : MonoBehaviour
{
    public Camera _Camera;
    private float mLastSample;

    private Texture2D mTexture;
    private RenderTexture mRtBuffer = null;

    public int fps = 15;
   
    //Use this to test the frames output. 
    public RawImage _Preview = null;

    //Width the output is suppose to have
    public int frameWidth;
    
    //Height the output is suppose to have
    public int frameHeight;

    private byte[] mByteBuffer = null;

    private Texture2D BufferTexture
    {
        get
        {
            if (mTexture == null)
            {
                mTexture = new Texture2D(frameWidth, frameHeight, TextureFormat.RGBA32, false);
            }

            return mTexture;
        }
    }
    private RenderTexture BufferRenderTexture
    {
        get
        {
            if (mRtBuffer == null)
            {
                mRtBuffer = new RenderTexture(frameWidth, frameHeight, 0, RenderTextureFormat.ARGB32);

                mRtBuffer.wrapMode = TextureWrapMode.Repeat;
                mRtBuffer.depth = 24;
            }

            return mRtBuffer;
        }
    }
    void Update()
    {
        if (!ScreenRecord.isRecording)
        {
            return;
        }


        //ensure correct fps
        float deltaSample = 1.0f / fps;
        mLastSample += Time.deltaTime;
        if (mLastSample >= deltaSample)
        {
            mLastSample -= deltaSample;

            //backup the current configuration to restore it later
            var oldTargetTexture = _Camera.targetTexture;
            var oldActiveTexture = RenderTexture.active;

            //Set the buffer as target and render the view of the camera into it
            _Camera.targetTexture = BufferRenderTexture;
            _Camera.Render();

            RenderTexture.active = BufferRenderTexture;
            BufferTexture.ReadPixels(new Rect(0, 0, BufferRenderTexture.width, BufferRenderTexture.height), 0, 0, false);
            BufferTexture.Apply();

            //get the byte array. still looking for a way to reuse the current buffer
            //instead of allocating a new one all the time
            mByteBuffer = BufferTexture.GetRawTextureData();

            ScreenRecord.instance.AddVideoFrames(mByteBuffer, mByteBuffer.Length);

            //reset the camera/active render texture  in case it is still used for other purposes
            _Camera.targetTexture = oldTargetTexture;
            RenderTexture.active = oldActiveTexture;

            //update debug output if available
            if (_Preview != null)
                _Preview.texture = BufferTexture;
        }
    }

    public void ReleaseData()
    {
        mByteBuffer = null;
        _Preview.texture = null;
        BufferRenderTexture.Release();
    }
    private void OnDestroy()
    {
        if (mRtBuffer != null)
        {
            Destroy(mRtBuffer);
            mRtBuffer = null;
        }

        if (mTexture != null)
        {
            Destroy(mTexture);
            mTexture = null;
        }
    }
}
