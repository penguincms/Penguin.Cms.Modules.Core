using Penguin.Reflection.Abstractions;
using Penguin.Reflection.Extensions;
using Penguin.Reflection.Serialization.Abstractions.Interfaces;
using Penguin.Reflection.Serialization.Abstractions.Wrappers;
using Penguin.Reflection.Serialization.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

            this.SearchUrl = searchUrl;
            this.SelectedItems = new List<InputListOptionPageModel>();

            this.SourceType = new MetaType(-1)
            {
                CoreType = CoreType.Collection,
                CollectionType = new MetaTypeHolder(Value.GetType().GetCollectionType())
            };

            foreach (object thisItem in Value.Cast<object>())
            {
                InputListOptionPageModel optionModel = new InputListOptionPageModel(thisItem)
                {
                    SourceType = this.SourceType,

                    ValuePropertyName = this.ValuePropertyName
                };

                this.SelectedItems.Add(optionModel);
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

            this.SearchUrl = searchUrl;

            this.LabelPropertyName = labelProperty.Name;
            this.ValuePropertyName = valueProperty.Name;

            this.SetUp(Model);
        }

        public InputListPageModel(IMetaObject Model, string labelProperty, string valueProperty, string searchUrl)
        {
            if (Model is null)
            {
                throw new System.ArgumentNullException(nameof(Model));
            }

            this.SearchUrl = searchUrl;

            this.LabelPropertyName = labelProperty;

            this.ValuePropertyName = valueProperty;

            this.SetUp(Model);
        }

        public InputListPageModel(IMetaObject Model, string searchUrl)
        {
            if (Model is null)
            {
                throw new System.ArgumentNullException(nameof(Model));
            }

            this.SearchUrl = searchUrl;

            this.SetUp(Model);
        }

        private void SetUp(IMetaObject Model)
        {
            this.BackingObject = Model;

            this.SourceType = Model.Type;

            if (Model.GetCoreType() != CoreType.Collection)
            {
                this.ItemType = Model.Type;
                if (!Model.Null)
                {
                    this.SelectedItems.Add(
                        new InputListOptionPageModel(this.LabelPropertyName, this.ValuePropertyName, Model)
                        {
                            ItemType = this.ItemType
                        });
                }
            }
            else if (Model.GetCoreType() == CoreType.Collection)
            {
                //Add all of the currently selected items to the display box
                this.ItemType = Model.Template.Type;
                foreach (IMetaObject thisItem in Model.CollectionItems)
                {
                    InputListOptionPageModel optionModel = new InputListOptionPageModel(this.LabelPropertyName, this.ValuePropertyName, thisItem)
                    {
                        ItemType = this.ItemType,
                        SourceType = this.SourceType
                    };

                    this.SelectedItems.Add(optionModel);
                }
            }
        }
    }
}