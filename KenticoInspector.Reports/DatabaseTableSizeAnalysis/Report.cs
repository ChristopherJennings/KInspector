﻿using KenticoInspector.Core;
using KenticoInspector.Core.Constants;
using KenticoInspector.Core.Models;
using KenticoInspector.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;


namespace KenticoInspector.Reports.DatabaseTableSizeAnalysis
{
    public class Report: IReport
    {
        readonly IDatabaseService _databaseService;
        readonly IInstanceService _instanceService;

        public Report(IDatabaseService databaseService, IInstanceService instanceService)
        {
            _databaseService = databaseService;
            _instanceService = instanceService;
        }

        public string Codename => "database-table-size-analysis";

        public IList<Version> CompatibleVersions => new List<Version> {
            new Version("10.0"),
            new Version("11.0"),
            new Version("12.0")
        };

        public IList<Version> IncompatibleVersions => new List<Version>();

        public string LongDescription => @"";

        public string Name => "Database table size analysis";

        public string ShortDescription => "Displays top 25 biggest tables from the database.";

        public IList<string> Tags => new List<string> {
            ReportTags.Health
        };

        // Anything under here needs to be fixed.
        public ReportResults GetResults(Guid InstanceGuid)
        {
            var instance = _instanceService.GetInstance(InstanceGuid);
            var instanceDetails = _instanceService.GetInstanceDetails(instance);
            _databaseService.ConfigureForInstance(instance);
            
            var checkDbResults = _databaseService.ExecuteSqlFromFile<DatabaseTableSizeResult>(Scripts.GetDatabaseTableSize);

            return CompileResults(checkDbResults);
        }

        private static ReportResults CompileResults(IEnumerable<DatabaseTableSizeResult> checkDbResults)
        {
            //var hasIssues = checkDbResults.Count() > 0;

            return new ReportResults
            {
                Type = ReportResultsType.Table.ToString(),
                Status = ReportResultsStatus.Information.ToString(),
                Summary = "Check results table for any issues",
                Data = new TableResult<DatabaseTableSizeResult>() {
                    Name = "Top 25 Results",
                    Rows =  checkDbResults
                }
            };
        }
    }
}
