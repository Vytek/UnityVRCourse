using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class PickUpParent : MonoBehaviour {

    
    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    public GameObject prefabSphere;
   
    [Space(10)]
    [Header("Haptics")]
    [Space(5)]
    [Range(0, 5)]
    public int pulseCount;
    [Range(0.0f, 5.0f)]
    public float pulseLength;
    [Range(0.0f, 5.0f)]
    public float pauseLength;


	void Awake () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

	}
	

	void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding touch on the trigger");
        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have pressed down the touch trigger.");
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have released the touch trigger.");
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding press on the trigger");
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have activated predddown the touch trigger.");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have released the up press trigger.");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("You have activated press up on the touchpad");
            GameObject.Instantiate(prefabSphere);
            //sphere.transform.position = Vector3.zero;
            //sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("You have collided with " + col.name + " and activated on trigger stay.");
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with the " + col.name + "While holding down touch");
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.transform.SetParent(this.gameObject.transform);

        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have released Touch while colliding with " + col.name);
            col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;

            tossObject(col.attachedRigidbody);

        }

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("You have collided with " + col.name);
        //StartCoroutine(HapticSinglePulse(2f));
       StartCoroutine(HapticPatternPulse(pulseCount, pulseLength, pauseLength));

    }

    IEnumerator HapticSinglePulse(float length)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse();
            yield return null;
        }
    }
    
    IEnumerator HapticPatternPulse(int pulseCount, float pulseLength, float pauseLength)
    {
        for (float i = 0; i < pulseCount; i++)
        {
            yield return StartCoroutine(HapticSinglePulse(pulseLength));
            yield return new WaitForSeconds(pauseLength);
        }
    }

    void tossObject(Rigidbody rigidbody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            rigidbody.velocity = origin.TransformVector(device.velocity);
            rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidbody.velocity = device.velocity;
            rigidbody.angularVelocity = device.angularVelocity;
        }
    }
}
