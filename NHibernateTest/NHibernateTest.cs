using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Linq;

namespace NHibernateTest
{
    [TestClass]
    public class NHibernateTest
    {

        [TestMethod]
        public void SaveOwner()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var owner = new Owner { Name = "Bookworm" };
                    session.Save(owner);
                    transaction.Commit();
                }
            }
        }

        [TestMethod]
        public void SaveCat()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var owner = new Owner { Name = "Yuppy" };
                    var cat = new Cat {Name = "Fat", Sex = 'F', Weight = new Random().Next(1, 10)/1.0m, Owner = owner};
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


        [TestMethod]
        public void UpdateCats()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                var cats = session.Query<Cat>().ToList();
                Assert.IsTrue(cats.Count > 0);

                var cat = cats.First();

                cat.Weight += 10;
                
                session.SaveOrUpdate(cat);
                session.Flush();
            }
        }

        [TestMethod]
        public void FindOwnerByName()
        {
            using (ISession session = NHibernateSession.OpenSession())
            {
                //var results = session.Query<Owner>().Where(o => o.Name == "Bookworm").ToList();  //Or this works too

                var query = session.CreateQuery("from Owner where Name = :name");

                query.SetParameter("name", "bookworm");

                var results = query.List<Owner>();

                Assert.IsTrue(results.ToList().Count > 0);
            }
        }
    }
}
