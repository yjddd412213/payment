using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;


namespace System.Web.Mvc
{
    public static class InputExtensions
    {
        const string RADIOBUTTON_HTML_STR = "<li><input type='radio' name='{0}' value='{1}' {3} {4}>{2}</input><li>";

        public static MvcHtmlString RadioButtonsFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, SelectList selectList, string htmlAttr = "")
        {
            var memberExpression = (MemberExpression)expression.Body;
            var modelType = expression.Type;
            var name = memberExpression.Member.Name;
            var propertyInfo = (PropertyInfo)memberExpression.Member;
            StringBuilder sb = new StringBuilder();
            if (selectList != null)
            {
                if (html.ViewData.Model != null)
                {
                    var value = propertyInfo.GetValue(html.ViewData.Model, null);
                    if (selectList.Count(m => m.Value == value) != 0)
                    {
                        foreach (var item in selectList)
                        {
                            sb.Append(string.Format(RADIOBUTTON_HTML_STR, name, item.Value, item.Text, item.Value == value.ToString() ? "checked='true'" : string.Empty, htmlAttr));
                        }
                        return MvcHtmlString.Create(sb.ToString());
                    }
                }
                foreach (var item in selectList)
                {
                    sb.Append(string.Format(RADIOBUTTON_HTML_STR, name, item.Value, item.Text, item.Selected ? "checked='true'" : string.Empty, htmlAttr));
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}