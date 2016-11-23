//select top (10) p.[MerchantId], min(r.DisplayName) as Name, sum(p.[Amount]) as Total
//from [sql.colossus].[dbo].[Payments] p
//join [sql.ironmandata].[dbo].[Partner] r on r.MerchantId = p.MerchantId
//where p.[IsDeposited] = 1
//group by p.[MerchantId]
//order by total desc

#r "FSharp.Data.TypeProviders"
#r "System.Data"
#r "System.Data.Linq"

open Microsoft.FSharp.Data.TypeProviders
open System.Linq

[<Literal>]
let compileTimeCsusCS = @"Data Source=(localdb)\projectsv12;Initial Catalog=Colossus.DB;Integrated Security=SSPI"
let runTimeCsusCS = @"Data Source=***;Initial Catalog=SQL.Colossus;User ID=***;Password=***"
[<Literal>]
let compileTimeImCS = @"Data Source=(localdb)\projectsv12;Initial Catalog=SQL.Ironman;Integrated Security=SSPI"
let runTimeImCS = @"Data Source=***;Initial Catalog=SQL.IronmanData;User ID=***;Password=***"

type Colossus = SqlDataConnection<compileTimeCsusCS>
type IronManData = SqlDataConnection<compileTimeImCS>

let pmtContext = Colossus.GetDataContext(runTimeCsusCS)
let imContext = IronManData.GetDataContext(runTimeImCS)

// Uncomment to see the T-SQL into which the LINQ from query {...} is translated
//pmtContext.Payments.Context.Log <- new System.IO.StreamWriter(@"C:\users\gene\downloads\0\pmtlinq.log", AutoFlush = true)
//imContext.Partner.Context.Log <- new System.IO.StreamWriter(@"C:\users\gene\downloads\0\imlinq.log", AutoFlush = true)

let mostPaid =
    fun x -> query {
                for payment in pmtContext.Payments do
                where (payment.IsDeposited.HasValue && payment.IsDeposited.Value)
                groupBy payment.MerchantId into p
                let total = query { for payment in p do sumBy payment.Amount}
                sortByDescending total
                select (p.Key,total)
                take x
             }

let active = (mostPaid 10)
let activeIds = active |> Seq.map fst

let mostActiveNames =
    query {
        for merchant in imContext.Partner do
        where (activeIds.Contains(merchant.MerchantId))
        select (merchant.MerchantId,merchant.DisplayName)
    } |> dict

active
|> Seq.map (fun (id, total) -> (mostActiveNames.[id],total))
|> Seq.iter (fun x -> printfn "%s: %.2f" (fst x) (snd x))