using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMana
{
    float MP { get; set; }
    float MaxMP { get; }


    Action<float> onMPChange { get; set; }

    void ManaRegenerate();
}
