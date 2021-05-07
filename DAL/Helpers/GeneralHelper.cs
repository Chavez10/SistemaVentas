using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helpers
{
    public class GeneralHelper
    {
        public static string GetDescriptionFromEnumValue(Enum value)
        { DescriptionAttribute attribute = value.GetType().
                GetField(value.ToString()).
                GetCustomAttributes(typeof(DescriptionAttribute), false).
                SingleOrDefault() as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static T GetEnumValueFromDescription<T>(string description)
        {
            var type = typeof(T); if (!type.IsEnum) throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields.SelectMany(f => f.GetCustomAttributes(typeof(DescriptionAttribute), false),
                (f, a) => new { Field = f, Att = a }).Where(a => ((DescriptionAttribute)a.Att).Description == description).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }

        public static string EncriptarPassword(string pass)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var textSHA = sha1.ComputeHash(Encoding.UTF8.GetBytes(pass));
                var sb = new StringBuilder(textSHA.Length * 2);
                foreach (byte b in textSHA)
                {
                    sb.Append(b.ToString());
                }

                return sb.ToString();
            }
        }

    }
}
