using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Define
{
    public enum enumLockMode
    {
        None=0,
        Read,
        Upgrade,
        UpgradeNoWait,
        Write
    }
    public interface INhibernateHelper
    {
        void StartTransaction();
        void CommitTransaction();
        void Rollback();

        IList<T> GetAll<T>();
        IList<T> GetAll<T>(params string[] sortProperties);
        IList<T> GetByParams<T>(string hql, System.Collections.Hashtable paramlist);
        IList<T> GetObjectByCondition<T>(string hql);
        object GetObjectById(Type type, object id);

        bool Initialize(object proxy);
        void LockObject(object obj, enumLockMode lockMode);
        void RefreshObject(object obj, enumLockMode lockMode);

        void DeleteObject(object obj);
        void SaveObject(object obj);
        void UpdateObject(object obj);

    }
}
