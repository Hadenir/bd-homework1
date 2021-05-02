using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace AviationDb.Database
{
    public sealed class DatabaseProxy : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _currentTransaction;

        public static DatabaseProxy ConnectTcp(string username, string password, string address, int port)
        {
            var connectionString =
                $"Data Source=tcp:{address},{port};Initial Catalog=Aviation;User ID={username};Password={password}";
            var connection = new SqlConnection(connectionString);
            connection.Open();

            return new DatabaseProxy(connection);
        }
        
        public void Dispose()
        {
            _connection.Close();
        }

        public void BeginTransaction()
        {
            if (_currentTransaction != null) throw new InvalidOperationException("Transaction already began!");

            _currentTransaction = _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_currentTransaction == null) throw new InvalidOperationException("No transaction to commit!");

            _currentTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            if (_currentTransaction == null) throw new InvalidOperationException("No transaction to rollback!");

            _currentTransaction.Rollback();
        }

        public IEnumerable<IDataRecord> Query(string queryString, params SqlParameter[] parameters)
        {
            var cmd = CreateCommand(queryString, parameters);

            using var reader = cmd.ExecuteReader();
            foreach (var row in reader)
                yield return row as IDataRecord;
        }

        public int Execute(string cmdString, params SqlParameter[] parameters)
        {
            var cmd = CreateCommand(cmdString, parameters);

            return cmd.ExecuteNonQuery();
        }

        public T QueryScalar<T>(string cmdString, params SqlParameter[] parameters)
        {
            var cmd = CreateCommand(cmdString, parameters);

            return (T) cmd.ExecuteScalar();
        }
        
        private DatabaseProxy(SqlConnection connection)
        {
            _connection = connection;
        }

        private SqlCommand CreateCommand(string cmdString, params SqlParameter[] parameters)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = cmdString;
            cmd.Transaction = _currentTransaction;
            cmd.Parameters.AddRange(parameters);

            return cmd;
        }
    }
}