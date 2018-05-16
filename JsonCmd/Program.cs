using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace JsonCmd
{
    class Program
    {
        static string api = "http://localhost:3000";

        private void AddCompany()
        {

            Console.WriteLine("");
            Console.Write("Enter Company ID : ");
            var companyId = Console.ReadLine();
            Console.Write("Enter Company Name : ");
            var companyName = Console.ReadLine();

            using (WebClient wc = new WebClient())
            {

                NameValueCollection myNameValueCollection = new NameValueCollection();

                try
                {
                    myNameValueCollection.Add("id", companyId);
                    myNameValueCollection.Add("companyname", companyName);
                    Console.WriteLine("\nUploading...");
                    byte[] responseArray = wc.UploadValues(api + "/experiences", "POST", myNameValueCollection);
                    Console.WriteLine("");
                    Console.WriteLine("Experiencia adicionada com sucesso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Add Error : " + ex.Message.ToString());
                }
            }
        }

        private void UpdateCompany()
        {
            Console.WriteLine("");
            Console.Write("Enter Company ID : ");
            var companyId = Console.ReadLine();

            Console.WriteLine("\nCarregando...");
            Console.WriteLine("");

            using (WebClient wc = new WebClient())
            {
                var jsonUser = wc.DownloadString(api + "/experiences/"+ companyId);

                try
                {
                    var jObjectUser = JObject.Parse(jsonUser);

                    if (jObjectUser != null)
                    {

                        Console.WriteLine("name: " + jObjectUser["companyname"].ToString());
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                Console.WriteLine("");
                Console.Write("Digite o novo nome: ");
                var companyNewName = Console.ReadLine();

                NameValueCollection myNameValueCollection = new NameValueCollection();

                try
                {
                    myNameValueCollection.Add("companyname", companyNewName);
                    byte[] responseArray = wc.UploadValues(api + "/experiences/"+ companyId, "PUT", myNameValueCollection);
                    Console.WriteLine("");
                    Console.WriteLine("Experiencia atualizada com sucesso");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Add Error : " + ex.Message.ToString());
                }
            }
        }

        private void DeleteCompany()
        {
            //using (WebClient wc = new WebClient())
            //{
            //    var json = File.ReadAllText(jsonFile);
            //    try
            //    {
            //        var jObject = JObject.Parse(json);
            //        JArray experiencesArrary = (JArray)jObject["experiences"];
            //        Console.Write("Enter Company ID to Delete Company : ");
            //        var companyId = Convert.ToInt32(Console.ReadLine());

            //        if (companyId > 0)
            //        {
            //            var companyName = string.Empty;
            //            var companyToDeleted = experiencesArrary.FirstOrDefault(obj => obj["companyid"].Value<int>() == companyId);

            //            experiencesArrary.Remove(companyToDeleted);

            //            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
            //            File.WriteAllText(jsonFile, output);
            //        }
            //        else
            //        {
            //            Console.Write("Invalid Company ID, Try Again!");
            //            UpdateCompany();
            //        }
            //    }
            //    catch (Exception)
            //    {

            //        throw;
            //    }
            //}
        }

        private void GetUserDetails()
        {
            Console.WriteLine("");
            Console.WriteLine("Carregando...");
            Console.WriteLine("");

            using (WebClient wc = new WebClient())
            {
                var jsonUser = wc.DownloadString(api + "/user/123");
                var jsonExp = wc.DownloadString(api + "/experiences");
                var jsonContact = wc.DownloadString(api + "/contact");

                try
                {
                    var jObjectUser = JObject.Parse(jsonUser);
                    var jArrayExp = JArray.Parse(jsonExp);
                    var jArrayContact = JArray.Parse(jsonContact);

                    if (jObjectUser != null)
                    {

                        Console.WriteLine("ID :" + jObjectUser["id"].ToString());
                        Console.WriteLine("Name :" + jObjectUser["name"].ToString());
                        Console.WriteLine("Street :" + jObjectUser["street"].ToString());
                        Console.WriteLine("City :" + jObjectUser["city"].ToString());
                        Console.WriteLine("Zipcode :" + jObjectUser["zipcode"]);
                    }

                    Console.WriteLine("");

                    if (jArrayExp != null)
                    {

                        foreach (var item in jArrayExp)
                        {
                            Console.WriteLine("company Id :" + item["id"]);
                            Console.WriteLine("company Name :" + item["companyname"].ToString());
                        }

                    }

                    Console.WriteLine("");


                    if (jArrayContact != null)
                    {
                        foreach (var item in jArrayContact)
                        {

                            Console.WriteLine("Phone Number :" + item["phoneNumber"].ToString());
                            Console.WriteLine("Role :" + item["role"].ToString());
                            Console.WriteLine("");
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        static void Main(string[] args)
        {
            Program objProgram = new JsonCmd.Program();

            Console.WriteLine("Choose Your Options : 1 - Add, 2 - Update, 3 - Delete, 4 - Select \n");
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    objProgram.AddCompany();
                    break;
                case "2":
                    objProgram.UpdateCompany();
                    break;
                case "3":
                    objProgram.DeleteCompany();
                    break;
                case "4":
                    objProgram.GetUserDetails();
                    break;
                default:
                    Main(null);
                    break;
            }
            Console.ReadLine();

        }
    }
}
