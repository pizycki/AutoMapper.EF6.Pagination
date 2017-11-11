using System;
using System.Linq.Expressions;

namespace SampleDatabase
{
    public class Sorting<T, K>
    {
        public readonly Expression<Func<T, K>> OrderBy;
        public readonly bool Descending;

        protected Sorting(Expression<Func<T, K>> orderBy, bool descending = false)
        {
            OrderBy = orderBy ?? throw new ArgumentException(nameof(orderBy));
            Descending = descending;
        }
    }

    public class Ascending<TEntity, TSort> : Sorting<TEntity, TSort>
    {
        public Ascending(Expression<Func<TEntity, TSort>> orderBy) : base(orderBy) { }

        public static Ascending<TEntity, TSort> By(Expression<Func<TEntity, TSort>> orderBy) => new Ascending<TEntity, TSort>(orderBy);
    }


    public class Descending<TEntity, TSort> : Sorting<TEntity, TSort>
    {
        public Descending(Expression<Func<TEntity, TSort>> orderBy) : base(orderBy, true) { }

        public static Descending<TEntity, TSort> Create(Expression<Func<TEntity, TSort>> orderBy) => new Descending<TEntity, TSort>(orderBy);
    }
}