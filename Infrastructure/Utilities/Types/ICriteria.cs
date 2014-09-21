using System;

namespace Controls.Types
{
    /// <summary>
    /// Implemented for every Read
    /// </summary>
    public interface ICriteria
    {
        /// <summary>
        /// Used to tie the left and right node if the current node is root node
        /// </summary>
        Glue Glue { get; }

        /// <summary>
        /// Used to tie the query and value
        /// </summary>
        Operator Operator { get; }

        /// <summary>
        /// Left Child of the current node
        /// </summary>
        ICriteria Left { get; }

        /// <summary>
        /// Query Name in case of child
        /// </summary>
        IComparable Query { get; }

        /// <summary>
        /// Right Child of the current node
        /// </summary>
        ICriteria Right { get; }

        /// <summary>
        /// Value in case of child
        /// </summary>
        IComparable Value { get; }
    }

    /// <summary>
    /// Used for tying left and right nodes
    /// </summary>
    public enum Glue : short
    {
        Invalid = 0,
        None,
        And,
        Or
    }
}