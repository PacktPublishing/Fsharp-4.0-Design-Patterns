#r @"C:\code\WebJobSQL\packages\ExcelProvider.0.8.0\lib\ExcelProvider.dll"
#r "System.Data"
open FSharp.ExcelProvider
open System.Data
open System.Data.SqlClient
open System.Collections.Generic

type LaserShip = ExcelFile< @"C:\code\PacktBook\Code\Chapter11\lasership invoice format.xlsx", HasHeaders=true, ForceString=true>

let asNullableString = function | null -> box System.DBNull.Value
                                | (s: string) -> s.Trim() |> function "" -> box System.DBNull.Value | l -> box l
let asString fieldName = function | null -> failwith (sprintf "%s %s" "asString: null for not-nullable field " fieldName)
                                    | (s: string) -> s.Trim() |> box
let asNullableDate = function | null ->  box System.DBNull.Value
                              | (s: string) -> s.Trim() |> System.DateTime.TryParse |> function (true,s) -> box s.Date | _ -> box System.DBNull.Value
let asNullableMoney = function | null -> box System.DBNull.Value
                               | (s: string) -> s.Trim().Replace("$", "") |>  System.Decimal.TryParse |> function (true,s) -> box s | _ -> box System.DBNull.Value
let asNullableDecimal =  function | null -> box System.DBNull.Value
                                  | (s: string) -> s.Trim() |>  System.Decimal.TryParse |> function (true,s) -> box s | _ -> box System.DBNull.Value
let asNullableTime  =  function | null -> box System.DBNull.Value
                                | (s: string) -> s.Trim() |> fun x ->
                                                System.DateTime.TryParseExact(x,"hh:mm:ss",System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.AssumeLocal)
                                                |> function (false,_) -> box System.DBNull.Value | (true,x) -> box (x - x.Date)
let asNullableDateTime = function | null ->  box System.DBNull.Value
                                  | (s: string) -> s.Trim() |> System.DateTime.TryParse |> function (true,s) -> box s | _ -> box System.DBNull.Value

let headers = ["invno";"JobNumber";"TDate";"Reference";"LSTrackingNumber";"Caller";"FromName";"FromNumber";"FromStreet";"FromRoom";
               "FromCity";"FromZip";"ToName";"ToNumber";"ToStreet";"ToRoom";"ToCity";"ToZip";"ServiceCode";"ServiceAmount";
               "ExtraCode1";"Extra1Amount";"ExtraCode2";"Extra2Amount";"ExtraCode3";"Extra3Amount";"ExtraCode4";"Extra4Amount";"EN";"Tax";
               "Total";"Zone";"Weight";"POD";"PODDate";"PODTime";"PickupDate";"SourceId";"RowKey";]

let loadLaserShip excelPath =
    (new LaserShip(excelPath)).Data

let fillDataTable sourceId (rows: IEnumerable<LaserShip.Row>) =
    let dt = new DataTable()
    do headers |> Seq.iter(fun h-> dt.Columns.Add(new DataColumn(h)))
    for row in rows do
        let dr = dt.NewRow()
        dr.Item(0) <- unbox (row.invno |> asString "invno")
        dr.Item(1) <- unbox (row.JobNumber |> asString "JobNumber")
        dr.Item(2) <- unbox (row.TDate |> asString "TDate")
        dr.Item(3) <- unbox (row.Reference |> asNullableString)
        dr.Item(4) <- unbox (row.LSTrackingNumber |> asNullableString)
        dr.Item(5) <- unbox (row.Caller |> asNullableString)
        dr.Item(6) <- unbox (row.FromName |> asNullableString)
        dr.Item(7) <- unbox (row.FromNumber |> asNullableString)
        dr.Item(8) <- unbox (row.FromStreet |> asNullableString)
        dr.Item(9) <- unbox (row.FromRoom |> asNullableString)
        dr.Item(10) <- unbox (row.FromCity |> asNullableString)
        dr.Item(11) <- unbox (row.FromZip |> asNullableString)
        dr.Item(12) <- unbox (row.ToName |> asString "ToName")
        dr.Item(13) <- unbox (row.ToNumber |> asNullableString)
        dr.Item(14) <- unbox (row.ToStreet |> asNullableString)
        dr.Item(15) <- unbox (row.ToRoom |> asNullableString)
        dr.Item(16) <- unbox (row.ToCity |> asString "ToCity")
        dr.Item(17) <- unbox (row.ToZip |> asString "ToZip")
        dr.Item(18) <- unbox (row.ServiceCode |> asString "ServiceCode")
        dr.Item(19) <- unbox (row.ServiceAmount |> asNullableMoney)
        dr.Item(20) <- unbox (row.ExtraCode1 |> asNullableString)
        dr.Item(21) <- unbox (row.Extra1Amount |> asNullableMoney)
        dr.Item(22) <- unbox (row.ExtraCode2 |> asNullableString)
        dr.Item(23) <- unbox (row.Extra2Amount |> asNullableMoney)
        dr.Item(24) <- unbox (row.ExtraCode3 |> asNullableString)
        dr.Item(25) <- unbox (row.Extra3Amount |> asNullableMoney)
        dr.Item(26) <- unbox (row.ExtraCode4 |> asNullableString)
        dr.Item(27) <- unbox (row.Extra4Amount |> asNullableMoney)
        dr.Item(28) <- unbox (row.EN |> asNullableMoney)
        dr.Item(29) <- unbox (row.Tax |> asNullableMoney)
        dr.Item(30) <- unbox (row.Total |> asNullableMoney)
        dr.Item(31) <- unbox (row.Zone |> asNullableString)
        dr.Item(32) <- unbox (row.Weight |> asNullableDecimal)
        dr.Item(33) <- unbox (row.POD |> asNullableString)
        dr.Item(34) <- unbox (row.PODDate |> asNullableString)
        dr.Item(35) <- unbox (row.PODTime |> asNullableString)
        dr.Item(36) <- unbox (row.PickupDate |> asNullableString)
        dr.Item(37) <- sourceId
        dt.Rows.Add(dr)
    printfn "loaded %d rows" dt.Rows.Count
    dt

let loadIntoSQL tableName connStr (dataTable: DataTable) =
    use con = new SqlConnection(connStr)
    con.Open()
    use bulkCopy = new SqlBulkCopy(con, DestinationTableName = tableName)
    bulkCopy.WriteToServer(dataTable)
    printfn "Finished write to server"
    
loadLaserShip @"C:\users\gene\Downloads\Book1.xlsx"
|> fillDataTable 42 // Source File Id
|> loadIntoSQL "LaserShip" @"Data Source=(localdb)\ProjectsV12;Initial Catalog=Colossus.DB;Integrated Security=True;Pooling=False;Connect Timeout=30"

