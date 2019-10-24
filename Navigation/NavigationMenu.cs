using Penguin.Navigation.Abstractions;
using Penguin.Security.Abstractions.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Cms.Modules.Core.Navigation
{
    public class NavigationMenu : INavigationMenu<INavigationMenu>
    {
        public IList<INavigationMenu> Children { get; set; } = new List<INavigationMenu>();
        public string Href { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Ordinal { get; set; }
        public INavigationMenu? Parent { get; set; }
        public IList<ISecurityGroupPermission> Permissions { get; set; } = new List<ISecurityGroupPermission>();
        public string Text { get; set; } = string.Empty;
        public string Uri { get; set; } = string.Empty;
    }
}