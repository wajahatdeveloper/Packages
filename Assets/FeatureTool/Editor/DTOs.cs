using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FeatureTool
{
    [HideLabel]
    [System.Serializable]
    public class Component
    {
        [DisplayAsString]
        public string name;
        [Multiline, HideLabel, HideInTables]
        public string description;
        [AssetSelector, HideLabel, PreviewField(ObjectFieldAlignment.Center), HideInTables,OnValueChanged("AssetChanged"),NonSerialized,ShowInInspector]
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