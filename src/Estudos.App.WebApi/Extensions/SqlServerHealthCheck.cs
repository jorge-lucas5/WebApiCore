﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Estudos.App.WebApi.Extensions
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        private readonly string _connection;

        public SqlServerHealthCheck(string connection)
        {
            _connection = connection;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                await using var connection = new SqlConnection(_connection);
                await connection.OpenAsync(cancellationToken);

                var command = connection.CreateCommand();
                command.CommandText = "select count(id) from produto";

                return Convert.ToInt32(await command.ExecuteScalarAsync(cancellationToken)) > 0 ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy();
            }
            catch (Exception)
            {
                return HealthCheckResult.Unhealthy();
            }
        }
    }
}