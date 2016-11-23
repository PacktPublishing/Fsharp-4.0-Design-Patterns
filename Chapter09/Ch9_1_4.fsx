open System.Data
open System.Data.SqlClient

let alphabet = seq { 'A' .. 'Z' }

let connStr = @"Data Source=(localdb)\projectsv12;Initial Catalog=Adventureworks2014;Integrated Security=true;"
let dbConnection = new SqlConnection(connStr)
dbConnection.Open()

let dbCommand = new SqlCommand("select FirstName from [Person].[Person]",dbConnection)
let names = seq {
                printfn "reading from db" 
                use reader = dbCommand.ExecuteReader(CommandBehavior.Default)
                while reader.Read() do yield reader.GetString(0) }
let distribution =
    query {
        for letter in alphabet do
            let howMuch =
                query {
                    for name in names do
                    where (name.StartsWith(string letter))
                    distinct
                    select name
                } |> Seq.length
            sortBy howMuch
            select (letter, howMuch)
    }
#time "on"
distribution |> Seq.toList |> printfn "%A"