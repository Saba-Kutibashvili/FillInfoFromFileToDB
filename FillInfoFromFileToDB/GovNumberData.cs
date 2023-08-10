using Microsoft.Data.SqlClient;

namespace FillInfoFromFileToDB
{
    public class GovNumberData
    {
        public static void CreateDatabase(SqlConnection con, string name)
        {
            string query = "Create Database " + name;

            SqlCommand command = new SqlCommand(query, con);

            command.ExecuteNonQuery();
        }

        public static void CreateTable(SqlConnection con, string name)
        {
            string query =
            @$"
                USE Data

                CREATE TABLE dbo.{name}
                (
                    Id nvarchar(450) NOT NULL,
                    CreateDate datetime NOT NULL,
                    IsActive bit NOT NULL,
                    IsDeleted bit NOT NULL,
                    IsHidden bit NOT NULL,
                    GovNumber_FullNumber varchar(250) NULL,
                    VinCode varchar(250) NULL
                );";

            SqlCommand command = new SqlCommand(query, con);

            command.ExecuteNonQuery();
        }

        private static int SQLBool(bool bolean)
        {
            return bolean ? 1 : 0;
        }

        private static string SQLDateTime(DateTime dt)
        {
            return $"{dt.Year}-{dt.Month}-{dt.Day} {dt.Hour}:{dt.Minute}:{dt.Second}";
        }

        public static void AddRows(SqlConnection con, string tableName, List<GovNumber> govNumbers)
        {

            foreach(var govNumber in govNumbers){
                string query = @$"INSERT INTO {tableName} VALUES('{govNumber.Id}', CAST(N'{SQLDateTime(govNumber.CreateDate)}' AS DateTime),
                                    {SQLBool(govNumber.IsActive)}, {SQLBool(govNumber.IsDeleted)}, 
                                    {SQLBool(govNumber.IsHidden)}, '{govNumber.GovNumber_FullNumber}', 
                                    '{govNumber.VinCode}')";

                SqlCommand command = new SqlCommand(query, con);
                command.ExecuteNonQuery();
            }
        }
    }
}
