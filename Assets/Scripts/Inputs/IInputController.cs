using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputController {

    float inputHorizontalMovement { get; set; }
    bool inputJump { get; set; }
    bool inputDash { get; set; }
    bool inputPush { get; set; }

}