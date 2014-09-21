using System;
using System.Collections.Generic;
using Controls.Types;

namespace Controls.IDGeneration
{
    public partial class IDRange : Persistable, IAggregate<IDRangeId>, IConvertable<IDRange>
    {
        public IDRangeId Id { get; private set; }

        public String Key
        {
            get
            {
                return this.Id.Key;
            }
        }

        public Int64 StartRange
        {
            get;
            set;
        }

        public Int64 EndRange
        {
            get;
            set;
        }

        public IDRange(IDRangeId idRangeID)
        {
            this.Id = idRangeID;
        }

        public IDRange()
        {
            this.Modified();
        }

        IEnumerable<IPersistable> IConvertable<IDRange>.ToPersistables(IDRange aggregate)
        {
            return new List<IPersistable>() { aggregate as IPersistable };
        }
    }
}