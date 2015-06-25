using System;
using Bom.Client.Contracts;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class ComponentEventArgs : EventArgs
    {
        public SubassemblyData Component { get; set; }
        public bool IsNew { get; set; }

        public ComponentEventArgs(SubassemblyData component, bool isNew)
        {
            Component = component;
            IsNew = isNew;
        }
    }
}
