using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SistemaVentas.Helpers
{
    public static class Render
    {
        public static MvcHtmlString CustomEnumDropDownListForCategories<TModel, TEnum>(
  this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var values = Enum.GetValues(typeof(Enums.category)).Cast<TEnum>();

            var items =
                values.Select(
                   value =>
                   new SelectListItem
                   {
                       Text = GetEnumDescription(value),
                       Value = value.ToString(),
                       Selected = value.Equals(metadata.Model)
                   });
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            return htmlHelper.DropDownListFor(expression, items, attributes);
        }

        private static string GetEnumDescription<TEnum>(TEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }

    
}