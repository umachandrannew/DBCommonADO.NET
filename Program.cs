using System;
using System.Collections.Generic;


namespace DBCommonADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            //CRUD operation

            //CREATE
            try
            {
                using (DataConnection d = new DataConnection("studentconn"))
                {
                    string sqlInsert = "insert into student_details values ('PHIL','M','CRICKET')";
                    d.DBOperation_Update(sqlInsert);
                    Console.WriteLine("Record inserted");
                }

            }
            catch
            {
                Console.WriteLine("Create");
            }

            //READ


            try { 
                using (DataConnection d = new DataConnection("studentconn"))
            {
                string sqlQry = "select name,sports from student_details";
                d.DBOperation_Read(sqlQry);

                while(d.dr.Read())
                {
                    Console.WriteLine($"{d.dr["name"].ToString()} plays {d.dr["sports"].ToString()}" ) ;
                       
                }
            }
            }

            catch
            {
                Console.WriteLine("Read");
            }

            //UPDATE


            try
            {
                using (DataConnection d = new DataConnection("studentconn"))
                {
                    string sqlUpdate = "update student_details set sports='BASEBALL' where name ='PHIL'";
                    d.DBOperation_Read(sqlUpdate);
                    Console.WriteLine("PHIL Record updated");
                }
            }

            catch
            {
                Console.WriteLine("Update");
            }

            //DELETE
            try
            { 
            using (DataConnection d = new DataConnection("studentconn"))
            {
                string sqlDelete = "delete from student_details  where name ='PHIL'";
                d.DBOperation_Read(sqlDelete);
                    Console.WriteLine("PHIL Record deleted");

                }
            }
            

            catch
            {
                Console.WriteLine("Delete");
            }



            //CALLING A STORED PROCEDURE

            Console.WriteLine("*******STORED PROCEDURE**********");
            using (DataConnection d = new DataConnection("studentconn"))
            {
                List<DataParam> sqlParams = new List<DataParam>();
                List<DataParam> sqlResult;

                sqlParams.Add(new DataParam("p_sports", System.Data.DbType.String, "TENNIS", System.Data.ParameterDirection.Input));
                sqlParams.Add(new DataParam("p_girls", System.Data.DbType.String, "", System.Data.ParameterDirection.Output));
                sqlParams.Add(new DataParam("p_boys", System.Data.DbType.String, "", System.Data.ParameterDirection.Output));

                string sqlCmd = "PROC_GIRL_BOY";

                sqlResult = d.DBOperation_ExecuteProcedure(sqlCmd, sqlParams);

                foreach(DataParam p in sqlResult)
                {
                    Console.WriteLine($"{ p.paramName} : { p.paramValue}");
                }

            }

                Console.ReadLine();
        }



        }
    }

