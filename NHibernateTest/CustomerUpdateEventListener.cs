using NHibernate;
using NHibernate.Event;
using NHibernate.Event.Default;

namespace NHibernateTest
{
    public class CustomUpdateEventListener : DefaultSaveEventListener
    {
        protected override object PerformSaveOrUpdate(SaveOrUpdateEvent evt)
        {
            if (evt.Entity is Cat)
            {
                DetermineIfAPropertyIsDirty(evt.Session, evt.Entity as Cat);
            }

            return base.PerformSaveOrUpdate(evt);
        }

        internal virtual void DetermineIfAPropertyIsDirty(ISession session, Cat cat)
        {
            var isDirtyCat = session.IsDirtyEntity(cat);

            var Id = session.IsDirtyProperty(cat, "Id");
            var Name = session.IsDirtyProperty(cat, "Name");
            var Owner = session.IsDirtyProperty(cat, "Owner");
            var OwnerId = session.IsDirtyProperty(cat, "OwnerId");
            var Sex = session.IsDirtyProperty(cat, "Sex");
            var Weight = session.IsDirtyProperty(cat, "Weight");

            if (isDirtyCat || Id || Name || Owner || OwnerId || Sex || Weight)
                cat.Sex = 'c';
        }
    }
}
