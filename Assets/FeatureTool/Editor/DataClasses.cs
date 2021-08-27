using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using ObjectFieldAlignment = Sirenix.OdinInspector.ObjectFieldAlignment;

namespace FeatureTool
{
    [HideLabel]
    [System.Serializable]
    public class Component
    {
        [DisplayAsString, LabelText("Name:")]
        public string name;
        [HorizontalGroup("g1",0.8f), PropertySpace(spaceBefore:10f)]
        [Multiline, HideLabel, HideInTables]
        public string description;
        [HorizontalGroup("g1"), PropertySpace(spaceBefore:10f)]
        [AssetSelector, HideLabel, PreviewField(ObjectFieldAlignment.Left), HideInTables, OnValueChanged("AssetChanged"), NonSerialized,ShowInInspector]
        public Object asset;
        [HideInInspector]
        public string assetGuid;

        public void AssetChanged()
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            string guid = AssetDatabase.AssetPathToGUID(assetPath);
            assetGuid = guid;
        }
    }
    
    [HideLabel]
    [System.Serializable]
    public class Feature
    {
        [DisplayAsString]
        public string name;
        [Multiline, HideLabel]
        public string description;
        [TableList(AlwaysExpanded = true, HideToolbar = true)]
        public List<Component> components;

        public Feature()
        {
            components = new List<Component>();
        }
        
        public Feature(Feature feature)
        {
            name = feature.name;
            description = feature.description;
            components = new List<Component>();
            components.AddRange(feature.components);
        }
    }
    
    [HideLabel]
    [System.Serializable]
    public class Domain
    {
        [HideInInspector]
        public string name;
        [Multiline, HideLabel, Header("Description")]
        public string description;
        [TableList(AlwaysExpanded = true, HideToolbar = true)]
        public List<Feature> features;
    }
}