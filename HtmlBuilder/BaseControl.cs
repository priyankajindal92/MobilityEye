namespace PriyankaFormBuilder.HtmlBuilder
{
    public abstract class BaseControl
    {
        public string Type { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Value { get; set; }
        public string ClassName { get; set; }
    }
}

