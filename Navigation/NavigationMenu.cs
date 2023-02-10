using Penguin.Navigation.Abstractions;
using Penguin.Security.Abstractions.Interfaces;
using System.Collections.Generic;

namespace Penguin.Cms.Modules.Core.Navigation
{
    /// <summary>
    /// A slim INavigationMenu implementation for systems where the Navigation Menu module is not used
    /// </summary>

    public class NavigationMenu : INavigationMenu<INavigationMenu>
    {
        /// <summary>
        /// Child navigation menu items for recursive menus
        /// </summary>
        public IList<INavigationMenu> Children { get; set; } = new List<INavigationMenu>();

        /// <summary>
        /// Navigation menu link target
        /// </summary>
        public string Href { get; set; } = string.Empty;

        /// <summary>
        /// Optional material icon name
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Navigation menu item name for reference purposes
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Ordinal int representing position in parent list
        /// </summary>
        public int Ordinal { get; set; }

        /// <summary>
        /// Parent navigation menu item, if not root
        /// </summary>
        public INavigationMenu? Parent { get; set; }

        /// <summary>
        /// Security group permissions list used to determine menu visibility
        /// </summary>
        public IList<ISecurityGroupPermission> Permissions { get; set; } = new List<ISecurityGroupPermission>();

        /// <summary>
        /// The display text for this navigation menu item
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// The tree path that leads to this navigation menu item, using Name
        /// </summary>
        public string Uri { get; set; } = string.Empty;
    }
}