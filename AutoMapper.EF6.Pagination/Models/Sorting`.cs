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
        private Type EntityType { get; }

        public Sorting(string orderByColumnName, bool descending = false)
        {
            EntityType = typeof(T);
            ColumnType = GetColumnType(EntityType, orderByColumnName);
            OrderBy = CreateOrderByExpression(EntityType, ColumnType);
            Descending = descending;
        }

        private static Type GetColumnType(Type entityType, string orderByPropertyName)
        {
            return entityType.GetProperty(orderByPropertyName).PropertyType;
        }

        private static LambdaExpression CreateOrderByExpression(Type entityType, MemberInfo columnToOrderByType)
        {
            var parameter = Expression.Parameter(entityType, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, columnToOrderByType);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            return orderByExpression;
        }
    }

}
