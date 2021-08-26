using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using Sisus.OdinSerializer;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace FeatureTool
{
    public class FeatureWindow : OdinMenuEditorWindow
    {
        public static List<Domain> domains = new List<Domain>();
        public static string savePath;
        public static bool didScriptsRecompiled = false;

        private List<Feature> _currentFeatures;
        private List<Component> _currentComponents;

        [MenuItem("Hub/Feature Window")]
        public static void Open()
        {
            savePath = Application.persistentDataPath + "/FeatureWindow.json";
            LoadChanges();

            var window = GetWindow<FeatureWindow>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            window.ForceMenuTreeRebuild();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;
            tree.Selection.SelectionChanged += OnSelectionChanged;
            tree.Config.DefaultMenuStyle.Height = 22;
            tree.Config.DefaultMenuStyle.Borders = true;

            if (domains.IsEmpty())
            {
                Domain defaultDomain = new Domain() {name = "Default", features = new List<Feature>()};
                domains.Add(defaultDomain);
                tree.Add(defaultDomain.name,defaultDomain);
            }
            
            foreach (Domain domain in domains)
            {
                tree.Add(domain.name,domain);
                foreach (Feature feature in domain.features)
                {
                    tree.Add($"{domain.name}/{feature.name}",feature);
                    foreach (Component component in feature.components)
                    {
                        tree.Add($"{domain.name}/{feature.name}/{component.name}",component);
                    }
                }
            }

            return tree;
        }
        
        protected override void OnBeginDrawEditors()
        {
            var selected = this.MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);

                    if (selected.Value.GetType() == typeof(Domain))
                    {
                        Domain currentDomain = (Domain)selected.Value;
                        if (SirenixEditorGUI.ToolbarButton(new GUIContent("Add New Feature")))
                        {
                            // Add Feature Here
                            EditorInputDialog.Show( "New Feature", "Enter new Feature name", "", ret: val =>
                            {
                                if (!string.IsNullOrEmpty(val))
                                {
                                    var newFeature = new Feature()
                                    {
                                        name = val,
                                        components = new List<Component>(),
                                    };
                                    _currentFeatures.AddIfNotContains(newFeature);
                                    MenuTree.Add($"{currentDomain.name}/{newFeature.name}",newFeature);
                                    base.TrySelectMenuItemWithObject(newFeature);
                                    hasUnsavedChanges = true;
                                } 
                            });
                        }
                        
                        if (SirenixEditorGUI.ToolbarButton(new GUIContent("Add Existing Feature")))
                        {
                            // Add Feature Here
                            var features = domains.Select(item => item.features).ToList();
                            var distinctFeatures = features.SelectMany(list => list).Distinct().ToList();
                            var distinctFeatureNames = distinctFeatures.Select(item => item.name).ToList();
                            
                            string selectedFeatureName = distinctFeatureNames[0];
                            
                            GenericSelector<string> selector = new GenericSelector<string>("Title", false, distinctFeatureNames);
                            selector.SetSelection(selectedFeatureName);
                            selector.SelectionTree.Config.DrawSearchToolbar = true;
                            selector.SelectionTree.DefaultMenuStyle.Height = 22;
                            selector.SelectionConfirmed += (selection) =>
                            {
                                selectedFeatureName = selection.FirstOrDefault();
                                var selectedFeature = new Feature(distinctFeatures.FirstOrDefault(item => item.name == selectedFeatureName));
                                _currentFeatures.AddIfNotContains(selectedFeature);
                                MenuTree.Add($"{currentDomain.name}/{selectedFeature.name}",selectedFeature);
                                base.TrySelectMenuItemWithObject(selectedFeature);
                                hasUnsavedChanges = true;
                            };
                            var popup = selector.ShowInPopup();
                        }

                        if (SirenixEditorGUI.ToolbarButton(new GUIContent("Delete Domain")))
                        {
                            // Delete Domains Here
                            EditorYesNoDialog.Show( "Delete Domain?", "Are you sure, this will delete the\n domain with its entire hierarchy!", () =>
                            {
                                domains.Remove(currentDomain);
                                ForceMenuTreeRebuild();
                                hasUnsavedChanges = true;
                            }, () =>
                            {
                                // Do Nothing
                            });
                        }
                    }
                    else if (selected.Value.GetType() == typeof(Feature))
                    {
                        Domain currentDomain = (Domain)selected.Parent.Value;
                        Feature currentFeature = (Feature)selected.Value;
                        if (SirenixEditorGUI.ToolbarButton(new GUIContent("Add Component")))
                        {
                            // Add Component Here
                            EditorInputDialog.Show("New Domain", "Enter new Domain name", "", ret: val =>
                            {
                                if (!string.IsNullOrEmpty(val))
                                {
                                    var newComponent = new Component()
                                    {
                                        name = val,
                                        asset = null,
                                    };
                                    _currentComponents.Add(newComponent);
                                    MenuTree.Add($"{currentDomain.name}/{currentFeature.name}/{newComponent.name}", newComponent);
                                    base.TrySelectMenuItemWithObject(newComponent);
                                    hasUnsavedChanges = true;
                                }
                            });
                        }
                        
                        if (SirenixEditorGUI.ToolbarButton(new GUIContent("Delete Feature")))
                        {
                            // Delete Domains Here
                            EditorYesNoDialog.Show( "Delete Feature?", "Are you sure, this will delete the\n feature with its entire hierarchy!", () =>
                            {
                                currentDomain.features.Remove(currentFeature);
                                ForceMenuTreeRebuild();
                                hasUnsavedChanges = true;
                            }, () =>
                            {
                                // Do Nothing
                            });
                        }
                    }
                    else if (selected.Value.GetType() == typeof(Component))
                    {
                        Domain currentDomain = (Domain)selected.Parent.Parent.Value;
                        Feature currentFeature = (Feature)selected.Parent.Value;
                        Component currentComponent = (Component)selected.Value;
                        if (SirenixEditorGUI.ToolbarButton(new GUIContent("Delete Component")))
                        {
                            // Delete Domains Here
                            EditorYesNoDialog.Show( "Delete Component?", "Are you sure, this will delete the component!", () =>
                            {
                                currentFeature.components.Remove(currentComponent);
                                ForceMenuTreeRebuild();
                                hasUnsavedChanges = true;
                            }, () =>
                            {
                                // Do Nothing
                            });
                        }
                    }
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Add Domain")))
                {
                    // Add Domain Here
                    EditorInputDialog.Show( "New Domain", "Enter new Domain name", "", ret: val =>
                    {
                        if (!string.IsNullOrEmpty(val))
                        {
                            var newDomain = new Domain()
                            {
                                name = val,
                                features = new List<Feature>(),
                            };
                            domains.Add(newDomain);
                            MenuTree.Add(newDomain.name,newDomain);
                            base.TrySelectMenuItemWithObject(newDomain);
                            hasUnsavedChanges = true;
                        } 
                    });
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Save")))
                {
                    SaveChanges();
                    hasUnsavedChanges = false;
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private void OnSelectionChanged(SelectionChangedType type)
        {
            if (type != SelectionChangedType.ItemAdded) { return; }
            
            if (MenuTree.Selection.SelectedValue.GetType() == typeof(Domain))
            {
                Domain domain = (Domain)MenuTree.Selection.SelectedValue;
                _currentFeatures = domain.features;
            }
            else if (MenuTree.Selection.SelectedValue.GetType() == typeof(Feature))
            {
                Feature feature = (Feature)MenuTree.Selection.SelectedValue;
                _currentComponents = feature.components;
            }
            else if (MenuTree.Selection.SelectedValue.GetType() == typeof(Component))
            {
                Component component = (Component)MenuTree.Selection.SelectedValue;
                string componentAssetPath = AssetDatabase.GUIDToAssetPath(component.assetGuid);
                component.asset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(componentAssetPath);
            }
        }

        public override void SaveChanges()
        {
            // Save to Json
            var jsonBytes = SerializationUtility.SerializeValue(domains, DataFormat.JSON);
            File.WriteAllBytes(savePath,jsonBytes);
            
            base.SaveChanges();
        }

        public static void LoadChanges()
        {
            // Load from Json
            if (!File.Exists(savePath)) { return; }

            domains.Clear();
            var jsonBytes = File.ReadAllBytes(savePath);
            domains = SerializationUtility.DeserializeValue<List<Domain>>(jsonBytes,DataFormat.JSON);
        }

        [DidReloadScripts]
        public static void OnScriptsRecompiled()
        {
            didScriptsRecompiled = true;
        }
        
        protected override void OnGUI()
        {
            if (didScriptsRecompiled)
            { 
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.BeginVertical();
                GUILayout.FlexibleSpace();
                if (GUILayout.Button ("Reload Window", GUILayout.Width(100), GUILayout.Height(60)))
                {
                    Open();
                    didScriptsRecompiled = false;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal ();
            }
            else
            {
                base.OnGUI();
            }
        }
    }
}