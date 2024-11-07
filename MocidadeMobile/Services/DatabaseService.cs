﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MocidadeMobile.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<DataTable> ExecuteQueryAsync(string query)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string query)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(query, connection))
                {
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}