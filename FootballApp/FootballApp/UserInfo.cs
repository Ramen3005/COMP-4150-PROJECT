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
            cmd.CommandText = "Select UserID from UserData where " + "UserName = @UserName";
            cmd.Parameters.AddWithValue("@UserName", userName);
            conn.Open();
            int userId = (int)cmd.ExecuteScalar();

            if (userId > 0)
            {
                return userId;
            }
            else
            {
                return -1;
            }
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
            cmd.CommandText = "Select UserID from UserData where "
                + " UserName = @UserName and Password = @Password ";
            cmd.Parameters.AddWithValue("@UserName", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            conn.Open();
            int userId = (int)cmd.ExecuteScalar();
            if (userId > 0) return userId;
            else return -1;
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

    internal string UserType(int userId)
    {
        throw new NotImplementedException();
    }
}

