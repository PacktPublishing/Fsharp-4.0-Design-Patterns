// Command design pattern
type OrderType = Sale | Refund
type Transaction = Transaction of OrderType * decimal

let sale total cost = total + cost
let refund total cost = total - cost

let Order total = function
| Transaction(OrderType.Sale, cost) -> sale total cost
| Transaction(OrderType.Refund, cost) -> refund total cost

let Cancellation total = function
| Transaction(OrderType.Sale, cost) -> refund total cost
| Transaction(OrderType.Refund, cost) -> sale total cost

let orderFlow = [Transaction(OrderType.Sale, 25.98M); Transaction(OrderType.Sale, 15.03M);
                 Transaction(OrderType.Refund, 19.49M); Transaction(OrderType.Sale, 250.34M)]

let totalForward = orderFlow |> Seq.fold Order 0.0M
let totalBackward = orderFlow |> Seq.fold Cancellation totalForward