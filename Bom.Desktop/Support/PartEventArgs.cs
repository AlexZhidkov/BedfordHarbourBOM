using System;
using Bom.Client.Contracts;
using Bom.Client.Entities;

namespace Bom.Desktop.Support
{
    public class PartEventArgs : EventArgs
    {
        public Part Part { get; set; }
        public bool IsNew { get; set; }

        public PartEventArgs(Part part, bool isNew)
        {
            Part = part;
            IsNew = isNew;
        }
    }
}
