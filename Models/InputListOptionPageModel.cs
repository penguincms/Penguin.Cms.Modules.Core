﻿using Penguin.Reflection.Abstractions;
using Penguin.Reflection.Serialization.Abstractions.Interfaces;
using Penguin.Reflection.Serialization.Extensions;
using Penguin.Reflection.Serialization.Objects;

namespace Penguin.Cms.Modules.Core.Models
{
    public class InputListOptionPageModel : InputListBase
    {
        private bool setAnchor;

        /// <summary>
        /// The default value for the type represented by this option if not populated, otherwise the actual value of the object
        /// </summary>
        public string DefaultValue => Value ?? ValueProperty?.Type?.Default ?? string.Empty;

        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// The type of this individual selection option
        /// </summary>
        public System.Type? OptionType => BackingObject?.GetType();

        /// <summary>
        /// Dirty hack to determine if the embedded view calling this is the first outer wrapper in its
        /// hierarchy. This is used to figure out what the delete button should be referencing when removing a list item.
        /// </summary>
        public bool SetAnchor
        {
            get
            {
                bool first = !setAnchor;

                setAnchor = true;

                return first;
            }
        }

        public string Value { get; set; } = string.Empty;

        public CoreType? ValueCoreType => ValueProperty?.Type?.CoreType ?? CoreType.Value;

        /// <summary>
        /// A property representing the "key" for the object for use in complex associations
        /// </summary>
        public IMetaProperty? ValueProperty => ItemType?.GetProperty(ValuePropertyName);

        public InputListOptionPageModel(object? backingObject = null) : base(new MetaObject(-1) { Value = backingObject?.ToString() })
        {
            Value = BackingObject!.Value;
            Label = BackingObject!.Value;
        }

        public InputListOptionPageModel(string labelProperty, string valueProperty, IMetaObject backingObject) : base(backingObject)
        {
            if (backingObject is null)
            {
                throw new System.ArgumentNullException(nameof(backingObject));
            }

            LabelPropertyName = labelProperty;
            ValuePropertyName = valueProperty;
            SourceType = backingObject.Type;

            //If we cant find the value for the key on the object, we fall back to "ToString"
            //Generated by the serializer
            Value = backingObject.HasProperty(ValuePropertyName) ? backingObject[ValuePropertyName].Value : backingObject.Value;

            //If we cant find the Display property specified, we fall back on whatever we chose for the value
            Label = backingObject.HasProperty(LabelPropertyName) ? backingObject[LabelPropertyName].Value : Value;
        }

        public InputListOptionPageModel(string labelProperty, string valueProperty, IMetaType? sourceType = null)
        {
            LabelPropertyName = labelProperty;
            ValuePropertyName = valueProperty;
            SourceType = sourceType;
        }
    }
}