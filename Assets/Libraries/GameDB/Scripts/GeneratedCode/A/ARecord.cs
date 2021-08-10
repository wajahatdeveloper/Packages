using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameDB
{
	//Generated code, do not edit!

	[Serializable]
	public class ARecord : BaseRecord<AIdentifier>
	{
		[ColumnName("a")] [SerializeField] private EventsIdentifier[] _a = default;
		[NonSerialized] private EventsRecord[] _aRecords = default;
		public EventsRecord[] A 
		{ 
			get 
			{ 
				if(_aRecords == null)
				{
					_aRecords = new EventsRecord[_a.Length];
					for(int i = 0; i < _aRecords.Length; i++)
						_aRecords[i] = ModelManager.EventsModel.GetRecord(_a[i]);
				}
				return _aRecords; 
			} 
			set
			{
				if(!CheckEdit())
					return;
					
				EventsIdentifier[] newData = new EventsIdentifier[value.Length];
				for(int i = 0; i < value.Length; i++)
				{
					EventsRecord record = value[i];
					if(record == null)
						newData[i] = EventsIdentifier.None;
					else
						newData[i] = record.Identifier;
				}
				_a = newData;
				_aRecords = null;
			}
		}

        protected bool runtimeEditingEnabled { get { return originalRecord != null; } }
        public AModel model { get { return ModelManager.AModel; } }
        private ARecord originalRecord = default;

        public override void CreateEditableCopy()
        {
#if UNITY_EDITOR
            if (runtimeEditingEnabled)
                return;

            ARecord editableCopy = new ARecord();
            editableCopy.Identifier = Identifier;
            editableCopy.originalRecord = this;
            CopyData(editableCopy);
            model.SetEditableCopy(editableCopy);
#else
            Debug.LogError("GameDB: Creating an editable record does not work in buolds. See documentation 'Editing your data at runtime' for more information.");
#endif
        }

        public override void SaveToScriptableObject()
        {
#if UNITY_EDITOR
            if (!runtimeEditingEnabled)
            {
                Debug.LogWarning("GameDB: Runtime Editing is not enabled for this object. Either you are not using the editable copy or you're trying to edit in a build.");
                return;
            }
            CopyData(originalRecord);
            model.SaveModel();
#else
            Debug.LogError("GameDB: Saving to ScriptableObject does not work in builds. See documentation 'Editing your data at runtime' for more information.");
#endif
        }

        private void CopyData(ARecord record)
        {
            record._a = _a;
        }

        private bool CheckEdit()
        {
            if (runtimeEditingEnabled)
                return true;

            Debug.LogWarning("GameDB: Runtime Editing is not enabled for this object. Either you are not using the editable copy or you're trying to edit in a build.");
            return false;
        }
    }
}
