using System.Linq.Expressions;

namespace DesafioTecnico.Business.Helpers
{
    internal static class ProtectedProperty
    {
        public static void SetValue<TClass>(TClass obj, Expression<Func<TClass, object>> field, object value)
        {
            MemberExpression member = default!;
            if (field.Body is UnaryExpression unary)
                member = (MemberExpression)unary.Operand;
            else
                member = (MemberExpression)field.Body;

            var propName = member.Member.Name;

            var prop = typeof(TClass).GetProperty(propName);
            prop!.SetValue(obj, value);
        }
    }
}