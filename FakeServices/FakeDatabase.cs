using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingDemo.Domain;

namespace EventSourcingDemo.FakeServices
{
    public abstract class FakeDatabase
    {
        private readonly Dictionary<string, Dictionary<int, object>> _allData = new();
        private readonly object _locker = new();

        public virtual T SaveData<T>(T data) where T : DomainModelBase
        {
            var dataset = GetDataSet<T>();
            int id;
            if (!dataset.TryGetValue(data.Id, out var existingData))
            {
                id = dataset.Values.Count > 0 ? dataset.Keys.LastOrDefault() + 1 : 1;
                data.Id = id;
                dataset.Add(id, data);
                return data;
            }

            id = (existingData as DomainModelBase).Id;
            dataset.Remove(id);
            data.Id = id;
            dataset.Add(data.Id, data);
            return data;
        }

        public virtual T GetData<T>(Func<object, bool> func) where T : DomainModelBase
        {
            var dataset = GetDataSet<T>();
            var data = dataset.Values.FirstOrDefault(func);
            return data as T;
        }

        public virtual bool DeleteData<T>(int id) where T : DomainModelBase
        {
            var dataset = GetDataSet<T>();
            return dataset.Remove(id);
        }


        public virtual IEnumerable<T> GetAllData<T>(Func<object, bool> func) where T : DomainModelBase
        {
            var dataset = GetDataSet<T>();
            return dataset.Values.Where(func).Select(w => w as T);
        }
        public virtual IEnumerable<T> GetAllData<T>() where T : DomainModelBase
        {
            var dataset = GetDataSet<T>();
            return dataset.Values.ToList().Select(w => w as T);
        }

        protected Dictionary<int, object> GetDataSet<T>() where T : DomainModelBase
        {
            string key = typeof(T).FullName;
            if (_allData.TryGetValue(key, out var dataSet))
            {
                return dataSet;
            }
            dataSet = new Dictionary<int, object>();

            lock (_locker)
            {
                if (_allData.TryGetValue(key, out var dataSet2))//try get again to ensure not locked and created by another thread
                {
                    return dataSet2;
                }
                _allData.Add(key, dataSet);
            }
            return dataSet;
        }
    }
}
