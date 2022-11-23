using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;

public static class DataManager
{
    public static MySqlConnection Conn;
    public static string connStr = "server=localhost;port=3306;database=work;user=root;password=wxp@#84352176;charset=utf8";


    /// <summary>
    /// 点击注册按钮时判断用户名是否已存在,如果不存在就直接进行注册
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public static bool ClickRegister(string username, string password,string age,string gender)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            string sqlQuary = $"select * from usermessage where username ='{username}' and password = '{password}'";
            MySqlCommand comd = new MySqlCommand(sqlQuary, conn);
            MySqlDataReader reader = comd.ExecuteReader();
            
            if (reader.Read())
            {
                Debug.Log("-----用户名已存在，请重新输入！------");
                return false;
            }
            else
            {
                InsertUser(username, password,age,gender);
                Debug.Log("------注册成功，请进行登入------");
                return true;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
        finally
        {
            conn.Close();
        }

        return false;
    }
    
    /// <summary>
    /// 注册成功时，将数据插入数据库
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    private static void InsertUser(string username,string password,string age,string gender)
    {
        MySqlConnection conn = new MySqlConnection(connStr);

        try
        {
            conn.Open();
            string sqlInsert = $"insert into usermessage(username,password,age,gender) values('{username}' ,'{password}','{age}','{gender}')";
            MySqlCommand comd2 = new MySqlCommand(sqlInsert, conn);
            comd2.ExecuteNonQuery();
        }
        catch (System.Exception e)
        {
            
            Debug.Log(e.Message);
        }
        finally
        {
            conn.Close();
        }
    }
    
    /// <summary>
    /// 点击登录按钮时判断是否存在用户名，如果存在就登录，不存在就返回false
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public static bool ClickLogin(string username, string password)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            string sqlQuary = $"select * from usermessage where username ='{username}' and password = '{password}'";
            MySqlCommand comd = new MySqlCommand(sqlQuary, conn);

            MySqlDataReader reader = comd.ExecuteReader();
            if (reader.Read())
            {
                Debug.Log("------用户存在，登录成功！------");
                //进行登入成功后的操作，例如进入新场景。。。
                return true;

            }
            else
            {
                Debug.Log("------用户名或和密码错误,登录失败------");
                return false;
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
        finally
        {
            conn.Close();
        }

        return false;
    }

    /// <summary>
    /// 查询所有用户名与密码，保存用户名与密码在一个数组中，返回一个数组列表
    /// </summary>
    /// <returns>数组列表</returns>
    public static List<string[]> ClickAccount()
    {
        List<string[]> accounts = new List<string[]>();
        
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            string sqlQuary = "select * from usermessage";
            MySqlCommand comd = new MySqlCommand(sqlQuary, conn);

            MySqlDataReader reader = comd.ExecuteReader();
            while (reader.Read())
            {
                string[] account ={reader.GetString(1),reader.GetString(3),reader.GetString(4)};
                accounts.Add(account);
                //Debug.Log(reader.GetInt32(0) + " " + reader.GetString(1) + " " + reader.GetString(2));
            }
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
        finally
        {
            conn.Close();
        }
        return accounts;
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="username">要删除的用户名</param>
    public static void DeleteData(string username)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();
            string sqlQuary = $"delete from usermessage where username = '{username}'";
            MySqlCommand comd = new MySqlCommand(sqlQuary, conn);
            comd.ExecuteReader();
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
        finally
        {
            conn.Close();
        }
    }
}
