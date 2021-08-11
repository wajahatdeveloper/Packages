using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[InlineProperty]
[LabelWidth(75)]
public class ScriptableValueReference<TValue, TAsset> where TAsset : ScriptableValueAsset<TValue>
{
    #region Value

    private static ValueDropdownList<bool> valueList = new ValueDropdownList<bool>()
    {
        {"Value", true},
        {"Reference", false}
    };
    
    [HorizontalGroup("1",MaxWidth = 100)]
    [ValueDropdown(nameof(valueList))]
    [HideLabel]
    [SerializeField]
    protected bool useValue = true;

    [HorizontalGroup("1")] 
    [ShowIf(nameof(useValue), Animate = false)] 
    [HideLabel]
    [SerializeField]
    protected TValue _value;

    #endregion

    #region Reference

    [ShowIf("@useValue == false", Animate = false)]
    [HorizontalGroup("1")]
    [OnValueChanged("UpdateAsset")]
    [HideLabel]
    [SerializeField]
    protected TAsset assetReference;
    
    [ShowIf("@assetReference != null && useValue == false")]
    [LabelWidth(100)]
    [SerializeField]
    protected bool editAsset = false;
    
    [ShowIf(nameof(editAsset))]
    [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
    [SerializeField]
    protected TAsset _assetReference;

    #endregion
    
    public TValue Value
    {
        get
        {
            if (_assetReference == null || useValue)
            {
                return _value;
            }
            else
            {
                return _assetReference.value;
            }
        }
    }
    
    protected void UpdateAsset()
    {
        _assetReference = assetReference;
    }
    
    public static implicit operator TValue(ScriptableValueReference<TValue, TAsset> valueRef)
    {
        return valueRef.Value;
    }
}