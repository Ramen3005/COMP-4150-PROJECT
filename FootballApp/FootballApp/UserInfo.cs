using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

internal class UserInfo
{
    public int UserCheck(string userName)
    {
        var conn = new SqlConnection(FootballApp.Properties.Settings.Default.asConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "Select UserID from UserData where UserName = @UserName";
            cmd.Parameters.AddWithValue("@UserName", userName);
            conn.Open();

            // Use DBNull.Value to handle cases where ExecuteScalar returns null
            object result = cmd.ExecuteScalar();

            if (result != null && result != DBNull.Value)
            {
                int userId;
                if (int.TryParse(result.ToString(), out userId))
                {
                    return userId;
                }
            }

            return -1;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return -1;
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
    public int LogIn(string userName, string password)
    {
        var conn = new SqlConnection(FootballApp.Properties.Settings.Default.asConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT UserID FROM UserData WHERE UserName = @UserName AND Password = @Password";
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            conn.Open();

            object result = cmd.ExecuteScalar();

            // Check if the result is null before trying to convert
            if (result != null)
            {
                return (int)result;
            }
            else
            {
                return -1; // or any other appropriate value for indicating failure
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return -1; // or any other appropriate value for indicating failure
        }
        finally
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }


    public string UserType(int userId)
    {
        var conn = new SqlConnection(FootballApp.Properties.Settings.Default.asConnectionString);
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT Type FROM UserData WHERE UserID = @UserID";
            cmd.Parameters.AddWithValue("@UserID", userId);
            conn.Open();
            string type = (string)cmd.ExecuteScalar();
            return type;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.ToString());
            return "Not a user";
        }
        finally
        {
            if (conn.State == ConnectionState.Open) ;
            conn.Close();
        }

    }
}
