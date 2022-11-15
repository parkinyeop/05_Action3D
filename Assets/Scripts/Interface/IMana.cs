using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMana
{
    float MP { get; set; }
    float MaxMP { get; }

    Action<float> onManaChange { get; set; }

    void ManaRegenerate(float totalRegen, float duration);
}
