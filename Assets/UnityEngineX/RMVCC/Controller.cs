using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Controller<M,V> : RuleBehaviour
    where M : Model
    where V : View
{
    [Required] public V view;
    [Required] public M model;
}