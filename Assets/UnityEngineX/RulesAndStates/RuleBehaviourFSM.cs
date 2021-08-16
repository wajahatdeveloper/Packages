using System.Collections;
using System.Collections.Generic;
using Assets.RobustFSM.Mono;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class RuleBehaviourFSM : MonoFSM
{
    [PropertyOrder(0)]
    [PropertySpace(SpaceBefore = 10, SpaceAfter = 20)]
    [ListDrawerSettings(Expanded = true, ShowPaging = true, ShowItemCount = true, NumberOfItemsPerPage = 10)]
    public List<string> rulesOverview;
}