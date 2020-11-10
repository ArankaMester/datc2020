using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Student;
using tabelmetrici;

namespace Lab4
{
    class Program
    {
        public static int contor=0;
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;
        private static CloudTable metriceTable;

        static void Main(string[] args)
        {
            Task.Run(async () => {await Initialize(); })
                .GetAwaiter()
                .GetResult();
            Task.Run(async () => {await Initialize_m(); })
                .GetAwaiter()
                .GetResult();
        }
        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
                + "AccountName=temanr4;"
                + "AccountKey=jceHl61EfvY2sL2n+k7o+fs/wPfGwN4KuQ04b3NgPXrm+VsmR8HF9PqMAuqpAydJObOLwn88bZcaH1noMjvwAw==;"
                + "EndpointSuffix=core.windows.net";
            var acoount= CloudStorageAccount.Parse(storageConnectionString);
            tableClient = acoount.CreateCloudTableClient();

            studentsTable=tableClient.GetTableReference("studenti");
            await studentsTable.CreateIfNotExistsAsync();
            //await AddNewStudent();
            // await EditStudent();
            await GetAllStudents();
        }
         static async Task Initialize_m()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
                + "AccountName=temanr4;"
                + "AccountKey=jceHl61EfvY2sL2n+k7o+fs/wPfGwN4KuQ04b3NgPXrm+VsmR8HF9PqMAuqpAydJObOLwn88bZcaH1noMjvwAw==;"
                + "EndpointSuffix=core.windows.net";
            var acoount= CloudStorageAccount.Parse(storageConnectionString);
            tableClient = acoount.CreateCloudTableClient();

            metriceTable=tableClient.GetTableReference("metrici");
            await metriceTable.CreateIfNotExistsAsync();
            await AddNewMetrici();
            // await EditStudent();
            //await GetAllStudents();
        }
        private static async Task GetAllStudents()
        {
            Console.WriteLine("Universitate\tCNP\tNume\tPrenume\tEmail\tTelefon\tAn\tFacultate");
            TableQuery<StudentEntity> query=new TableQuery<StudentEntity>();
            TableContinuationToken token =null;
            do {
                TableQuerySegment<StudentEntity> resultSegment= await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token= resultSegment.ContinuationToken;

                foreach(StudentEntity entity in resultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\r{3}\t{4}\t{5}\t{6}",entity.PartitionKey, entity.RowKey, entity.Nume, entity.Prenume,entity.Telefon,entity.Email,entity.An, entity.Facultate);
                    contor++;
                }
            }while(token!=null);
        }
        private static async Task AddNewStudent(){
            var student = new StudentEntity("UPT","2981208053212");
            student.Nume="Popescu";
            student.Prenume="Mihai";
            student.Telefon="0771548775";
            student.Email="mihai.popescu@yahoo.com";
            student.An= 3;
            student.Facultate="AC";

            var student2 = new StudentEntity("UVT","1990805120150");
            student2.Nume="Marinescu";
            student2.Prenume="Ionel";
            student2.Telefon="0755126325";
            student2.Email="ionel.marinescu@gmail.com";
            student2.An= 1;
            student2.Facultate="Psihologie";

            var student3 = new StudentEntity("UPT","2970301200165");
            student3.Nume="Budea";
            student3.Prenume="Ana";
            student3.Telefon="0710117585";
            student3.Email="ana.budea@yahoo.com";
            student3.An= 4;
            student3.Facultate="ETC";

           var insertOperation = TableOperation.Insert(student);
          // var insertOperation = TableOperation.Insert(student2);
            //var insertOperation = TableOperation.Insert(student3);
            await studentsTable.ExecuteAsync(insertOperation);

        }
        private static async Task AddNewMetrici()
        {   
            var metrice=new Metrici("General"," ");
            metrice.RowKey=DateTime.UtcNow.ToString("yyyy-MM-dd-HH:mm:ss");
            metrice.count=contor;
            var insertOperation=TableOperation.Insert(metrice);
            await metriceTable.ExecuteAsync(insertOperation);
            
        }
        private static async Task EditStudent()
        {
            var student = new StudentEntity("UVT", "2981208053212");
            student.Nume="Darla";
            var editOperation = TableOperation.Merge(student);
            await studentsTable.ExecuteAsync(editOperation);
        }
    }
}


