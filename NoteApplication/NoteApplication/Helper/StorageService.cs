using Newtonsoft.Json;
using NoteApplication.Interfaces;
using Windows.Storage;

namespace NoteApplication.Helper
{
	public class StorageService : IStorageService
	{
		private readonly ApplicationDataContainer appData = ApplicationData.Current.LocalSettings;

		public void Write<T>(string key, T value)
		{
			var json = JsonConvert.SerializeObject(value);
			appData.Values[key] = json;
		}

		public T Read<T>(string key)
		{
			return Read<T>(key, default(T));
		}

		public T Read<T>(string key, T defaultValue)
		{
			if (!appData.Values.ContainsKey(key))
			{
				return defaultValue;
			}
			var json = (string)appData.Values[key];
			return JsonConvert.DeserializeObject<T>(json);
		}
	}
}
