using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerControler : MonoBehaviour
{   
    [Header("General")]   
    [Tooltip("in m")] [SerializeField] float xRange = 8f;
    [Tooltip("in m")] [SerializeField] float yRange = 4f;
    [SerializeField] GameObject[] guns;
    
    /*
    [Header("Rotation Factor")]
    [SerializeField] float positionPitchFactor = -2.5f;
    */

    [SerializeField] float positionYawFactor = 3f;
    

    [Header("Control Factor")]
    [SerializeField] float aileronFactor = -0.15f;
    [SerializeField] float aileronAngularFactor = 35f;
    [SerializeField] float yawAngularFactor = 5f;


    [SerializeField] float profundorFactor = 0.15f;
    [SerializeField] float profundorAngularFactor = 35f;

    [SerializeField] float angularRollFactor;
    [SerializeField] float angularPitchFactor;
    [SerializeField] float angularYawFactor;


    float angularRoll;
    float angularPitch;
    float angularYaw;


    float xThrow, yThrow;
    bool disable = false;

    
    

    // Update is called once per frame
    void Update()
    {   
        if (disable) { return; }

        ProcessTraslation();
        ProcessRotation();
        ProcessGuns();

        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
    }

    public void disableControl() // Called by string reference on colision document
    {
        disable = true;
        print("Sended message");

    }


    private void ProcessRotation()
    {      
        ProcessAngularPitch();
        ProcessAngularRoll();
        ProcessControlYaw();
    }

    private void ProcessControlYaw()
    {
        float yawAngleLimiter = yawAngularFactor * xThrow;

        if ((xThrow < 0) &&(angularYaw >= -yawAngleLimiter))
        {           
            angularYaw = angularYaw + angularYawFactor;
            print(angularYaw);
        }
        if ((xThrow > 0) && (angularYaw <= -yawAngleLimiter))
        {
            angularYaw = angularYaw - angularYawFactor;
            print(angularYaw);
        }
        if (xThrow == 0)
        {
            if (angularYaw < 0)
            {
                angularYaw = angularYaw - angularYawFactor;
            }
            if (angularYaw > 0)
            {
                angularYaw = angularYaw + angularYawFactor;
            }

            print(angularYaw);
        }

        float yaw = (transform.localPosition.x * positionYawFactor) + angularYaw;
        transform.localRotation = Quaternion.Euler(angularPitch, yaw, angularRoll);
    }

    private void ProcessAngularPitch()
    {
        if ((yThrow < 0) && (angularPitch < profundorAngularFactor))
        {
            angularPitch = angularPitch + angularPitchFactor;           
        }
        if ((yThrow > 0) && (angularPitch > -profundorAngularFactor))
        {
            angularPitch = angularPitch -angularPitchFactor;            
        }
        if (yThrow == 0)
        {
            if (angularPitch < 0)
            {
                angularPitch = angularPitch + angularPitchFactor * 0.5f;                
            }
            if (angularPitch > 0)
            {
                angularPitch = angularPitch - angularPitchFactor* 0.5f;                
            }
        }
    }

    private void ProcessAngularRoll()
        
    {
        float aileronAngleLimiter = aileronAngularFactor * xThrow;

        if ((xThrow < 0) && (angularRoll <= -aileronAngleLimiter))
        {
            angularRoll = angularRoll + angularRollFactor;            
        }
        if ((xThrow > 0) && (angularRoll >= -aileronAngleLimiter))
        {
            angularRoll = angularRoll - angularRollFactor;
        }

        if (xThrow == 0)
        {
            if (angularRoll < Mathf.Epsilon)
            {
                angularRoll = angularRoll + angularRollFactor * 0.5f;
            }
            if (angularRoll > Mathf.Epsilon)
            {
                angularRoll = angularRoll - angularRollFactor *0.5f;
            }
        }
    }

    private void ProcessTraslation()
    {    

        float xOffset = angularRoll * aileronFactor * Time.deltaTime;
        float yOffset = angularPitch * profundorFactor * Time.deltaTime;

        float rawXpos = transform.localPosition.x + xOffset;
        float rawYpos = transform.localPosition.y + yOffset;

        float clapedXpos = Mathf.Clamp(rawXpos, -xRange, xRange);
        float clapedYpos = Mathf.Clamp(rawYpos, -yRange, yRange);


        transform.localPosition = new Vector3(clapedXpos, clapedYpos, transform.localPosition.z);
    }

    void ProcessGuns()
    {
        if(CrossPlatformInputManager.GetButton("Fire1"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }
    void SetGunsActive(bool ActiveGuns)
    {
        foreach(GameObject gun in guns)
        {
            var EmissionModule = gun.GetComponent<ParticleSystem>().emission;
            EmissionModule.enabled = ActiveGuns;
        }
    }
}
