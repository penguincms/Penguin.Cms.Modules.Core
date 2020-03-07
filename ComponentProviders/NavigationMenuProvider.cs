using Penguin.Cms.Abstractions.Interfaces;
using Penguin.Cms.Modules.Core.Security;
using Penguin.Extensions.Collections;
using Penguin.Navigation.Abstractions;
using Penguin.Security.Abstractions;
using Penguin.Security.Abstractions.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Penguin.Cms.Modules.Core.ComponentProviders
{
    public abstract class NavigationMenuProvider : IProvideComponents<INavigationMenu, string>
    {
        public ISecurityGroupPermission CreatePermission(string RoleName, PermissionTypes PermissionType)
        {
            return new SecurityGroupPermission()
            {
                SecurityGroup = new Role()
                {
                    ExternalId = RoleName
                },
                Type = PermissionType
            };
        }

        public ISecurityGroupPermission CreatePermission(IRole Role, PermissionTypes PermissionType)
        {
            return new SecurityGroupPermission()
            {
                SecurityGroup = Role,
                Type = PermissionType
            };
        }

        public abstract INavigationMenu GenerateMenuTree();

        public IEnumerable<INavigationMenu> GetComponents(string MenuName)
        {
            INavigationMenu toReturn = this.Search(MenuName);

            if (toReturn is null)
            {
                yield break;
            }
            else
            {
                yield return toReturn;
            }
        }

        protected INavigationMenu Search(string Name)
        {
            List<INavigationMenu> toCheck = new List<INavigationMenu>
            {
                this.GenerateMenuTree()
            };

            while (toCheck.Any())
            {
                INavigationMenu checkMe = toCheck.First();

                if (checkMe.Name == Name)
                {
                    return checkMe;
                }
                else
                {
                    if (checkMe.Children.AnyNotNull())
                    {
                        toCheck.AddRange(checkMe.Children);
                    }

                    toCheck.RemoveAt(0);
                }
            }

            return null;
        }
    }
}