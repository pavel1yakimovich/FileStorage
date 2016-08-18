using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Visitor<T> : ExpressionVisitor
    {
        ParameterExpression _parameter;

        public Visitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var memberName = node.Member.Name;
            //find property on type T (=PersonData) by name
            var otherMember = typeof(T).GetProperty(memberName);
            //visit left side of this expression p.Id this would be p
            var inner = Visit(node.Expression);
            return Expression.Property(inner, otherMember);
        }
    }
}
