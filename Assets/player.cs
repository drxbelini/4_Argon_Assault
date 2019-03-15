using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class player : MonoBehaviour
{
    [Tooltip ("in ms^-1")] [SerializeField] float xSpeed = 10f;
    [Tooltip("in m")] [SerializeField] float xPos = 8f;

    [Tooltip("in ms^-1")] [SerializeField] float ySpeed = 10f;
    [Tooltip("in m")] [SerializeField] float yPos = 4f;

    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float controlPitchFactor = -17f;

    [SerializeField] float positionYawFactor = 3f;
    
    [SerializeField] float controlRollFactor = -20f;
/*
    [SerializeField] ParticleSystem LaserParticles_L;
    [SerializeField] ParticleSystem LaserParticles_R;
*/
    float xThrol, yThrol;


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        ProcessTraslation();
        ProcessRotation();

       /* bool fire = CrossPlatformInputManager.GetButton("Fire1");

        if (LaserParticles_L.isPlaying && LaserParticles_R.isPlaying && !fire)
        {
            LaserParticles_L.Stop();
            LaserParticles_R.Stop();
        }
        else
        {
            LaserParticles_L.Play();
            print("NOT - firing");
        }
        */
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

        float xOffset = xThrol * xSpeed * Time.deltaTime;
        float yOffset = yThrol * ySpeed * Time.deltaTime;

        float rawXpos = transform.localPosition.x + xOffset;
        float rawYpos = transform.localPosition.y + yOffset;

        float clapedXpos = Mathf.Clamp(rawXpos, -xPos, xPos);
        float clapedYpos = Mathf.Clamp(rawYpos, -yPos, yPos);

        transform.localPosition = new Vector3(clapedXpos, clapedYpos, transform.localPosition.z);
    }
}
