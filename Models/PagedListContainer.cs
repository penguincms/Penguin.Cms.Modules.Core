using System;
using System.Collections.Generic;

namespace Penguin.Cms.Modules.Core.Models
{
    public class PagedListContainer<T>
    {
        public int Count { get; set; }
        public int End => this.Start + this.Items.Count;
        public List<string> HiddenColumns { get; } = new List<string>();
        public List<T> Items { get; } = new List<T>();
        public int Page { get; set; }
        public Dictionary<string, string> PostbackParameters { get; } = new Dictionary<string, string>();
        public int Start => this.Page * this.Count;
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(this.TotalCount / (decimal)this.Count);
    }

    public class PagedListContainer : PagedListContainer<object>
    {
    }
}