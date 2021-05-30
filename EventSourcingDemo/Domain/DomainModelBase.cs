using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventSourcingDemo.Domain
{
    public abstract class DomainModelBase
    {
        public int Id { get; set; }
    }
}
