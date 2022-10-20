using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float HP { get; set; }
    float MaxHP { get; }


    Action onHealthChange { get; set; }

    void Die();

    Action onDie { get; set; }
}
