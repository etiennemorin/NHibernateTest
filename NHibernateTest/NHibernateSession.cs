using NHibernate;
using NHibernate.Cfg;

namespace NHibernateTest
{
    public class NHibernateSession
    {
        public static ISession OpenSession()
        {
            var sessionFactory = new Configuration().Configure().BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }

}