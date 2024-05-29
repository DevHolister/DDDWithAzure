using Linde.Domain.Coaching.Common;
using Linde.Domain.Coaching.MenuAggregate.ValueObjects;
using Linde.Domain.Coaching.PermissionAggregate;
using Linde.Domain.Coaching.PermissionAggregate.ValueObjects;
using Linde.Domain.Coaching.UserAggreagate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Linde.Domain.Coaching.MenuAggregate
{
    public sealed class Menu : Entity<MenuId>
    {        
        public string Name { get; private set; }
        public string? Icon { get; private set; }
        public string Path { get; private set; }
        public int Level { get; private set; }
        public MenuId? ParentId { get; private set; }
        public bool ContainsChildren { get; private set; }
        public PermissionId? PermissionId { get; set; }
        public Menu Attribute { get; set; }
        public Permission Permissions { get; set; }
        public List<Menu> Attributes { get; set; } = new List<Menu>();
        private Menu() { }
        private Menu(
        MenuId menuId,
        string name,
        string path,
        int level,
        Guid parentId,
        bool containsChildren,
        Guid permissionId) : base(menuId)
        {
            Name = name;
            Path = path;
            Level = level;
            ParentId = MenuId.Create(parentId);
            ContainsChildren = containsChildren;
            PermissionId = PermissionId.Create(permissionId);
        }
    }
}
