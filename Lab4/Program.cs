using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Student;

namespace Lab4
{
    class Program
    {
        private static CloudTableClient tableClient;
        private static CloudTable studentsTable;

        static void Main(string[] args)
        {
            Task.Run(async () => {await Initialize(); })
                .GetAwaiter()
                .GetResult();
        }
        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;"
                + "AccountName=temanr4;"
                + "AccountKey=jceHl61EfvY2sL2n+k7o+fs/wPfGwN4KuQ04b3NgPXrm+VsmR8HF9PqMAuqpAydJObOLwn88bZcaH1noMjvwAw==;"
                + "EndpointsSuffix=core.windows.net";
            var acoount= CloudStorageAccount.Parse(storageConnectionString);
            tableClient = acoount.CreateCloudTableClient();

            studentsTable=tableClient.GetTableReference("studenti");
            await studentsTable.CreateIfNotExistsAsync();
            // await AddNewStudent();
            // await EditStudent();
            // await GetAllStudens();
        }
        private static async Task GetAllStudents()
        {
            Console.WriteLine("Universitate\tCNP\tNume\tPrenume\tEmail\tTelefon\tAn");
            TableQuery<StudentEntity> query=new TableQuery<StudentEntity>();
            TableContinuationToken token =null;
            do {
                TableQuerySegment<StudentEntity.ConvertBack resultSegment= await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token= ResultSegment.ContinuationToken;

                foreach(StudentEntity entity in ResultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\r{3}\t{4}\t{5}\t{6}",entity.PartitionKey, entity.RowKey, entity.Nume, entity.Prenume,entity.Telefon,entity.Email,entity.An, entity.Facultate);
                }
            }
        }
        private static async Task AddNewStudents(){
            var student = new StudentEntity("UPT","2981208053212");
            student.Nume="Popescu";
            student.Prenume="Mihai";
            student.Telefon="0771548775";
            student.Email="mihai.popescu@yahoo.com";
            student.An="3";
            student.Facultate="AC";

            var insertOperation = TableOperation.Insert(student);
            await studentsTable.ExecuteAsync(insertOperation);

        }
        private static async Task EditStudent()
        {
            var student = new StudentEntity("UVT, 2981208053212");
            student.Nume="Darla";
            var editOperation = TableOperation.Merge(student);
            await studentsTable.ExecuteAsync(editOperation);

        }
    }
}
