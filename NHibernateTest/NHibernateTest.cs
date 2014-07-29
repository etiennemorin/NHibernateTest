using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace NHibernateTest
{
    [TestClass]
    public class NHibernateTest
    {
        [TestMethod]
        public void SaveCat()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var princess = new Cat {Name = "Princess", Sex = 'F', Weight = new Random(10).Next() / 1.0f};
                    session.Save(princess);
                    transaction.Commit();
                }
            }
        }

        [TestMethod]
        public void GetCats()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var cats = session.Query<Cat>().ToList();
                Assert.IsTrue(cats.Count > 0);
            }
        }
    }
}
