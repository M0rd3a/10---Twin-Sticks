using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour {

    private const int bufferFrames = 1000;
    private MyKeyFrame[] keyFrames = new MyKeyFrame[bufferFrames];
    private GameManager gameManager;
    private Rigidbody rigidBody;

    private int bufferSize = bufferFrames;
    private int lastRecordedFrame = 0, nextRecordedFrame = 0;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameManager.recording)
        {
            Record();
        }
        else
        {
            PlayBack();
        }
    }

    void PlayBack()
    {
        rigidBody.isKinematic = true;

        if (Time.frameCount < bufferSize || 
            (lastRecordedFrame != 0 && lastRecordedFrame < bufferSize))
        {//Never reached the end of the cycle
            bufferSize = Time.frameCount;
            lastRecordedFrame = Time.frameCount;
        }

        int frame = Time.frameCount % bufferSize;

        //print("Reading frame: " + frame);
        transform.position = keyFrames[frame].position;
        transform.rotation = keyFrames[frame].rotation;
    }

    void Record()
    {
        bufferSize = bufferFrames;
        rigidBody.isKinematic = false;
        nextRecordedFrame = Time.frameCount;

        if (lastRecordedFrame > 0)
        {
            nextRecordedFrame = lastRecordedFrame++;
        }

        int frame = nextRecordedFrame % bufferFrames;
        //print("Writing frame: " + frame);
        keyFrames[frame] = new MyKeyFrame(Time.time, transform.position, transform.rotation);
    }
}
/// <summary>
/// A structure for storing time, position and rotation
/// </summary>
public struct MyKeyFrame
{
    public float frameTime;
    public Vector3 position;
    public Quaternion rotation;

    public MyKeyFrame(float t, Vector3 pos, Quaternion rot)
    {
        frameTime = t;
        position = pos;
        rotation = rot;
    }
}