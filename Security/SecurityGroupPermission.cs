using Penguin.Security.Abstractions;
using Penguin.Security.Abstractions.Interfaces;

namespace Penguin.Cms.Modules.Core.Security
{
    public class SecurityGroupPermission : ISecurityGroupPermission
    {
        public ISecurityGroup SecurityGroup { get; set; }

        public PermissionTypes Type { get; set; }
    }
}