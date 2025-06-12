using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentUI.Generator
{
    public class MethodInfoComparer : EqualityComparer<MethodInfo>
    {
        public override bool Equals(MethodInfo x, MethodInfo y)
        {
            // Compare by declaring type, name, and parameters
            if (x == null || y == null)
                return false;

            if (x.Name != y.Name)
                return false;

            var xParams = x.GetParameters();
            var yParams = y.GetParameters();

            if (xParams.Length != yParams.Length)
                return false;

            return !xParams.Where((t, i) => t.ParameterType != yParams[i].ParameterType).Any();
        }

        public override int GetHashCode(MethodInfo obj)
        {
            unchecked
            {
                int hash = obj.DeclaringType?.GetHashCode() ?? 0;
                hash = (hash * 397) ^ (obj.Name?.GetHashCode() ?? 0);
                return obj.GetParameters().Aggregate(hash, (current, param) => (current * 397) ^ (param.ParameterType?.GetHashCode() ?? 0));
            }
        }
    }

}
