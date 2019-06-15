using System;

namespace Sandbox.HelperUtils
{
    public class TagAttribute : Attribute
    {
        public TagAttribute(Category category)
        {
            Category = category;
        }

        public Category Category { get; set; }
    }
}
