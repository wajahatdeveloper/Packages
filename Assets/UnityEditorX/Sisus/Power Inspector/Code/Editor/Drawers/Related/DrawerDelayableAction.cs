﻿using System;

namespace Sisus
{
	public interface IDrawerDelayableAction
	{
		void InvokeIfInstanceReferenceIsValid();
	}

	/// <summary>
	/// Action targeting a specific Drawer target.
	/// Because Drawer might get Disposed at any moment if the inspected targets of an inspector
	/// are changed, we need to check whether a Drawer target still exists right before a delayed
	/// action is about to be invoked.
	/// This can also handle instances where IDrawer has been pooled before the action gets invoked.
	/// </summary>
	public struct DrawerDelayableAction : IDrawerDelayableAction
	{
		private readonly DrawerTarget targetInstance;
		private readonly Action action;

		public DrawerDelayableAction(IDrawer targetDrawer, Action delayedAction)
		{
			targetInstance = new DrawerTarget(targetDrawer);
			action = delayedAction;
		}

		public void InvokeIfInstanceReferenceIsValid()
		{
			if(HasValidInstanceReference())
			{
				Invoke();
			}
		}

		private bool HasValidInstanceReference()
		{
			return targetInstance.HasValidInstanceReference();
		}

		private void Invoke()
		{
			// Make sure that active inspector is pointing to correct one.
			// Only rely on IGameObjectDrawer.Inspector, since BaseDrawer.Inspector
			// actually just uses ActiveManager.ActiveInspector internally.
			var drawer = targetInstance.Target;
			do
			{
				var go = drawer as IGameObjectDrawer;
				if(go != null)
				{
					InspectorUtility.ActiveManager.ActiveInspector = go.Inspector;
				}
				drawer = drawer.Parent;
			}
			while(drawer != null);


			if(action != null)
			{
				action();
			}
		}
	}

	/// <summary>
	/// Action targeting a specific Drawer target.
	/// Because Drawer might get Disposed at any moment if the inspected targets of an inspector
	/// are changed, we need to check whether a Drawer target still exists right before a delayed
	/// action is about to be invoked.
	/// This can also handle instances where IDrawer has been pooled before the action gets invoked.
	/// </summary>
	public struct DrawerDelayableTargetedAction : IDrawerDelayableAction
	{
		private readonly DrawerTarget targetInstance;
		private readonly Action<IDrawer> action;

		public DrawerDelayableTargetedAction(IDrawer targetDrawer, Action<IDrawer> delayedAction)
		{
			targetInstance = new DrawerTarget(targetDrawer);
			action = delayedAction;
		}

		public void InvokeIfInstanceReferenceIsValid()
		{
			if(HasValidInstanceReference())
			{
				Invoke();
			}
		}

		private bool HasValidInstanceReference()
		{
			return targetInstance.HasValidInstanceReference();
		}

		private void Invoke()
		{
			if(action != null)
			{
				action(targetInstance.Target);
			}
		}
	}

	/// <summary>
	/// Action targeting a specific Drawer target.
	/// Because Drawer might get Disposed at any moment if the inspected targets of an inspector
	/// are changed, we need to check whether a Drawer target still exists right before a delayed
	/// action is about to be invoked.
	/// This can also handle instances where IDrawer has been pooled before the action gets invoked.
	/// </summary>
	public struct DrawerDelayableAction<T>
	{
		private readonly DrawerTarget targetInstance;
		private readonly Action<T> action;

		public DrawerDelayableAction(IDrawer targetDrawer, Action<T> delayedAction)
		{
			targetInstance = new DrawerTarget(targetDrawer);
			action = delayedAction;
		}

		public void InvokeIfInstanceReferenceIsValid(T parameter)
		{
			if(HasValidInstanceReference())
			{
				Invoke(parameter);
			}
		}

		public void Invoke(T parameter)
		{
			if(action != null)
			{
				action(parameter);
			}
		}

		public bool HasValidInstanceReference()
		{
			return targetInstance.HasValidInstanceReference();
		}

		public static implicit operator Action<T>(DrawerDelayableAction<T> delayable)
		{
			return delayable.InvokeIfInstanceReferenceIsValid;
		}
	}
}