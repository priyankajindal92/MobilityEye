using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text;

namespace PriyankaFormBuilder.HtmlBuilder
{
    public class CheckBoxOrRadioButtonControl : BaseControl
    {
        public JArray Options { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<div class=\"form-group\"><label>{Label}</label><div class=\"form-check\">");
            foreach (string option in Options)
            {
                sb.Append($"<label class=\"{Type}-inline\" style=\"margin-right: 35px;\">");
                sb.Append(Type == "radio" ? $"<input class=\"{ClassName}\" name=\"radio_{Id}\" value=\"{option}\" type=\"{Type}\">" : $"<input name=\"check_{Id}\" class=\"{ClassName}\" value=\"{option}\" type=\"{Type}\">");
                sb.Append($"{option}</label>");
            }
            sb.Append("</div></div>");
            return sb.ToString();
        }
    }
}
