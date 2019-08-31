﻿using System;
using System.Collections.Generic;

using KenticoInspector.Core.Models;
using KenticoInspector.Core.Modules;
using KenticoInspector.Core.Services.Interfaces;

namespace KenticoInspector.Core
{
    public abstract class AbstractReport<T> : IReport, IWithModuleMetadata<T> where T : new()
    {
        protected readonly IModuleMetadataService reportMetadataService;

        private ModuleMetadata<T> metadata;

        public AbstractReport(IModuleMetadataService reportMetadataService)
        {
            this.reportMetadataService = reportMetadataService;
        }

        public string Codename => GetCodename(this.GetType());

        public abstract IList<Version> CompatibleVersions { get; }

        public virtual IList<Version> IncompatibleVersions => new List<Version>();

        public ModuleMetadata<T> Metadata
        {
            get
            {
                return metadata ?? (metadata = reportMetadataService.GetModuleMetadata<T>(Codename));
            }
        }

        public abstract IList<string> Tags { get; }

        public static string GetCodename(Type reportType)
        {
            return GetDirectParentNamespace(reportType);
        }

        public abstract ReportResults GetResults();

        private static string GetDirectParentNamespace(Type reportType)
        {
            var fullNameSpace = reportType.Namespace;
            var indexAfterLastPeriod = fullNameSpace.LastIndexOf('.') + 1;
            return fullNameSpace.Substring(indexAfterLastPeriod, fullNameSpace.Length - indexAfterLastPeriod);
        }
    }
}