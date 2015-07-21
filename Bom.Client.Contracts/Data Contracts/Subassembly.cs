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
    public class Subassembly : BaseEntity
    {
        private int _assemblyId;
        private int _subassemblyId;
        private string _partDescription;
        private decimal _costContribution;
        private decimal _inheritedCost;
        private int _capability;
        private decimal _demand;

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

        /// <summary>
        /// How many units (or lengths) of Subassembly used to build Assembly
        /// </summary>
        public decimal CostContribution
        {
            get { return _costContribution; }
            set
            {
                if (_costContribution == value) return;
                _costContribution = value;
                OnPropertyChanged(() => CostContribution);
            }
        }

        /// <summary>
        /// How much of the cost of the Assembly comes from the Subassembly in $.
        /// Value calculated from cost of the Subassembly multiplied by its CostContribution.
        /// </summary>
        public decimal InheritedCost
        {
            get { return _inheritedCost; }
            set
            {
                if (_inheritedCost == value) return;
                _inheritedCost = value;
                OnPropertyChanged(() => InheritedCost);
            }
        }

        /// <summary>
        /// How many Assemblies it is possible to build from current stock of the Subassembly
        /// </summary>
        public int Capability
        {
            get { return _capability; }
            set
            {
                if (_capability == value) return;
                _capability = value;
                OnPropertyChanged(() => Capability);
            }
        }

        /// <summary>
        /// How many Subassemblies need to build required number of Assemblies
        /// </summary>
        public decimal Demand
        {
            get { return _demand; }
            set
            {
                if (_demand == value) return;
                _demand = value;
                OnPropertyChanged(() => Demand);
            }
        }

        class SubassemblyDataValidator : AbstractValidator<Subassembly>
        {
            public SubassemblyDataValidator()
            {
                RuleFor(obj => obj.PartDescription).NotEmpty();
                RuleFor(obj => obj.AssemblyId).GreaterThan(0);
                RuleFor(obj => obj.SubassemblyId).GreaterThan(0);
                RuleFor(obj => obj.CostContribution).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
            }
        }

        protected override IValidator GetValidator()
        {
            return new SubassemblyDataValidator();
        }
    }
}
