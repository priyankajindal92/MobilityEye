namespace PriyankaFormBuilder.HtmlBuilder
{
    public class TextBoxControl : BaseControl
    {
        public override string ToString()
        {
            return $"<div class=\"form-group\"><label for=\"txt_{Id}\">{Label}</label><input name=\"txt_{Name}\" class=\"{ClassName}\" id=\"txt_{Id}\" type=\"{Type}\"/></div>";
        }
    }
}
