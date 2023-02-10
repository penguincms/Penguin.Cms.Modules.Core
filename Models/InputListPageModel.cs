using Penguin.Reflection.Abstractions;
using Penguin.Reflection.Extensions;
using Penguin.Reflection.Serialization.Abstractions.Interfaces;
using Penguin.Reflection.Serialization.Abstractions.Wrappers;
using Penguin.Reflection.Serialization.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Cms.Modules.Core.Models
{
    public class InputListPageModel : InputListBase
    {
        public string SearchUrl { get; set; }

        public List<InputListOptionPageModel> SelectedItems { get; } = new List<InputListOptionPageModel>();

        public InputListPageModel(IList Value, string searchUrl)
        {
            if (Value is null)
            {
                throw new System.ArgumentNullException(nameof(Value));
            }

            SearchUrl = searchUrl;
            SelectedItems = new List<InputListOptionPageModel>();

            SourceType = new MetaType(-1)
            {
                CoreType = CoreType.Collection,
                CollectionType = new MetaTypeHolder(Value.GetType().GetCollectionType())
            };

            foreach (object thisItem in Value.Cast<object>())
            {
                InputListOptionPageModel optionModel = new(thisItem)
                {
                    SourceType = SourceType,

                    ValuePropertyName = ValuePropertyName
                };

                SelectedItems.Add(optionModel);
            }
        }

        public InputListPageModel(IMetaObject Model, IMetaProperty labelProperty, IMetaProperty valueProperty, string searchUrl) : base(Model)
        {
            if (Model is null)
            {
                throw new System.ArgumentNullException(nameof(Model));
            }

            if (labelProperty is null)
            {
                throw new System.ArgumentNullException(nameof(labelProperty));
            }

            if (valueProperty is null)
            {
                throw new System.ArgumentNullException(nameof(valueProperty));
            }

            SearchUrl = searchUrl;

            LabelPropertyName = labelProperty.Name;
            ValuePropertyName = valueProperty.Name;

            SetUp(Model);
        }

        public InputListPageModel(IMetaObject Model, string labelProperty, string valueProperty, string searchUrl)
        {
            if (Model is null)
            {
                throw new System.ArgumentNullException(nameof(Model));
            }

            SearchUrl = searchUrl;

            LabelPropertyName = labelProperty;

            ValuePropertyName = valueProperty;

            SetUp(Model);
        }

        public InputListPageModel(IMetaObject Model, string searchUrl)
        {
            if (Model is null)
            {
                throw new System.ArgumentNullException(nameof(Model));
            }

            SearchUrl = searchUrl;

            SetUp(Model);
        }

        private void SetUp(IMetaObject Model)
        {
            BackingObject = Model;

            SourceType = Model.Type;

            if (Model.GetCoreType() != CoreType.Collection)
            {
                ItemType = Model.Type;
                if (!Model.Null)
                {
                    SelectedItems.Add(
                        new InputListOptionPageModel(LabelPropertyName, ValuePropertyName, Model)
                        {
                            ItemType = ItemType
                        });
                }
            }
            else if (Model.GetCoreType() == CoreType.Collection)
            {
                //Add all of the currently selected items to the display box
                ItemType = Model.Template.Type;
                foreach (IMetaObject thisItem in Model.CollectionItems)
                {
                    InputListOptionPageModel optionModel = new(LabelPropertyName, ValuePropertyName, thisItem)
                    {
                        ItemType = ItemType,
                        SourceType = SourceType
                    };

                    SelectedItems.Add(optionModel);
                }
            }
        }
    }
}