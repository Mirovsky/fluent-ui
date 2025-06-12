namespace FluentUI.Generator
{
    public static class GeneratorNamingUtils
    {
        public static string AdjustMethodNameToFluent(string name, string innerObjectName = "")
        {
            string methodName = FirstCharToUpper(name.Replace("Set", ""));
            if (!string.IsNullOrEmpty(innerObjectName))
            {
                methodName = FirstCharToUpper(innerObjectName) + methodName;
            }

            return methodName;
        }

        public static string GetPropertyName(string name, string innerObjectName)
        {
            return !string.IsNullOrEmpty(innerObjectName) ?
                $"{FirstCharToLower(innerObjectName)}.{name}" :
                name;
        }

        private static string FirstCharToUpper(string input) => input[0].ToString().ToUpper() + input[1..];

        private static string FirstCharToLower(string input) => input[0].ToString().ToLower() + input[1..];

        public static string GetPropertyFieldName(string input)
        {
            int dotIndex = input.IndexOf('.');
            if (dotIndex == -1)
            {
                return input;
            }

            return input[..dotIndex] + input[dotIndex + 1].ToString().ToUpper() + input[(dotIndex + 2)..];
        }
    }
}
