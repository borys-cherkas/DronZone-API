namespace Common.Constants
{
    public static class DbConstants
    {
        //public const string LocalConnectionString = "Server=(localdb)\\mssqllocaldb;Database=dronezonedb;Trusted_Connection=True;";

        public const string AzureConnectionString = "Server=tcp:dronzone-db.csvsyoxrmhzk.us-east-2.rds.amazonaws.com,1433; Initial Catalog=DronZoneDB;Persist Security Info=False;User ID=admin;Password=Test123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
