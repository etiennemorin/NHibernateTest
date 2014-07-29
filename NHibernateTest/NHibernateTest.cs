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
                    var owner = new Owner { Name = "Bitch" };
                    var cat = new Cat {Name = "Princess", Sex = 'F', Weight = new Random(10).Next()/1.0f, Owner = owner};
                    session.Save(owner);
                    session.Save(cat);
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
