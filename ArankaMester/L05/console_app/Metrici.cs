using Microsoft.WindowsAzure.Storage.Table;
namespace tabelmetrici{
    public class Metrici:TableEntity
    {
        public Metrici(string universitate, string timestamp)
        {
        this.PartitionKey = universitate;
        this.RowKey= timestamp;
       

        }

        public Metrici(){}
        
         public int count{ get; set;}
        

    }
}
