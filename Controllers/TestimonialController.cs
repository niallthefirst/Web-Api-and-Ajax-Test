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
using WebApiTest.Admin;

namespace WebApiTest
{
    public class testimonialsController : ApiController
    {
        private static string ConnectionString()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["TestimonialsDB_D"].ToString();
            //var connectionString = ConfigurationManager.ConnectionStrings["TestimonialsDB_C"].ToString();
            return connectionString;
        }

        // GET api/<controller>
        public IEnumerable<Testimonial> Get()
        {
            var result = new List<Testimonial>();

            try
            {

                var connectionString = ConnectionString();

                using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;

                    cmd.CommandText = "GetAllTestimonials";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sqlConnection1;

                    sqlConnection1.Open();


                    
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int index = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string comment = reader.GetString(2);
                            string date = reader.GetString(3);

                            var testimonial = new Testimonial();
                            testimonial.ID = index;
                            testimonial.Name = name;
                            testimonial.Comment = comment;
                            testimonial.Date = date;
                            
                            result.Add(testimonial);

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorHandler.Write("something went wrong", ex);
                return null;
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
        public Testimonial Get(int id)
        {
            Testimonial result = null;

            var testimonials = Get();

            if(testimonials.Where(t => t.ID == id).Any())
                result = testimonials.Where(t => t.ID == id).Single();


            return result;

        }

        // POST api/<controller>
        public string Post([FromBody]Testimonial value)
        {
            string response = "Failed to add testimonial.";
            int newID = -1;
            try
            {
                newID = AddTestimonialToDB(value);

                if (newID >= 0)
                {
                    value.ID = newID;

                    response = string.Format("Successfully added testimonial {0}, {1}. Thankyou {2}. ", value.ID, value.Comment, value.Name);

                }
                else
                {
                    ErrorHandler.Write("something went wrong");


                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Write("something went wrong", ex);
            }

            return response;
        }

        private int AddTestimonialToDB(Testimonial testimonial)
        {
            var result = -1;

            if (testimonial.Date == null)
                testimonial.Date = DateTime.Today.Year.ToString();

            var connectionString = ConnectionString();

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


                result = (int)cmd.ExecuteScalar(); //cmd.ExecuteNonQuery() > 0;


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