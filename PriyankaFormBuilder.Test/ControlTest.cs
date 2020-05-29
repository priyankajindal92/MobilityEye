using PriyankaFormBuilder.HtmlBuilder;
using Xunit;

namespace PriyankaFormBuilder.PriyankaFormBuilder.Test
{
    public class ControlTest
    {
        [Fact]
        public void Test_TextBoxControl()
        {
            BaseControl ctrl = new TextBoxControl()
            {
                Type = "text",
                Label = "Hello",
                ClassName = "form-control",
                Id = int.MinValue.ToString(),
                Name = int.MinValue.ToString()
            };

            var actualResult = ctrl.ToString();
            string expectedResult = "<div class=\"form-group\"><label for=\"txt_0\">Hello</label><input name=\"txt_0\" class=\"form-control\" id=\"txt_0\" type=\"text\"/></div>";

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Test_ButtonControl()
        {
            BaseControl ctrl = new ButtonControl()
            {
                Type = "submit",
                Value = "Save",
                ClassName = "btn",
            };

            var actualResult = ctrl.ToString();
            string expectedResult = "<input type=\"submit\" class=\"btn\" value=\"Save\">";
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
