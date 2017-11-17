using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.EF6.Pagination.Models
{
    public class Sorting<T>
    {
        public LambdaExpression OrderBy { get; }
        public bool Descending { get; }
        public Type ColumnType { get; }
        private PropertyInfo ColumnProperty { get; }
        private Type EntityType { get; }

        public Sorting(string columnNameToOrderBy, bool descending)
        {
            EntityType = typeof(T);
            ColumnProperty = EntityType.GetProperty(columnNameToOrderBy);
            ColumnType = ColumnProperty.PropertyType;
            OrderBy = CreateOrderByExpression(EntityType, ColumnProperty);
            Descending = descending;
        }

        private static LambdaExpression CreateOrderByExpression(Type entityType, PropertyInfo columnProperty)
        {
            var parameter = Expression.Parameter(entityType, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, columnProperty);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            return orderByExpression;
        }

        public static Sorting<T> Create(string columnToOrderByName, bool descending) =>
            new Sorting<T>(columnToOrderByName, descending);
    }

    public class Ascending<T> : Sorting<T>
    {
        private Ascending(string columnNameToOrderBy) : base(columnNameToOrderBy, false) { }

        public static Ascending<T> Create(string columnNameToOrderBy) => new Ascending<T>(columnNameToOrderBy);
    }

    public class Descending<T> : Sorting<T>
    {
        private Descending(string columnNameToOrderBy) : base(columnNameToOrderBy, true) { }

        public static Descending<T> Create(string columnNameToOrderBy) => new Descending<T>(columnNameToOrderBy);
    }
}