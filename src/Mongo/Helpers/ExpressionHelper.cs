using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Mongo.Helpers
{
    
        public static class ExpressionHelper
        {
            public static string GetPropertyName<T>(this Expression<Func<T, object>> property) 
            {
                MemberExpression memberExpression;

                var unaryExpression = property.Body as UnaryExpression;

                if (unaryExpression != null)
                {
                    memberExpression = (MemberExpression)unaryExpression.Operand;
                }
                else
                {
                    memberExpression = (MemberExpression)property.Body;
                }

                return ((PropertyInfo)memberExpression.Member).Name;
            }
        }
   
}
