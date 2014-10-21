using System;
using NHibernate;
using NHibernate.Engine;
using NHibernate.Persister.Entity;
using NHibernate.Proxy;

namespace NHibernateTest
{
    public static class SessionExtensions
    {
        public static Boolean IsDirtyEntity(this ISession session, Object entity)
        {
            String className = NHibernateProxyHelper.GuessClass(entity).FullName;
            ISessionImplementor sessionImpl = session.GetSessionImplementation();
            IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);
            EntityEntry oldEntry = sessionImpl.PersistenceContext.GetEntry(entity);
 
            if ((oldEntry == null) && (entity is INHibernateProxy))
            {
                INHibernateProxy proxy = entity as INHibernateProxy;
                Object obj = sessionImpl.PersistenceContext.Unproxy(proxy);
                oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);
            }
 
            Object [] oldState = oldEntry.LoadedState;
            Object [] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);
            Int32 [] dirtyProps = persister.FindDirty(currentState, oldState, entity, sessionImpl);
             
            return (dirtyProps != null);
        }
 
        public static Boolean IsDirtyProperty(this ISession session, Object entity, String propertyName)
        {
            String className = NHibernateProxyHelper.GuessClass(entity).FullName;
            ISessionImplementor sessionImpl = session.GetSessionImplementation();
            IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);
            EntityEntry oldEntry = sessionImpl.PersistenceContext.GetEntry(entity);
 
            if ((oldEntry == null) && (entity is INHibernateProxy))
            {
                INHibernateProxy proxy = entity as INHibernateProxy;
                Object obj = sessionImpl.PersistenceContext.Unproxy(proxy);
                oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);
            }
 
            Object [] oldState = oldEntry.LoadedState;
            Object [] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);
            Int32 [] dirtyProps = persister.FindDirty(currentState, oldState, entity, sessionImpl);
            Int32 index = Array.IndexOf(persister.PropertyNames, propertyName);
 
            Boolean isDirty = (dirtyProps != null) ? (Array.IndexOf(dirtyProps, index) != -1) : false;
 
            return (isDirty);
        }
 
        public static Object GetOriginalEntityProperty(this ISession session, Object entity, String propertyName)
        {
            String className = NHibernateProxyHelper.GuessClass(entity).FullName;
            ISessionImplementor sessionImpl = session.GetSessionImplementation();
            IEntityPersister persister = sessionImpl.Factory.GetEntityPersister(className);
            EntityEntry oldEntry = sessionImpl.PersistenceContext.GetEntry(entity);
 
            if ((oldEntry == null) && (entity is INHibernateProxy))
            {
                INHibernateProxy proxy = entity as INHibernateProxy;
                Object obj = sessionImpl.PersistenceContext.Unproxy(proxy);
                oldEntry = sessionImpl.PersistenceContext.GetEntry(obj);
            }
 
            Object [] oldState = oldEntry.LoadedState;
            Object [] currentState = persister.GetPropertyValues(entity, sessionImpl.EntityMode);
            Int32 [] dirtyProps = persister.FindDirty(currentState, oldState, entity, sessionImpl);
            Int32 index = Array.IndexOf(persister.PropertyNames, propertyName);
 
            Boolean isDirty = (dirtyProps != null) ? (Array.IndexOf(dirtyProps, index) != -1) : false;
 
            return ((isDirty) ? oldState [ index ] : currentState [ index ]);
        }
    }
//- See more at: http://weblogs.asp.net/ricardoperes/finding-dirty-properties-in-nhibernate#sthash.JnWL5Bt0.dpuf
}
