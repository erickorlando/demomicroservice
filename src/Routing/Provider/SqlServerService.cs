using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Utility;

namespace Provider
{
    public class SqlServerService
    {
        protected readonly string _cn;

        public SqlServerService(string cn) => _cn = cn;

        public async Task<DataTable> TransaccionAsync(string procedure, Parameter param)
        {
            return await TransaccionAsync(procedure,param,0);
        }

        public async Task<DataTable> TransaccionAsync(string procedure, Parameter param, int timeout)
        {
            using (SqlConnection connection = new SqlConnection(_cn))
            using (SqlCommand command = new SqlCommand(procedure, connection))
            {
                DataTable data = new DataTable();

                command.CommandType = CommandType.StoredProcedure;

                if (timeout > 0) { command.CommandTimeout = timeout; }
                if (param != null) { command.Parameters.Add(new SqlParameter(param.key, IsNull(param.value))); }

                connection.Open();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    data.Load(reader);

                    reader.Close();
                }

                connection.Close();

                return data;
            }
        }

        public async Task<DataTable> TransaccionAsync(string procedure, List<Parameter> param)
        {
            return await TransaccionAsync(procedure,param,0);
        }

        public async Task<DataTable> TransaccionAsync(string procedure, List<Parameter> param, int timeout)
        {
            using (SqlConnection connection = new SqlConnection(_cn))
            using (SqlCommand command = new SqlCommand(procedure, connection))
            {
                DataTable data = new DataTable();

                command.CommandType = CommandType.StoredProcedure;

                if (timeout > 0) { command.CommandTimeout = timeout; }
                if (param != null) { param.ForEach(P => command.Parameters.Add(new SqlParameter(P.key, IsNull(P.value)))); }

                connection.Open();

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    data.Load(reader);

                    reader.Close();
                }

                connection.Close();

                return data;
            }
        }

        public async Task<DataSet> TransaccionDsAsync(string procedure, Parameter param)
        {
            return await TransaccionDsAsync(procedure, param, 0);
        }

        public async Task<DataSet> TransaccionDsAsync(string procedure, Parameter param, int timeout)
        {
            return await Task<DataSet>.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(_cn))
                using (SqlCommand command = new SqlCommand(procedure, connection))
                using (DataSet data = new DataSet())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (timeout > 0) { command.CommandTimeout = timeout; }
                    if (param != null) { command.Parameters.Add(new SqlParameter(param.key, IsNull(param.value))); }

                    connection.Open();

                    using (SqlDataAdapter reader = new SqlDataAdapter(command))
                    {
                        reader.Fill(data);
                    }

                    connection.Close();

                    return data;
                }
            });
        }

        public async Task<DataSet> TransaccionDsAsync(string procedure, List<Parameter> param)
        {
            return await TransaccionDsAsync(procedure, param, 0);
        }

        public async Task<DataSet> TransaccionDsAsync(string procedure, List<Parameter> param, int timeout)
        {
            return await Task<DataSet>.Factory.StartNew(() =>
            {
                using (SqlConnection connection = new SqlConnection(_cn))
                using (SqlCommand command = new SqlCommand(procedure, connection))
                using (DataSet data = new DataSet())
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (timeout > 0) { command.CommandTimeout = timeout; }
                    if (param != null) { param.ForEach(P => command.Parameters.Add(new SqlParameter(P.key, IsNull(P.value)))); }

                    connection.Open();

                    using (SqlDataAdapter reader = new SqlDataAdapter(command))
                    {
                        reader.Fill(data);
                    }

                    connection.Close();

                    return data;
                }
            });
        }

        private static object IsNull(object value)
        {
            return value ?? DBNull.Value;
        }
    }
}
