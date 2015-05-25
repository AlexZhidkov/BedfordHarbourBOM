﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace Core.Common.Extensions
{
    public static class MefExtensions
    {
        public static CompositionContainer Container;

        public static object GetExportedValueByType(this CompositionContainer container, Type type)
        {
            foreach (var PartDef in container.Catalog.Parts)
            {
                foreach (var ExportDef in PartDef.ExportDefinitions)
                {
                    if (ExportDef.ContractName == type.FullName)
                    {
                        var contract = AttributedModelServices.GetContractName(type);
                        var definition = new ContractBasedImportDefinition(contract, contract, null, ImportCardinality.ExactlyOne,
                                                                           false, false, CreationPolicy.Any);
                        return container.GetExports(definition).FirstOrDefault().Value;
                    }
                }
            }

            return null;
        }

        public static IEnumerable<object> GetExportedValuesByType(this CompositionContainer container, Type type)
        {
            foreach (var PartDef in container.Catalog.Parts)
            {
                foreach (var ExportDef in PartDef.ExportDefinitions)
                {
                    if (ExportDef.ContractName == type.FullName)
                    {
                        var contract = AttributedModelServices.GetContractName(type);
                        var definition = new ContractBasedImportDefinition(contract, contract, null, ImportCardinality.ExactlyOne,
                                                                           false, false, CreationPolicy.Any);
                        return container.GetExports(definition);
                    }
                }
            }

            return new List<object>();
        }

        public static T GetExportedValue<T>(this CompositionContainer container,
            Func<IDictionary<string, object>, bool> predicate)
        {
            foreach (var PartDef in container.Catalog.Parts)
            {
                foreach (var ExportDef in PartDef.ExportDefinitions)
                {
                    if (ExportDef.ContractName == typeof(T).FullName)
                    {
                        if (predicate(ExportDef.Metadata))
                            return (T)PartDef.CreatePart().GetExportedValue(ExportDef);
                    }
                }
            }
            return default(T);
        }

        public static T GetExportedValueByType<T>(this CompositionContainer container, string type)
        {
            foreach (var PartDef in container.Catalog.Parts)
            {
                foreach (var ExportDef in PartDef.ExportDefinitions)
                {
                    if (ExportDef.ContractName == type)
                        return (T)PartDef.CreatePart().GetExportedValue(ExportDef);
                }
            }
            return default(T);
        }
    }
}
