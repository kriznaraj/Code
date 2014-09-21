using System;

namespace Controls.Types
{
    public class Criteria : ICriteria
    {
        public Glue Glue
        {
            get;
            set;
        }

        public ICriteria Left
        {
            get;
            set;
        }

        public Operator Operator
        {
            get;
            set;
        }

        public IComparable Query
        {
            get;
            set;
        }

        public ICriteria Right
        {
            get;
            set;
        }

        public IComparable Value
        {
            get;
            set;
        }
    }
}