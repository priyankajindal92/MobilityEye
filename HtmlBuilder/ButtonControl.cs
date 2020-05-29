namespace PriyankaFormBuilder.HtmlBuilder
{
    public class ButtonControl : BaseControl
    {
        public override string ToString()
        {
            return $"<input type=\"{Type}\" class=\"{ClassName}\" value=\"{Value}\">";
        }
    }
}
