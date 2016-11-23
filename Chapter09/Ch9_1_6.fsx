open System.Data
open System.Data.SqlClient

let connStr = @"Data Source=(localdb)\projectsv12;Initial Catalog=Adventureworks2014;Integrated Security=true;"
let dbConnection = new SqlConnection(connStr)
dbConnection.Open()

let dbCommandF =
    new SqlCommand("select SUBSTRING(FirstName, 1, 1),count(distinct FirstName) as \"count\"
                    from [Adventureworks2014].[Person].[Person]
                    group by SUBSTRING(FirstName, 1, 1)
                    order by count",dbConnection)

let frequences = seq {
                printfn "reading from db" 
                use reader = dbCommandF.ExecuteReader(CommandBehavior.Default)
                while reader.Read() do yield (reader.GetString(0), reader.GetInt32(1)) }

let distribution =
    query {
        for freq in frequences do
        select freq
    }
#time "on"
distribution |> Seq.toList |> printfn "%A"
