using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameDB
{
	//Generated code, do not edit!

	public static class ModelManager
	{
        private static Dictionary<DatasheetType, LoadRequest> loadRequests;

        static ModelManager()
        {
            loadRequests = new Dictionary<DatasheetType, LoadRequest>();
        }

        public static void InitializeAll()
        {
            DatasheetType[] values = Enum.GetValues(typeof(DatasheetType)) as DatasheetType[];
            foreach(DatasheetType value in values)
                Initialize(value);
        }
		
        public static void Unload(DatasheetType datasheetType)
        {
            switch (datasheetType)
            {
                case DatasheetType.Events:
                    {
                        if (eventsModel == null || eventsModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to unload model {0}. Model is not loaded.", datasheetType));
                            break;
                        }
                        Resources.UnloadAsset(eventsModel);
                        eventsModel = null;
                        LoadRequest request;
                        if (loadRequests.TryGetValue(DatasheetType.Events, out request))
                        {
                            loadRequests.Remove(DatasheetType.Events);
                            request.resourceRequest.completed -= OnLoadCompleted_EventsModel;
							foreach (Action<bool> callback in request.callbacks)
								callback(false);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public static void Initialize(DatasheetType datasheetType)
        {
            switch (datasheetType)
            {
                case DatasheetType.Events:
                    {
                        if (eventsModel != null && !eventsModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to Initialize {0}. Model is already initialized.", datasheetType));
                            break;
                        }

                        eventsModel = Resources.Load<EventsModel>("ScriptableObjects/Events");
                        LoadRequest request;
                        if (loadRequests.TryGetValue(DatasheetType.Events, out request))
                        {
                            Log(string.Format("Sheet Codes: Trying to initialize {0} while also async loading. Async load has been canceled.", datasheetType));
                            loadRequests.Remove(DatasheetType.Events);
                            request.resourceRequest.completed -= OnLoadCompleted_EventsModel;
							foreach (Action<bool> callback in request.callbacks)
								callback(true);
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public static void InitializeAsync(DatasheetType datasheetType, Action<bool> callback)
        {
            switch (datasheetType)
            {
                case DatasheetType.Events:
                    {
                        if (eventsModel != null && !eventsModel.Equals(null))
                        {
                            Log(string.Format("Sheet Codes: Trying to InitializeAsync {0}. Model is already initialized.", datasheetType));
                            callback(true);
                            break;
                        }
                        if(loadRequests.ContainsKey(DatasheetType.Events))
                        {
                            loadRequests[DatasheetType.Events].callbacks.Add(callback);
                            break;
                        }
                        ResourceRequest request = Resources.LoadAsync<EventsModel>("ScriptableObjects/Events");
                        loadRequests.Add(DatasheetType.Events, new LoadRequest(request, callback));
                        request.completed += OnLoadCompleted_EventsModel;
                        break;
                    }
                default:
                    break;
            }
        }

        private static void OnLoadCompleted_EventsModel(AsyncOperation operation)
        {
            LoadRequest request = loadRequests[DatasheetType.Events];
            eventsModel = request.resourceRequest.asset as EventsModel;
            loadRequests.Remove(DatasheetType.Events);
            operation.completed -= OnLoadCompleted_EventsModel;
            foreach (Action<bool> callback in request.callbacks)
                callback(true);
        }

		private static EventsModel eventsModel = default;
		public static EventsModel EventsModel
        {
            get
            {
                if (eventsModel == null)
                    Initialize(DatasheetType.Events);

                return eventsModel;
            }
        }
		
        private static void Log(string message)
        {
            Debug.LogWarning(message);
        }
	}
	
    public struct LoadRequest
    {
        public readonly ResourceRequest resourceRequest;
        public readonly List<Action<bool>> callbacks;

        public LoadRequest(ResourceRequest resourceRequest, Action<bool> callback)
        {
            this.resourceRequest = resourceRequest;
            callbacks = new List<Action<bool>>() { callback };
        }
    }
}
