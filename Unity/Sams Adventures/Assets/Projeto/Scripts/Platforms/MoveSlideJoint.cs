using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSlideJoint : MonoBehaviour
{
    public float velocityMotor = 3;
    public SliderJoint2D slider;
    private JointMotor2D motor;

    // Start is called before the first frame update
    void Start()
    {
        motor = slider.motor;
        motor.motorSpeed = 1 * velocityMotor;
        slider.motor = motor;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.limitState == JointLimitState2D.LowerLimit)
        {
            motor.motorSpeed = 1 * velocityMotor;
            slider.motor = motor;
        }

         if(slider.limitState == JointLimitState2D.UpperLimit)
        {
            motor.motorSpeed = -1 * velocityMotor;
            slider.motor = motor;
        }
    }
}
