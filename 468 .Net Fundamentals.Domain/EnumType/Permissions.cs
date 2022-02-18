using System;
using System.Collections.Generic;
using System.Text;

namespace _468_.Net_Fundamentals.Domain.EnumType
{
    public static class Permissions
    {
        public static IList<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
                {
                    $"Permissions.{module}.Create",
                    $"Permissions.{module}.View",
                    $"Permissions.{module}.Edit",
                    $"Permissions.{module}.Delete",
                };
        }

        public static class Projects
        {
            public const string View = "Permissions.Projects.View";
            public const string Create = "Permissions.Projects.Create";
            public const string Edit = "Permissions.Projects.Edit";
            public const string Delete = "Permissions.Projects.Delete";
        }

        public static class Tags
        {
            public const string View = "Permissions.Tags.View";
            public const string Create = "Permissions.Tags.Create";
            public const string Edit = "Permissions.Tags.Edit";
            public const string Delete = "Permissions.Tags.Delete";
        }

        public static class Businesses
        {
            public const string View = "Permissions.Businesses.View";
            public const string Create = "Permissions.Businesses.Create";
            public const string Edit = "Permissions.Businesses.Edit";
            public const string Delete = "Permissions.Businesses.Delete";
        }
    }
}
