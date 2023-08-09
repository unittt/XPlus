using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace ZEngine.Utility.State
{
	/// <summary>
	/// 状态器
	/// </summary>
	public sealed class StateMachine
	{
		/// <summary>
		/// 一般状态不会太多 默认最大容量为10
		/// </summary>
		private readonly Dictionary<Type, StateBase> _stateInstance = new();
		private StateBase _curState;
		
		/// <summary>
		/// 状态机持有者
		/// </summary>
		public Object Owner { get; private set; }

		public StateMachine(Object owner)
		{
			Owner = owner;
		}

		/// <summary>
		/// 更新状态机
		/// </summary>
		public void Update()
		{
			_curState?.OnUpdate();
		}

		/// <summary>
		/// 启动状态机
		/// </summary>
		public void Run<TState>() where TState : StateBase
		{
			Run(typeof(TState));
		}

		public void Run(Type state)
		{
			SwitchState(state);
		}

		public void AddState<T>()where T:StateBase
		{
			var state = Activator.CreateInstance<T>();
			AddState(state);
		}
		
		/// <summary>
		/// 加入状态
		/// </summary>
		/// <param name="state"></param>
		/// <exception cref="ArgumentNullException"></exception>
		private void AddState<T>(T state) where T:StateBase
		{
			if (state == null)
			{
				Debug.LogError("状态为空");
				return;
			}

			var stateType = state.GetType();

			if (!_stateInstance.ContainsKey(stateType))
			{
				_stateInstance.Add(stateType, state);
				state.Machine = this;
				state.OnInit();
			}
			else
			{
				Debug.LogError($"State node already existed : {stateType.FullName}");
			}
		}

		/// <summary>
		/// 移除结点
		/// </summary>
		/// <param name="state"></param>
		/// <typeparam name="T"></typeparam>
		public void RemoveState<T>() where T:StateBase
		{
			var stateType = typeof(T);
			if (!_stateInstance.TryGetValue(stateType, out var state)) return;
			_stateInstance.Remove(stateType);
			state.Machine = null;
				
			if (state == _curState)
			{
				_curState = null;
			}
		}

		/// <summary>
		/// 转换状态
		/// </summary>
		public void SwitchState<State>() where State : StateBase
		{
			SwitchState(typeof(State));
		}

		/// <summary>
		/// 切换状态
		/// </summary>
		/// <param name="nodeType"></param>
		public void SwitchState(Type stateType)
		{
			if (!_stateInstance.TryGetValue(stateType, out var state))
			{
				Debug.LogError($"Can not found state node : {stateType}");
				return;
			}

			if (_curState == state)return;
			
			_curState?.OnExit();
			_curState = state;
			_curState.OnEnter();
		}
	}
}