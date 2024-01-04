using Oracle.ManagedDataAccess.Client;
using PDI2024.Models;
using System;
using System.Collections.Generic;
using System.Data;

public class OracleDBHelper
{
    private readonly string connectionString;

    public OracleDBHelper(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public OracleConnection OpenConnection()
    {
        OracleConnection connection = new OracleConnection(connectionString);

        try
        {
            connection.Open();
            Console.WriteLine("Oracle Connection Opened Successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error opening Oracle connection: " + ex.Message);
            throw ex;
        }

        return connection;
    }

    public void CloseConnection(OracleConnection connection)
    {
        try
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
                Console.WriteLine("Oracle Connection Closed Successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error closing Oracle connection: " + ex.Message);
            throw ex;
        }
    }

    public List<Vehicle> SelectUsers()
    {
        List<Vehicle> Vehicle = new List<Vehicle>();
        OracleConnection connection = OpenConnection();

        try
        {
            string query = "SELECT * FROM VEHICLEINFO"; // Thay thế "Users" bằng tên bảng thực tế của bạn
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Đọc dữ liệu từ cột và tạo đối tượng User
                        Vehicle Vehicles = new Vehicle
                        {                            
                            VEHICLEID = reader["VEHICLEID"].ToString(),
                            LOCATION = reader["LOCATION"].ToString(),
                            REMARK = reader["REMARK"].ToString(),
                            // Thêm các thuộc tính khác tùy thuộc vào cấu trúc bảng
                        };

                        Vehicle.Add(Vehicles);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error retrieving users: " + ex.Message);
            throw ex;
        }
        finally
        {
            CloseConnection(connection);
        }

        return Vehicle;
    }


    public List<Vehicle> getVehicle()
    {
        List<Vehicle> Vehicle = new List<Vehicle>();
        OracleConnection connection = OpenConnection();

        try
        {
            string query = "select VEHICLEID,LOCATION,REMARK,STATUS from VEHICLEINFO"; 
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Đọc dữ liệu từ cột và tạo đối tượng User
                        Vehicle Vehicles = new Vehicle
                        {
                            VEHICLEID = reader["VEHICLEID"].ToString(),
                            LOCATION = reader["LOCATION"].ToString(),
                            REMARK = reader["REMARK"].ToString(),
                            STATUS = reader["STATUS"].ToString(),
                            // Thêm các thuộc tính khác tùy thuộc vào cấu trúc bảng
                        };

                        Vehicle.Add(Vehicles);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error retrieving list: " + ex.Message);
            throw ex;
        }
        finally
        {
            CloseConnection(connection);
        }

        return Vehicle;
    }

    public List<Vehicle> getVehicleByID(string location)
    {
        List<Vehicle> Vehicle = new List<Vehicle>();
        OracleConnection connection = OpenConnection();

        try
        {
            string query = "select VEHICLEID,LOCATION,REMARK from VEHICLEINFO where LOCATION='" + location + "'";
            using (OracleCommand command = new OracleCommand(query, connection))
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                        reader.Read();

                    // Đọc dữ liệu từ cột và tạo đối tượng User
                    Vehicle Vehicles = new Vehicle
                    {
                        VEHICLEID = reader["VEHICLEID"].ToString(),
                        LOCATION = reader["LOCATION"].ToString(),
                        REMARK = reader["REMARK"].ToString(),
                        // Thêm các thuộc tính khác tùy thuộc vào cấu trúc bảng
                    };
                    Vehicle.Add(Vehicles);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error retrieving list: " + ex.Message);
            throw ex;
        }
        finally
        {
            CloseConnection(connection);
        }

        return Vehicle;
    }

    public void updateVehicle(string vehicleID, string newLocation, string newRemark)
    {
        OracleConnection connection = OpenConnection();

        try
        {
            string query = "UPDATE VEHICLEINFO SET LOCATION = :newLocation, REMARK = :newRemark WHERE VEHICLEID = :vehicleID";

            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.Parameters.Add(new OracleParameter("newLocation", newLocation));
                command.Parameters.Add(new OracleParameter("newRemark", newRemark));
                command.Parameters.Add(new OracleParameter("vehicleID", vehicleID));

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating vehicle: " + ex.Message);
            throw ex;
        }
        finally
        {
            CloseConnection(connection);
        }
    }

    public void insertVehicle(string vehicleID, string location, string remark,int STATUS)
    {
        OracleConnection connection = OpenConnection();

        try
        {
            string query = "INSERT INTO VEHICLEINFO (VEHICLEID, LOCATION, REMARK,STATUS) VALUES (:VEHICLEID, :LOCATION, :REMARK,:STATUS)";

            using (OracleCommand command = new OracleCommand(query, connection))
            {
                command.Parameters.Add(new OracleParameter("VEHICLEID", vehicleID));
                command.Parameters.Add(new OracleParameter("LOCATION", location));
                command.Parameters.Add(new OracleParameter("REMARK", remark));
                command.Parameters.Add(new OracleParameter("STATUS", STATUS));

                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error inserting vehicle: " + ex.Message);
            throw ex;
        }
        finally
        {
            CloseConnection(connection);
        }
    }



}


