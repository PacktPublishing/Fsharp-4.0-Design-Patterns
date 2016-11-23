#r "FSharp.Data.TypeProviders"
#r "System.Data"
#r "System.Data.Linq"

open Microsoft.FSharp.Data.TypeProviders
open System.Linq

[<Literal>]
let runTimeCsusCS = @"Data Source=***;Initial Catalog=SQL.Colossus;User ID=***;Password=***"

type Colossus = SqlDataConnection<runTimeCsusCS>

let pmtContext = Colossus.GetDataContext(runTimeCsusCS)
pmtContext.Payments.Context.Log <- new System.IO.StreamWriter(@"C:\users\gene\downloads\0\pmtlinq.log", AutoFlush = true)
//// COMPOSABLE ////
type PartialQueryBuilder() =
    inherit Linq.QueryBuilder()
    member __.Run(e: Quotations.Expr<Linq.QuerySource<'T,IQueryable>>) = e

let pquery = PartialQueryBuilder()

type Linq.QueryBuilder with
    [<ReflectedDefinition>]
    member __.Source(qs: Linq.QuerySource<'T,_>) = qs

let mostPaid = pquery {
                    for payment in pmtContext.Payments do
                    where (payment.IsDeposited.HasValue && payment.IsDeposited.Value)
                    groupBy payment.MerchantId into p
                    let total = pquery { for payment in p do sumBy payment.Amount}
                    sortByDescending total
                    select (p.Key,total)
                    take 10
                         }

let dashboard = pquery {
                    for merchant in pmtContext.Partner do
                        for (id,total) in %mostPaid do
                        where (merchant.MerchantId = id )
                        select (merchant.DisplayName, total)
                       }

query { for m in %dashboard do
           select m } |> Seq.iter (fun x -> printfn "%s: %.2f" (fst x) (snd x))