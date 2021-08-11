using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Model_Variables : SerializedMonoBehaviour
{
    public List<Bool_Reference> boolVars = new List<Bool_Reference>();
    public List<Int_Reference> intVars = new List<Int_Reference>();
    public List<Float_Reference> floatVars = new List<Float_Reference>();
    public List<String_Reference> stringVars = new List<String_Reference>();
    public List<Color_Reference> colorVars = new List<Color_Reference>();
    public List<Vector2_Reference> vec2Vars = new List<Vector2_Reference>();
    public List<Vector3_Reference> vec3Vars = new List<Vector3_Reference>();
}