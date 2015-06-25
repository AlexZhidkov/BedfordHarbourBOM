using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Bom.Client.Entities;
using Core.Common.ServiceModel;
using FluentValidation;

namespace Bom.Client.Contracts
{
    public class SubassemblyData : BaseEntity
    {
        private int _assemblyId;
        private int _subassemblyId;
        private string _partDescription;
        private int _costContribution;
        private int _count;

        public int AssemblyId
        {
            get { return _assemblyId; }
            set
            {
                if (_assemblyId == value) return;
                _assemblyId = value;
                OnPropertyChanged(() => AssemblyId);
            }
        }
        public int SubassemblyId
        {
            get { return _subassemblyId; }
            set
            {
                if (_subassemblyId == value) return;
                _subassemblyId = value;
                OnPropertyChanged(() => SubassemblyId);
            }
        }

        public string PartDescription
        {
            get { return _partDescription; }
            set
            {
                if (_partDescription == value) return;
                _partDescription = value;
                OnPropertyChanged(() => PartDescription);
            }
        }

        public int CostContribution
        {
            get { return _costContribution; }
            set
            {
                if (_costContribution == value) return;
                _costContribution = value;
                OnPropertyChanged(() => CostContribution);
            }
        }

        public int Count
        {
            get { return _count; }
            set
            {
                if (_count == value) return;
                _count = value;
                OnPropertyChanged(() => Count);
            }
        }

        class SubassemblyDataValidator : AbstractValidator<SubassemblyData>
        {
            public SubassemblyDataValidator()
            {
                RuleFor(obj => obj.PartDescription).NotEmpty();
                RuleFor(obj => obj.AssemblyId).GreaterThan(0);
                RuleFor(obj => obj.SubassemblyId).GreaterThan(0);
                RuleFor(obj => obj.CostContribution).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
                RuleFor(obj => obj.Count).GreaterThan(0);
            }
        }

        protected override IValidator GetValidator()
        {
            return new SubassemblyDataValidator();
        }
    }
}
