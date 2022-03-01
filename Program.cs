
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Main_Method
{

    // JsonProperty attr : control serialization of JSON obj
    class TriviaDB
    {
        [JsonProperty("question")]
        public string? Question { get; set; }


        [JsonProperty("answer")]
        public string? Answer { get; set; }
 

       [JsonProperty("created_at")]
       public string? createdDate { get; set; }

    }

    

    class Program // main func class
    {
        // to send HTTP requests and receive HTTP responses from URI
        private static readonly HttpClient client = new HttpClient();

        // async instead of void main, will call new,private, static, async methods
        // Task is an obj to represent work and tell if completed.
        // Task<TResult> for operations that reutrn values
        static async Task Main(string[] args)
        {
            // que program
            await ProcessRepositories();
        } // Main

        // inf loop, breaks when enter is pressed
        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Trivia Time! Type in random to begin or Enter to end.");
                    var usersAnswer = Console.ReadLine();
                    if (string.IsNullOrEmpty(usersAnswer))
                    {
                        break;
                    }
                    // serialization : process of converting an obj into stream of bytes to store obj into memory,db, or file
                    // saves state of obj in order. deserialization deletes obj in order.
                    // client's GetAsync method makes  an API call

                    var result = await client.GetAsync("http://jservice.io/api/" + usersAnswer);
                    var resultRead = await result.Content.ReadAsStringAsync();

                    // deserialization 
                    var readTrivia = JsonConvert.DeserializeObject<List<TriviaDB>>(resultRead);
                    
                    // var TriviaCateg = JsonConvert.DeserializeObject<List<TriviaDB>>(resultRead);

                    // console prints starts here

                    Console.WriteLine("\n---");

                    Console.WriteLine("Created: " + readTrivia[0].createdDate);
                    Console.WriteLine("\n---");
                    Console.WriteLine("Question: " + readTrivia[0].Question);
                    Console.WriteLine("\n---");
                    Console.WriteLine("Answer: " + readTrivia[0].Answer);
                    Console.WriteLine("\n---");



                }
                catch (Exception)
                {
                    Console.WriteLine("error");
                }
            } // while loop
             

          
        } // ProcessRepositories



    } // Program


} // namespace Main_Method
    



