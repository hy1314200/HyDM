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

        System.Collections.IList GetAll(Type type);
        System.Collections.IList GetAll(Type type, params string[] sortProperties);
        System.Collections.IList GetByParams(string hql, System.Collections.Hashtable paramlist);
        System.Collections.IList GetObjectByCondition(string hql);
        object GetObjectById(Type type, object id);

        bool Initialize(object proxy);
        void LockObject(object obj, enumLockMode lockMode);
        void RefreshObject(object obj, enumLockMode lockMode);

        void DeleteObject(object obj);
        void SaveObject(object obj);
        void UpdateObject(object obj);

    }
}
