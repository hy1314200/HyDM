using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NHibernate;
using System.Reflection;
using System.Collections;
using NHibernate.Criterion;
using Define;

namespace Utility
{
	public class NhibernateHelper:Define.INhibernateHelper
	{
        private ISession m_Session = null;
        private ITransaction m_Transaction = null;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        internal NhibernateHelper(ISession session)
        {
            if (session == null)
                throw new Exception("NhiberateHelper初始化错误：传入的Session不能为null");

            this.m_Session = session;
            if (!session.IsOpen)
            {
                this.m_Session.SessionFactory.OpenSession();
            }
        }
        

        /// <summary>
        /// Flushes the current active NHibernate session.
        /// </summary>
        public void Flush()
        {
            if (this.m_Session != null && this.m_Session.IsOpen)
            {
                this.m_Session.Flush();
            }
        }
        /// <summary>
        /// 关闭Nhibernate连接
        /// </summary>
        public void Close()
        {
            if (this.m_Session != null)
            {
                if (this.m_Session.IsOpen)
                {
                    this.m_Session.Close();
                }
                this.m_Session.Dispose();              
            }
        }
        public void Reconnect()
        {
            if (this.m_Session != null)
                this.m_Session.Reconnect();
        }


        /// <summary>
        /// 开启事务,为批量更新做处理
        /// </summary>
        /// <returns></returns>
        public void StartTransaction()
        {
            if (m_Transaction == null || m_Transaction.WasCommitted || m_Transaction.WasRolledBack)
                m_Transaction = this.m_Session.BeginTransaction();           
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            if (m_Transaction != null && m_Transaction.IsActive)
            {
                m_Transaction.Commit();
                m_Transaction = null;
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (m_Transaction != null && m_Transaction.IsActive)
            {
                m_Transaction.Rollback();
                m_Transaction = null;
            }
        }
        

        /// <summary>
        /// Generic method for retrieving single objects by primary key.
        /// </summary>
        /// <param name="type">The type of the object to fetch.</param>
        /// <param name="id">The identifier of the object.</param>
        /// <returns></returns>
        public object GetObjectById(Type type, object id)
        {
            return this.m_Session.Get(type, id);
        }
        
        /// <summary>
        /// Get all objects of a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList GetAll(Type type)
        {
            return GetAll(type, null);
        }

        /// <summary>
        /// Get all objects of a given type and add one or more names of properties to sort on.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sortProperties"></param>
        /// <remarks>Sorting is Ascending order. Construct a specific query/method when the sort order
        /// should be different.</remarks>
        /// <returns></returns>
        public IList GetAll(Type type, params string[] sortProperties)
        {
            ICriteria crit = this.m_Session.CreateCriteria(type);
            if (sortProperties != null)
            {
                foreach (string sortProperty in sortProperties)
                {
                    crit.AddOrder(Order.Asc(sortProperty));
                }
            }
            return crit.List();
        }

        public IList GetByParams(string hql, Hashtable paramlist)
        {
            IQuery query = this.m_Session.CreateQuery(hql);
            if (paramlist != null)
                foreach (string ParamName in paramlist.Keys)
                {
                    query.SetParameter(ParamName, paramlist[ParamName]);
                }
            return query.List();
        }

        public IList GetObjectByCondition(String hql)
        {
            if (this.m_Session != null)
            {
                return this.m_Session.CreateQuery(hql).List();
            }
            else
            {
                throw new Exception("Nhibernate初始化错误：Session没有初始化。");
            }
        }
        
        
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="obj"></param>
        public void SaveObject(object obj)
        {
            //ITransaction trTemp = this.m_Session.BeginTransaction();
            //try
            //{
            //    // Try to find a UpdateTimestamp property and when found, set it to the current date/time.
            //    PropertyInfo pi = obj.GetType().GetProperty("UpdateTimestamp");
            //    if (pi != null)
            //    {
            //        pi.SetValue(obj, DateTime.Now, null);
            //    }
            //    this.m_Session.Save(obj);
            //    trTemp.Commit();
            //}
            //catch (Exception ex)
            //{
            //    trTemp.Rollback();
            //    throw ex;
            //}
            this.m_Session.Save(obj);                
        }
        
        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="obj"></param>
        public void UpdateObject(object obj)
        {
            this.m_Session.Update(obj);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteObject(object obj)
        {
            this.m_Session.Delete(obj);
        }


        /// <summary>
        /// 以LockMode关联数据对象，为数据对象处理做准备
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="lockMode"></param>
        public void LockObject(object obj, enumLockMode lockMode)
        {
            if (this.m_Session != null && this.m_Session.IsOpen)
            {
                switch (lockMode)
                {

                    case enumLockMode.None:
                        m_Session.Lock(obj, LockMode.None);
                        break;

                    case enumLockMode.Read:
                        m_Session.Lock(obj, LockMode.Read);
                        break;

                    case enumLockMode.Upgrade:
                        m_Session.Lock(obj, LockMode.Upgrade);
                        break;

                    case enumLockMode.UpgradeNoWait:
                        m_Session.Lock(obj, LockMode.UpgradeNoWait);
                        break;

                    case enumLockMode.Write:
                        m_Session.Lock(obj, LockMode.Write);
                        break;

                    default:
                        m_Session.Lock(obj, LockMode.Read);
                        break;
                }
            }
        }
        public void RefreshObject(object obj)
        {
            this.m_Session.Refresh(obj);
        }

        public void RefreshObject(object obj, enumLockMode lockMode)
        {
            if (this.m_Session != null && this.m_Session.IsOpen)
            {
                switch (lockMode)
                {
                    case enumLockMode.None:
                        m_Session.Refresh(obj, LockMode.None); 
                        break;

                    case enumLockMode.Read:
                        m_Session.Refresh(obj, LockMode.Read); 
                        break;

                    case enumLockMode.Upgrade:
                        m_Session.Refresh(obj, LockMode.Upgrade);
                        break;

                    case enumLockMode.UpgradeNoWait:
                        m_Session.Refresh(obj, LockMode.UpgradeNoWait);
                        break;

                    case enumLockMode.Write:
                        m_Session.Refresh(obj, LockMode.Write);
                        break;

                    default:
                        m_Session.Refresh(obj, LockMode.Read);
                        break;
                }
            }
        }
        public bool Initialize(object proxy)
        {
            try
            {
                if (this.m_Session != null && this.m_Session.IsOpen)
                {
                    if (!NHibernateUtil.IsInitialized(proxy))
                    {

                        NHibernateUtil.Initialize(proxy);
                        return true;
                    }
                }
            }catch
            {
             return false;
            }
            return false;
        }
    }
}
