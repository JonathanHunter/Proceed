﻿//Proceed: Jonathan Hunter, Larry Smith, Justin Coates, Chris Tansey
using UnityEngine;
using System.Collections;

public class BasicWhiskerNav : MonoBehaviour {

    Vector3 moveDirection;
    public float whiskerLength = 4.5f;
    public float emergencyWhiskerLength = 4f;
    public float RunSpeed = 0.1f;
    public float BackPedalSpeed = 0.03f;
    public float MaxTurnSpeed = 1f;

    bool backOff = false;
    int backUpDuration = 30;
    int backUpTimer = 31;

    public GameObject Target;
    public float targetDistance;

	// Use this for initialization
	void Awake () 
    {
        //moveDirection = Vector3.forward * 0.1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        if(!backOff && backUpTimer > backUpDuration)
            transform.position += transform.forward * RunSpeed;
        else transform.position += -transform.forward * BackPedalSpeed;

        if(backUpTimer <= backUpDuration )
        {
            backUpTimer++;
        }

        Whisker(whiskerLength);
        TargetTracking();
	}

    void Whisker(float whiskerLength)
    {

        Vector3 leftWhiskerVector = -transform.right + transform.forward;
        Vector3 rightWhiskerVector = transform.right + transform.forward;

        int NavObjectMask = 1 << 10;

        RaycastHit[] emergencyWhiskerRight = Physics.RaycastAll(transform.position, rightWhiskerVector, emergencyWhiskerLength, NavObjectMask);
        RaycastHit[] emergencyWhiskerLeft = Physics.RaycastAll(transform.position, leftWhiskerVector, emergencyWhiskerLength, NavObjectMask);
        RaycastHit[] whiskerLeft = Physics.RaycastAll(transform.position, leftWhiskerVector, whiskerLength, NavObjectMask);
        RaycastHit[] whiskerRight = Physics.RaycastAll(transform.position, rightWhiskerVector, whiskerLength, NavObjectMask);
        
        /*
        if (emergencyWhiskerLeft.Length > 0)
        {
            print("Left Whisker: " + emergencyWhiskerLeft[0].collider);
        }

        if (emergencyWhiskerRight.Length > 0)
        {
            print("right Whisker: " + emergencyWhiskerRight[0].collider);
        }
        */
        if (emergencyWhiskerLeft.Length > 0 && emergencyWhiskerRight.Length > 0)
        {
            backOff = true;
            backUpTimer = 0;
        }else
        {
            backOff = false;
        }
        
        if(whiskerLeft.Length > 0)
        {
            transform.Rotate(0, MaxTurnSpeed, 0);
        }
        else if (whiskerRight.Length > 0)
        {
            transform.Rotate(0, -MaxTurnSpeed, 0);
        }

        Debug.DrawRay(transform.position, whiskerLength * leftWhiskerVector, Color.red);
        Debug.DrawRay(transform.position, emergencyWhiskerLength * rightWhiskerVector, Color.blue);

    }

    void TargetTracking()
    {
        Vector3 targetDirVec = Target.transform.position - transform.position;
        float targetDistance = Vector3.Distance(Target.transform.position, transform.position);

        int NavObjectMask = 1 << 10;
        RaycastHit[] obstacles = Physics.RaycastAll(transform.position, targetDirVec, targetDistance, NavObjectMask);

        if(obstacles.Length == 0)
        {
            Transform transformCopyLeft = this.transform;
            Transform transformCopyRight = this.transform;

            transformCopyLeft.Rotate(0, -MaxTurnSpeed, 0);
            Vector3 targetDirVecLeft = new Vector3(transformCopyLeft.forward.x, transformCopyLeft.forward.y, transformCopyLeft.forward.z);


            transformCopyRight.Rotate(0, 2*MaxTurnSpeed, 0);

            Vector3 targetDirVecRight = new Vector3(transformCopyRight.forward.x,transformCopyRight.forward.y,transformCopyRight.forward.z);

            transformCopyRight.Rotate(0, -MaxTurnSpeed, 0);


            Debug.DrawRay(transform.position, targetDirVec, Color.black);
            //Debug.DrawRay(transform.position, targetDirVecLeft*5f, Color.yellow,1f);
            //Debug.DrawRay(transform.position, targetDirVecRight *5f, Color.green,1f);

            float angleLeft = Vector3.Angle(targetDirVecLeft, targetDirVec);
            float angleRight = Vector3.Angle(targetDirVecRight, targetDirVec);


            if(angleLeft < angleRight)
            {
                
                if (angleLeft > 5)
                {
                    transform.Rotate(0, -MaxTurnSpeed, 0);
                }
                //print("turn left!!: " + angleLeft + "vecs: "+targetDirVec + " - "+targetDirVecLeft);
                 
            }else
            {
                if (angleRight > 5)
                {
                    transform.Rotate(0, MaxTurnSpeed, 0);
                }
                //print("turn right!!: " + angleRight + " vecs: "+ targetDirVec+ " - "+targetDirVecRight);

            }
        }
    }
}
