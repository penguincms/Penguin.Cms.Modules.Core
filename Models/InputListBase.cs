using Penguin.Reflection.Serialization.Abstractions.Interfaces;

namespace Penguin.Cms.Modules.Core.Models
{
    public class InputListBase
    {
        public IMetaObject? BackingObject { get; set; }

        public IMetaType? ItemType { get; set; }

        public string LabelPropertyName { get; set; } = string.Empty;

        public IMetaType? SourceType { get; set; }

        public string ValuePropertyName { get; set; } = string.Empty;

        public InputListBase(IMetaObject backingObject)
        {
            BackingObject = backingObject;
        }

        public InputListBase()
        {
        }
    }
}