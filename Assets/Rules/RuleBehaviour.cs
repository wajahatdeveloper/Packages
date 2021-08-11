using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class RuleBehaviour : MonoBehaviour
{
    [PropertyOrder(0)]
    [PropertySpace(SpaceBefore = 10, SpaceAfter = 20)]
    [ListDrawerSettings(Expanded = true, ShowPaging = true, ShowItemCount = true, NumberOfItemsPerPage = 10)]
    public List<string> rulesOverview;
}