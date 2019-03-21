using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerControler : MonoBehaviour
{   
    [Header("General")]
    [Tooltip ("in ms^-1")] [SerializeField] float controlSpeed = 10f;
    [Tooltip("in m")] [SerializeField] float xRange = 8f;
    [Tooltip("in m")] [SerializeField] float yRange = 4f;

    [Header("Position Factor")]
    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float positionYawFactor = 3f;

    [Header("Control Factor")]
    [SerializeField] float controlPitchFactor = -17f;   
    [SerializeField] float controlRollFactor = -20f;

    float xThrol, yThrol;
    bool disable = false;
    

    // Update is called once per frame
    void Update()
    {   
        if (disable) { return; }
        ProcessTraslation();
        ProcessRotation();               
    }

    public void disableControl() // Called by string reference
    {
        disable = true;
        print("Sended message");

    }


    private void ProcessRotation()
    {
        float pitchDuePositon = transform.localPosition.y * positionPitchFactor;
        float pitchDueControl = controlPitchFactor * yThrol;
        float pitch = pitchDueControl + pitchDuePositon;
                    
        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = controlRollFactor * xThrol;
        
        
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }


    private void ProcessTraslation()
    {
        xThrol = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrol = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrol * controlSpeed * Time.deltaTime;
        float yOffset = yThrol * controlSpeed * Time.deltaTime;

        float rawXpos = transform.localPosition.x + xOffset;
        float rawYpos = transform.localPosition.y + yOffset;

        float clapedXpos = Mathf.Clamp(rawXpos, -xRange, xRange);
        float clapedYpos = Mathf.Clamp(rawYpos, -yRange, yRange);

        transform.localPosition = new Vector3(clapedXpos, clapedYpos, transform.localPosition.z);
    }
}
