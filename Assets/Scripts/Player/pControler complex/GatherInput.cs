using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherInput : MonoBehaviour
{
    public InputData inputData = new InputData();
    // Update is called once per frame
    void Update()
    {
        inputData.move = Input.GetAxisRaw("Horizontal");
        inputData.jump = Input.GetButton("Jump");
    }

}
