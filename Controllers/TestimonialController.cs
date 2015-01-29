using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiTest.Controllers;

namespace WebApiTest
{
    public class testimonialsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Testimonial> Get()
        {
            var testimonials = GetTransactionsFromDB();

            return testimonials;
        }

        private IEnumerable<Testimonial> GetTransactionsFromDB()
        {

            var result = new List<Testimonial>();

            //var connectionString = ConfigurationManager.ConnectionStrings["TestimonialsDB_D"].ToString();
            var connectionString = ConfigurationManager.ConnectionStrings["TestimonialsDB_C"].ToString();

            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "GetAllTestimonials";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();


                var testimonial = new Testimonial();
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int index = reader.GetInt32(0); 
                        string name = reader.GetString(1);
                        string comment = reader.GetString(2);
                        string date = reader.GetString(3);

                        testimonial.Name = name;
                        testimonial.Comment = comment;

                        result.Add(testimonial);
                        
                    }
                }
            }


            return result;

            
         
        }

        //// GET api/<controller>
        //public IEnumerable<string[]> Get()
        //{
        //    //return new string[] { "value1", "value2" };

        //    var csv = new List<string[]>();
        //    var lines = System.IO.File.ReadAllLines(HttpContext.Current.Server.MapPath("/Models/data.csv"));
        //    foreach (string line in lines)
        //        csv.Add(line.Split(','));
        //    return csv;

        //}
        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]Testimonial value)
        {
            var result = AddTestimonialToDB(value);
        }

        private bool AddTestimonialToDB(Testimonial testimonial)
        {
            var result = false;

            if (testimonial.Date == null)
                testimonial.Date = DateTime.Today.Year.ToString();

            var connectionString = ConfigurationManager.ConnectionStrings["TestimonialsDB"].ToString();

            using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand();
                
                cmd.CommandText = "AddTestimonial";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection1;

                sqlConnection1.Open();

                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = testimonial.Name;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar).Value = testimonial.Comment;
                cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = testimonial.Date;


                result = cmd.ExecuteNonQuery() > 0;
                
            }



            return result;
        }



        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}