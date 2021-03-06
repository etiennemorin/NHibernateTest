﻿using System.Collections.Generic;

namespace NHibernateTest
{
    public class Cat
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual char Sex { get; set; }

        public virtual decimal Weight { get; set; }

        public virtual string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }
    }

    public class Owner
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual Iesi.Collections.Generic.ISet<Cat> Cats { get; set; }
    }
}
