﻿using System;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.MSSqlServer.Sinks.MSSqlServer.Options;

namespace Serilog.Sinks.MSSqlServer.Configuration
{
    internal class MicrosoftExtensionsSinkOptionsProvider : IMicrosoftExtensionsSinkOptionsProvider
    {
        public SinkOptions ConfigureSinkOptions(SinkOptions sinkOptions, IConfigurationSection config)
        {
            if (config == null)
            {
                return sinkOptions;
            }

            ReadTableOptions(config, sinkOptions);
            ReadBatchSettings(config, sinkOptions);
            ReadAzureManagedIdentitiesOptions(config, sinkOptions);

            return sinkOptions;
        }

        private void ReadTableOptions(IConfigurationSection config, SinkOptions sinkOptions)
        {
            SetProperty.IfNotNull<string>(config["tableName"], val => sinkOptions.TableName = val);
            SetProperty.IfNotNull<string>(config["schemaName"], val => sinkOptions.SchemaName = val);
            SetProperty.IfNotNull<bool>(config["autoCreateSqlTable"], val => sinkOptions.AutoCreateSqlTable = val);
        }

        private void ReadBatchSettings(IConfigurationSection config, SinkOptions sinkOptions)
        {
            SetProperty.IfNotNull<int>(config["batchPostingLimit"], val => sinkOptions.BatchPostingLimit = val);
            SetProperty.IfNotNull<string>(config["batchPeriod"], val => sinkOptions.BatchPeriod = TimeSpan.Parse(val, CultureInfo.InvariantCulture));
        }

        private void ReadAzureManagedIdentitiesOptions(IConfigurationSection config, SinkOptions sinkOptions)
        {
            SetProperty.IfNotNull<bool>(config["useAzureManagedIdentity"], val => sinkOptions.UseAzureManagedIdentity = val);
            SetProperty.IfNotNull<string>(config["azureServiceTokenProviderResource"], val => sinkOptions.AzureServiceTokenProviderResource = val);
        }
    }
}
