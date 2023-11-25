using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Xml.Linq;

internal class UserInfo
{
    public int UserCheck(string userName)
    {
        var conn = new SQLiteConnection();
        try
        {
            using (SQLiteCommand cmd = new SQLiteCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT UserID FROM UserData WHERE UserName = @UserName";
                cmd.Parameters.AddWithValue("@UserName", userName);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    int userId = Convert.ToInt32(result);
                    return userId;
                }
                else
                {
                    return -1;
                }
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
        var conn = new SqlConnection(Properties.Settings.Default.DataBase);
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

