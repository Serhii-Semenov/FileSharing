﻿using System;

using MySql.Data.MySqlClient;
using FSServer.Model;
using System.Collections.Generic;

namespace FSServer.Privider
{
    public class Provider
    {
        const string connectionString = "Database=dbfilesharing;server=127.0.0.1;UserId=root;Password=''";
        //const string connectionString = "Database=c17db;server=10.6.1.199;UserId=c17db;Password=12345";

        private MySqlConnection Connect()
        {
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        public int CreateAccount(string _login, string _password, string _mail)
        {
            int lastId = -1;
            using (var con = Connect())
            {
                string myInsertQuery = string.Format("INSERT INTO `clients`( `id`, `login`, `password`, `mail`) VALUES(null, '{0}', '{1}', '{2}')", _login, _password, _mail);
                myInsertQuery += "; SELECT LAST_INSERT_ID();";
                var cmd = new MySqlCommand(myInsertQuery, con);
                try
                {
                    lastId = int.Parse((cmd.ExecuteScalar()).ToString());
                }
                catch (Exception err) { var v = err; }
            }
            return lastId;
        }

        public int Login(string _login, string _password)
        {
            using (var con = Connect())
            {
                string myQuery = string.Format("SELECT `id` FROM `clients` WHERE `login`='{0}' AND `password`='{1}'", _login, _password);
                var cmd = new MySqlCommand(myQuery, con);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return int.Parse(reader["id"].ToString());
                    }
                    return -2;
                }
            }
        }

        public ClientList GetClientsOnline()
        {
            var cl = new ClientList();
            var c = new Client();
            int i = 0;
            using (var con = Connect())
            {
                string myQuery = "SELECT `id`, `login` FROM `clients` WHERE `online`= 1 ";
                var cmd = new MySqlCommand(myQuery, con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        c.Id = int.Parse(reader["id"].ToString());
                        c.ClientName = reader["login"].ToString();
                        cl.Clients.Add(i++, c);
                    }
                    return cl;
                }
            }
        }

        public bool AddPath(string _path, string _name)
        {
            bool b = true;
            int id = GetIdByName(_name);
            using (var con = Connect())
            {
                string myInsertQuery = string.Format("INSERT INTO `filepath`( `id`, `client`, `path`) VALUES(null, {0}, '{1}');", id, _path);
                var cmd = new MySqlCommand(myInsertQuery, con);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception err) { b = false; var v = err; }
            }
            return b;
        }

        public int GetIdByName(string _name)
        {
            int id = 0;
            using (var con = Connect())
            {
                string mySubQuery = string.Format("(SELECT `id` FROM `clients` WHERE `login`='{0}'); ", _name);
                var cmd = new MySqlCommand(mySubQuery, con);
                try
                {
                    id = int.Parse((cmd.ExecuteScalar()).ToString());
                }
                catch (Exception err) { var v = err; }
            }
            return id;
        }

        public string GetNameById(int _id)
        {
            string name = "";
            using (var con = Connect())
            {
                string mySubQuery = string.Format("(SELECT `login` FROM `clients` WHERE `id`='{0}'); ", _id);
                var cmd = new MySqlCommand(mySubQuery, con);
                try
                {
                    name = cmd.ExecuteScalar().ToString();
                }
                catch (Exception err) { var v = err; }
            }
            return name;
        }

        public List<string> GetPaths(string _name)
        {
            List<string> ls = new List<string>();
            using (var con = Connect())
            {
                int id = GetIdByName(_name);
                string myQuery = string.Format("SELECT `path` FROM `filepath` WHERE `client`= {0} ", id);
                var cmd = new MySqlCommand(myQuery, con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        ls.Add(reader["path"].ToString());
                    }
                }
            }
            return ls;
        }

        public List<string> GetChat()
        {
            List<string> ls = new List<string>();
            using (var con = Connect())
            {
                string myQuery = "SELECT `clients`.`login`, `fschat`.`message` FROM `clients`, `fschat` WHERE `fschat`.`client`= `clients`.`id`; ";
                var cmd = new MySqlCommand(myQuery, con);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ls.Add((reader[0].ToString() + " : " + reader[1].ToString()));
                    }
                }
            }
            return ls;
        }

        public void SendMessageToChat(string _mes, int _id)
        {
            using (var con = Connect())
            {
                string myInsertQuery = string.Format("INSERT INTO `fschat`( `id`, `message`, `client`) VALUES(null, '{0}', {1});", _mes, _id);
                var cmd = new MySqlCommand(myInsertQuery, con);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception err) { var v = err; }
            }
        }
    }
}
