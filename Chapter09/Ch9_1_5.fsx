open System.Data
open System.Data.SqlClient

let alphabet = seq { 'A' .. 'Z' }

let connStr = @"Data Source=(localdb)\projectsv12;Initial Catalog=Adventureworks2014;Integrated Security=true;"
let dbConnection = new SqlConnection(connStr)
dbConnection.Open()

let dbCommandR l =
    new SqlCommand(
        (sprintf "%s%s%s" "select distinct FirstName from [Person].[Person] where FirstName like '" l "%'"),
        dbConnection)

let names l = seq {
                printfn "reading from db" 
                use reader = (dbCommandR l).ExecuteReader(CommandBehavior.Default)
                while reader.Read() do yield reader.GetString(0) }

let distribution =
    query {
        for letter in alphabet do
            let howMuch = names (string letter) |> Seq.length
            sortBy howMuch
            select (letter, howMuch)
    }
#time "on"
distribution |> Seq.toList |> printfn "%A"
