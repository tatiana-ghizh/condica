using CVU.CONDICA.Dto.Enums;
using System.Collections;
using System.Reflection;
using System.Text;

namespace CVU.CONDICA.Client.Services
{
    public static class Extensions
    {
        public static string ToQueryStringg(this object obj)
        {
            StringBuilder builder = new StringBuilder();
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object propertyValue = property.GetValue(obj, null);
                if (propertyValue != null)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("&");
                    }
                    builder.Append(GetPropertyValueAsQueryString(property.Name, propertyValue));
                }
            }
            return builder.ToString();
        }

        public static string To24HourStringFormat(this DateTime source)
        {
            return source.ToString("dd/MM/yyyy HH:mm");
        }

        public static string ToShortString(this DateTime source)
        {
            return source.ToString("dd/MM/yyyy");
        }

        public static string ToDisplayName(this Enum model)
        {
            string enumString = "";

            switch (model)
            {
                case Role:
                    enumString = RoleDisplayName((Role)model);
                    break;
                case VacationType:
                    enumString = VacationTypeDisplayName((VacationType)model);
                    break;
                case VacationStatus:
                    enumString = VacationStatusDisplayName((VacationStatus)model);
                    break;
                default:
                    enumString = model.ToString().SplitCamelCase();
                    break;
            }

            return enumString;
        }
        public static string SplitCamelCase(this string inputString)
        {
            return inputString.Aggregate(string.Empty, (result, next) =>
            {
                if (char.IsUpper(next) && result.Length > 0)
                {
                    result += ' ';
                }
                return result + next;
            });
        }

        private static string RoleDisplayName(Role role)
        {
            switch (role)
            {
                case Role.Administrator:
                    return "Administrator";
                case Role.Member:
                    return "Membru";
            }

            return "";
        }

        private static string VacationTypeDisplayName(VacationType vacationType)
        {
            switch (vacationType)
            {
                case VacationType.Anual:
                    return "Anual";
                case VacationType.UndpaidLeave:
                    return "Neplătit";
                case VacationType.StudyLeave:
                    return "De studii";
                case VacationType.SickLeave:
                    return "Pe foaie de boală";
                case VacationType.MaternityLeave:
                    return "De maternitate";
            }

            return "";
        }

        private static string VacationStatusDisplayName(VacationStatus vacationStatus)
        {
            switch (vacationStatus)
            {
                case VacationStatus.Approved:
                    return "Aprobat";
                case VacationStatus.Rejected:
                    return "Refuzat";
                case VacationStatus.Pending:
                    return "În așteptare";

            }

            return "";
        }

        private static string GetPropertyValueAsQueryString(string propertyName, object propertyValue)
        {
            StringBuilder builder = new StringBuilder();
            if (propertyValue is IEnumerable && !(propertyValue is string))
            {
                IEnumerable enumerable = (IEnumerable)propertyValue;
                foreach (object item in enumerable)
                {
                    if (builder.Length > 0)
                    {
                        builder.Append("&");
                    }
                    builder.Append(GetPropertyValueAsQueryString(propertyName, item));
                }
            }
            else
            {
                builder.Append(Uri.EscapeDataString(propertyName));
                builder.Append("=");
                builder.Append(GetPropertyValueAsString(propertyValue));
            }
            return builder.ToString();
        }

        private static string GetPropertyValueAsString(object propertyValue)
        {
            if (propertyValue is bool)
            {
                return ((bool)propertyValue) ? "true" : "false";
            }
            else if (propertyValue is byte || propertyValue is short || propertyValue is int || propertyValue is long || propertyValue is decimal)
            {
                return propertyValue.ToString();
            }
            else if (propertyValue is float || propertyValue is double)
            {
                return ((float)propertyValue).ToString("R");
            }
            else if (propertyValue is DateTime)
            {
                return ((DateTime)propertyValue).ToString("O");
            }
            else
            {
                return Uri.EscapeDataString(propertyValue.ToString());
            }
        }
    }
}
