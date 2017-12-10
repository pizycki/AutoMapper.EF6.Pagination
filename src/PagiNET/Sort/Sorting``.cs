using System;
using System.Linq.Expressions;

namespace PagiNET.Sort
{
    public abstract class Sorting<T, K>
    {
        public readonly Expression<Func<T, K>> OrderBy;
        public readonly bool Descending;

        protected Sorting(Expression<Func<T, K>> column, bool descending = false)
        {
            OrderBy = column ?? throw new ArgumentNullException(nameof(column));
            Descending = descending;
        }
    }

    public class Ascending<TEntity, TSort> : Sorting<TEntity, TSort>
    {
        public Ascending(Expression<Func<TEntity, TSort>> column) : base(column) { }

        public static Ascending<TEntity, TSort> By(Expression<Func<TEntity, TSort>> column) => new Ascending<TEntity, TSort>(column);
    }


    public class Descending<TEntity, TSort> : Sorting<TEntity, TSort>
    {
        public Descending(Expression<Func<TEntity, TSort>> column) : base(column, true) { }

        public static Descending<TEntity, TSort> By(Expression<Func<TEntity, TSort>> orderBy) => new Descending<TEntity, TSort>(orderBy);
    }
}