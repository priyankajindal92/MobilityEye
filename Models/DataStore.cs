using System;
using System.Collections.Generic;

namespace PriyankaFormBuilder.Models
{
    public static class DataStore
    {
        static Dictionary<string, object> _dataStore;
        static DataStore() => _dataStore = _dataStore ?? new Dictionary<string, object>();

        public static Dictionary<string, object> GetItems()
        {
            try
            {
                return _dataStore ?? new Dictionary<string, object>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static object GetItemByKey(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
                return _dataStore[key];
            else
                throw new KeyNotFoundException();
        }

        public static void Save(string key, object data)
        {
            try
            {
                if (_dataStore == null)
                    _dataStore = new Dictionary<string, object>();

                if (!_dataStore.ContainsKey(key))
                    _dataStore[key] = data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
