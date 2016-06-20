using System;
using System.Collections.Generic;

namespace NoteApplication.Helper
{
	public sealed class Mediator
	{
		private readonly Dictionary<string, List<Action<object>>> callbacks 
			= new Dictionary<string, List<Action<object>>>();

		public static Mediator Instance { get; } = new Mediator();

		public void Register(string id, Action<object> action)
		{
			if (!callbacks.ContainsKey(id))
			{
				callbacks[id] = new List<Action<object>>();
			}

			callbacks[id].Add(action);
		}

		public void SendMessage(string id, object message)
		{
			callbacks[id].ForEach(action => action(message));
		}
	}
}
