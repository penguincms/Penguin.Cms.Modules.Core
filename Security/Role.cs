using Penguin.Security.Abstractions.Interfaces;
using System;

namespace Penguin.Cms.Modules.Core.Security
{
    public class Role : IRole
    {
        public string Description { get; set; }
        public string ExternalId { get; set; }
        public Guid Guid { get; set; }
    }
}