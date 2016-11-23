#I __SOURCE_DIRECTORY__
#r "../packages/FSharp.Quotations.Evaluator.1.0.7/lib/net40/FSharp.Quotations.Evaluator.dll"
open FSharp.Quotations.Evaluator

///////////// Demo abilities /////////////
let mutable divider = Quotations.Expr.Value (5)
let is5Divisor = <@ fun x -> x % %%divider = 0 @> |> QuotationEvaluator.Evaluate
is5Divisor 14
is5Divisor 15
divider <- Quotations.Expr.Value (7)
is5Divisor 14
let is7Divisor = <@ fun x -> x % %%divider = 0 @> |> QuotationEvaluator.Evaluate
is7Divisor 14
/////////////////////////////////////////////

open System.Collections.Generic
open System

type Adjustment =
| Absent
| Premium of TimeSpan * decimal
| Penalty of TimeSpan * decimal

type Terms(?premium: Adjustment, ?penalty: Adjustment) =
    let penalty = defaultArg penalty Absent
    let premium = defaultArg premium Absent
          
    member x.Adjust() =
        match premium,penalty with
        | Absent,Absent -> None
        | Absent,Penalty (d,m) -> Some(<@ fun ((date:DateTime),amount) -> if DateTime.UtcNow.Date - date.Date > d then Decimal.Round(amount * (1M + m),2) else amount @> |> QuotationEvaluator.Evaluate)
        | Premium(d,m),Absent -> Some(<@ fun ((date:DateTime),amount) -> if DateTime.UtcNow.Date - date.Date < d then Decimal.Round(amount * (1M - m),2) else amount @> |> QuotationEvaluator.Evaluate)
        | Premium(d',m'),Penalty (d,m) -> Some(<@ fun ((date:DateTime),amount) ->
            if DateTime.UtcNow.Date - date.Date > d then Decimal.Round(amount * (1M + m),2)
            elif DateTime.UtcNow.Date - date.Date < d' then Decimal.Round(amount * (1M - m'),2)
            else amount @> |> QuotationEvaluator.Evaluate)
        | _,_ -> None


////////////////////////////////////////////////////////////////////////////////////////////

type Invoice = { total:decimal ; date:System.DateTime; }

let invoices = [
    { total=1005.20M; date=System.DateTime.Today.AddDays(-3.0) }
    { total=5027.78M; date=System.DateTime.Today.AddDays(-29.0) }
    { total=51400.49M; date=System.DateTime.Today.AddDays(-36.0) }
]

let payment (terms: Terms) invoice = let adjust = terms.Adjust() in if adjust.IsSome then (adjust.Value) (invoice.date, invoice.total) else invoice.total

let terms = Terms(penalty=Penalty(TimeSpan.FromDays(31.),0.015M),premium=Premium(TimeSpan.FromDays(5.),0.02M))
let termsA = Terms()
let termsB = Terms(Premium(TimeSpan.FromDays(4.),0.02M))
let termsC = Terms(penalty=Penalty(TimeSpan.FromDays(30.),0.02M))

List.map (payment terms) invoices
List.map (payment termsA) invoices
List.map (payment termsB) invoices
List.map (payment termsC) invoices
